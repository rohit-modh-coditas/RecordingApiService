using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Common;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using NAudio.Lame;
using NAudio.Wave;
using System.IO;

namespace Core.Services
{
    public static class RecordingExtension
    {
        public static void getCallableNumber(this string callNumber, ref List<string> CallableNumbers)
        {
            //CallableNumbers = new List<string>();
            if (!string.IsNullOrEmpty(callNumber))
            {
                callNumber = callNumber.Replace("+", "").Trim();
                if (callNumber.FirstOrDefault() != '1')
                {
                    CallableNumbers.Add(string.Concat("011", callNumber));//011 is the phone exit code 
                    CallableNumbers.Add(string.Concat("1", callNumber));//1 is the local code
                    /*a number cud belong to US 48 states or Canada. These numbers are dailed with a prefix of 1 and 
                    no exitcode of 011 is required.*/
                }
                else
                {
                    CallableNumbers.Add(callNumber);
                }
            }
            //return callableNumbers;
        }
        public static string Get(this Dictionary<string, string> map, string key)
        {
            return map[key];
        }
    }
    public class RecordingUtilityService : IRecordingUtilityService
    {
        public void ConvertToMP3Recording(string localPath)
        {
            try
            {
                var wavFile = File.ReadAllBytes(localPath);
                using (var retMs = new MemoryStream())
                using (var ms = new MemoryStream(wavFile))
                using (var rdr = new WaveFileReader(ms))
                using (var wtr = new LameMP3FileWriter(retMs, rdr.WaveFormat, 128))
                {
                    rdr.CopyTo(wtr);
                    var byteArr = retMs.ToArray();
                    localPath = localPath.Replace(".wav", ".mp3");
                    File.WriteAllBytes(localPath, byteArr);
                }
            }
            catch (Exception e)
            {
                throw new RecordingFailureException("Conversion failed for .wav to .mp3 files - " + e);
            }
        }

        public bool FetchCdrRecording(IDateTime dateTime, IConfiguration _config, string called1, string called2, string called3, int LeadTransitId, string recordSavePath, int TimeBuffer, int TimeShift)
        {
            try
            {
                dateTime.StartTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime.StartTime, dateTime.LocalTimeZone);
                dateTime.StartTime = dateTime.StartTime.AddSeconds(TimeShift);

                DateTime startTime = dateTime.StartTime.AddSeconds(-TimeBuffer);
                DateTime startTimeTo = dateTime.StartTime.AddSeconds(TimeBuffer);
                //dateTime.StartTime = dateTime.StartTime.AddSeconds(-TimeBuffer);
                //dateTime.StartTime = dateTime.StartTime.AddSeconds(TimeBuffer);
                string queryStartTime = String.Format("{0:yyyy-MM-dd HH:mm:ss}", startTime);
                string queryStartTimeTo = String.Format("{0:yyyy-MM-dd HH:mm:ss}", startTimeTo);
                List<string> callableNumbers = new List<string>();
                called1.getCallableNumber(ref callableNumbers);
                called2.getCallableNumber(ref callableNumbers);
                called3.getCallableNumber(ref callableNumbers);

                int PhoneCount = callableNumbers.Count;

                for (int i = 0; i < PhoneCount; i++)
                {
                    StringBuilder cdrUrl = new StringBuilder();
                    cdrUrl.Append(_config.GetValue<string>("RecordingsServerBasePath").ToString() + "api.php?task=getVoipCalls&user=" + Constants.CdrLoginCredentials.Get("UserName") + "&password=" + Constants.CdrLoginCredentials.Get("Password") + "&params={\"startTime\":\"" + queryStartTime + "\",\"startTimeTo\":\"" + queryStartTimeTo + "\",\"called\":\"" + callableNumbers[i] + "\"}");
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(cdrUrl.ToString());
                    httpWebRequest.Method = "GET";
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        StringBuilder result = new StringBuilder();
                        result.Append(streamReader.ReadToEnd());
                        dynamic json = JsonConvert.DeserializeObject(result.ToString());
                        if (json != null)
                        {
                            if (json.success != null && json.success == true && json.cdr != null)
                            {
                                string cdrId = "";
                                double difference = 0;    // lowest value
                                foreach (var item in json.cdr)
                                {
                                    DateTime callDate = item.calldate;
                                    DateTime callEnd = item.callend;
                                    if (difference < Math.Abs(callEnd.ToUnixTime() - callDate.ToUnixTime()))
                                    {
                                        cdrId = item.cdrId;
                                        difference = Math.Abs(callEnd.ToUnixTime() - callDate.ToUnixTime());
                                    }
                                }
                                StringBuilder recordingUrl = new StringBuilder();
                                recordingUrl.Append(_config.GetValue<string>("RecordingsServerBasePath").ToString() + "api.php?task=getVoiceRecording&user=" + Constants.CdrLoginCredentials.Get("UserName") + "&password=" + Constants.CdrLoginCredentials.Get("Password") + "&params={\"cdrId\":\"" + cdrId + "\"}");

                                using (MyWebCient client = new MyWebCient(_config))
                                {
                                    client.DownloadFile(recordingUrl.ToString(), recordSavePath);
                                    return true;
                                }
                            }
                            //else
                            //{
                            //    throw new RecordingFailureException("Json Failure: " + json.error != null ? json.error.ToString() : json.ToString());
                            //}
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new RecordingFailureException("Error Fetching recording from CDR - " + e);
            }

            return true;
        }

        public void MoveRecordingToGCS(string recordingBasePath, IConfiguration _config)
        {
            recordingBasePath += "\\";
            string datePath = recordingBasePath.Split(new string[] { _config.GetValue<string>("RecordingsBasePath") }, StringSplitOptions.None)[1].Replace("\\", "/");
            string key = _config.GetValue<string>("S3RecordingBaseKey") + datePath;
            string GoogleAuthFilePath = _config.GetValue<string>("GoogleAuthFilePath");
            string _bucketName = _config.GetValue<string>("GCSBucketName");

            GoogleCloudStorageService.UploadAllRecording(key,GoogleAuthFilePath, _bucketName, recordingBasePath);
        }
    }
}
