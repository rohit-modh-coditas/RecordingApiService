using Application.Common.Interfaces;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.SecretManager.V1;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class GoogleCloudStorageService: IGoogleCloudStorageService
    {
        private static SecretManagerServiceClient _client;
        private static string _projectId;
        public static GoogleCredential _googleCredential;
        public static StorageClient _storageClient;
        public static string _bucketName;
        public static string _googleAuthFilePath;
        public static string _environment;
        private readonly IConfiguration _configuration;
        public GoogleCloudStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
            
            _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        }
        public static void Login()
        {
            if (_environment.Equals("Development")) //this is only For testing locally
            {
                _googleCredential = GoogleCredential.FromFile(_googleAuthFilePath);//.GetApplicationDefault();
            }
            else
            {
                _googleCredential = GoogleCredential.GetApplicationDefault();
            }
            _storageClient = StorageClient.Create(_googleCredential);
        }
        public static void UploadRecording(string key, string fileName)
        {
            if (_storageClient == null)
            {
                Login();
            }
            using (var f = File.OpenRead(fileName))
            {
                _storageClient.UploadObject(_bucketName, key, null, f);
            }
        }

        public static void UploadAllRecording(string key, string googleAuthFilePath,string bucket, string recordingBasePath)
        {
            _googleAuthFilePath = googleAuthFilePath;
            _bucketName = bucket;
            DirectoryInfo dir = new DirectoryInfo(recordingBasePath);
            FileInfo[] files = dir.GetFiles("*.wav"); //change to mp3
            foreach (var file in files)
            {
                string fileName = file.Name;
                UploadRecording(key + fileName, recordingBasePath + fileName);
            }
        }
       
        public static void DownloadRecording(string key, string fileName)
        {
            if (_storageClient == null)
            {
                Login();
            }
            using (var f = File.OpenWrite(fileName))
            {
                _storageClient.DownloadObject(_bucketName, key, f);
            }
        }

        public string getSecret(string key)
        {
            StringBuilder payload = new StringBuilder();
            if (_environment.Equals("Development")) //this is only For testing locally
            {
                _projectId = "cas-prod-env";
                _googleAuthFilePath = _configuration["GoogleAuthFilePath"];
                var text = File.ReadAllText(_googleAuthFilePath);
                SecretManagerServiceClientBuilder secretManagerServiceClientBuilder = new SecretManagerServiceClientBuilder()
                {
                    JsonCredentials = text,
                };
                _client = secretManagerServiceClientBuilder.Build();
            }
            else
            {
                _client = SecretManagerServiceClient.Create();
                _projectId = GoogleProject.GetProjectId();
            }
            SecretVersionName secretVersionName = new SecretVersionName(_projectId, key, "1");
            // Call the API.
            AccessSecretVersionResponse result = _client.AccessSecretVersion(secretVersionName);
            // Convert the payload to a string. Payloads are bytes by default.
            payload.Append(result.Payload.Data.ToStringUtf8());

            return payload.ToString();
        }
    }
}
