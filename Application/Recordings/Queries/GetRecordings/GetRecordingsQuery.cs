using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Recordings.Queries.GetRecordings
{
    public class GetRecordingsQuery : IRequest<ConversationRecordingViewModel>
    {
        public int LeadTransitId { get; set; }
        public int EnvironmentId { get; }
        public string AuthToken { get; set; }
    }
    public class GetRecordingsQueryHandler : IRequestHandler<GetRecordingsQuery, ConversationRecordingViewModel>
    {
        private readonly I10XStagingDbContext _StagingDbContext;
        private readonly ICommonFunctions _CommonFunctions;
        private readonly IRecordingUtilityService _utilityService;
        // private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _config;
        private readonly IDistributedCache _distributedCache;
        private readonly IEmailService _EmailService;
        public IDateTime _dateTime;
        private readonly IMapper _mapper;
        private readonly IAppSettings _appSettings;
        public bool _SFTPCheck = false;
        public string _CompanyName = "";
        public const string IP = "localhost";
        public const int Port = 6379;

        public GetRecordingsQueryHandler(I10XStagingDbContext stagingDbContext, IConfiguration config, IRecordingUtilityService utilityService, IDistributedCache distributedCache, IMapper mapper, IDateTime dateTime, IAppSettings appSettings, ICommonFunctions commonFunctions, IEmailService emailService)
        {
            _config = config;
            _utilityService = utilityService;
            _StagingDbContext = stagingDbContext;
            _distributedCache = distributedCache;
            _mapper = mapper;
            _dateTime = dateTime;
            _appSettings = appSettings;
            _CommonFunctions = commonFunctions;
            _EmailService = emailService;

        }
        public string RecordingCorePath { get; set; }

        public string RecordingBasePath { get; set; }
        public string RecordSavePath { get; set; }

        public async Task<ConversationRecordingViewModel> Handle(GetRecordingsQuery request, CancellationToken cancellationToken)
        {
            //var cacheKey = "callRecording";
            //string serializedCustomerList;
            int primaryNumberIndex = 0;

            //var redisRecordingList = await _distributedCache.GetAsync(cacheKey);
            //if (redisRecordingList != null)
            //{
            //    serializedCustomerList = Encoding.UTF8.GetString(redisRecordingList);
            //    var res = JsonConvert.DeserializeObject<List<ConversationRecordingViewModel>>(serializedCustomerList);
            //    if (res == null)
            //    {
            //        throw new NotFoundException("Call Recording", request.LeadTransitId);
            //    }
            //    return res.FirstOrDefault();
            //}
            //else
            //{

            var recordingQuery = _StagingDbContext.TCalls.Where(x => x.LeadTransitId == request.LeadTransitId)
                                  .AsNoTracking().ProjectTo<ConversationRecordingViewModel>(_mapper.ConfigurationProvider);

            if (!recordingQuery.Any())
            {
                //log not found or exception
                throw new NotFoundException("No Record found with leadId" + request.LeadTransitId);
            }

            var recordingConversationInfo = await recordingQuery.ToListAsync(cancellationToken);
            _dateTime.CreateDate = recordingConversationInfo.Select(x => x.createDate).FirstOrDefault();

            //PERFORM SFTP CHECK - move to common or recording utility
            //to access recording via sftp, add leadtransitid
            StringBuilder sftpRecordingSpecific = new StringBuilder();
            sftpRecordingSpecific.Append(_config.GetValue<string>("SFTPRecordingSpecific"));

            StringBuilder sftpDispositionText = new StringBuilder();
            sftpDispositionText.Append(_config.GetValue<string>("SFTPDisposition"));  //"Meeting Scheduled";

            StringBuilder sftpCompanyName = new StringBuilder();
            sftpCompanyName.Append(_config.GetValue<string>("SFTPCompanyName"));

            if (!string.IsNullOrWhiteSpace(sftpRecordingSpecific.ToString()) &&
                !string.IsNullOrWhiteSpace(sftpCompanyName.ToString()) &&
                !string.IsNullOrWhiteSpace(sftpDispositionText.ToString()))
            {
                _SFTPCheck = true;
            }

            if (recordingQuery.Count() == 1)
            {
                _dateTime.StartTime = recordingConversationInfo[0].LeadCatchTime.Value;

                if (_SFTPCheck && !string.IsNullOrWhiteSpace(recordingConversationInfo[0].companyName) &&
                    !string.IsNullOrWhiteSpace(recordingConversationInfo[0].disposition) &&
                    recordingConversationInfo[0].companyName.ToLower() == sftpCompanyName.ToString().ToLower() &&
                    recordingConversationInfo[0].disposition.ToLower() == sftpDispositionText.ToString().ToLower())
                {
                    recordingConversationInfo[0].IsSFTP = true;
                }
            }
            else if (recordingQuery.Count() == 2)
            {
                if (recordingConversationInfo[0].CallSendTime != null && recordingConversationInfo[1].CallSendTime != null)
                {
                    _dateTime.StartTime = recordingConversationInfo[0].LeadCatchTime.Value;
                }
                primaryNumberIndex = recordingConversationInfo[0].PrimaryNumberIndex ?? 0;
                if (_SFTPCheck && !string.IsNullOrWhiteSpace(recordingConversationInfo[0].companyName) &&
                   !string.IsNullOrWhiteSpace(recordingConversationInfo[0].disposition) &&
                   recordingConversationInfo[0].companyName.ToLower() == sftpCompanyName.ToString().ToLower() &&
                   recordingConversationInfo[0].disposition.ToLower() == sftpDispositionText.ToString().ToLower())
                {
                    recordingConversationInfo[0].IsSFTP = true;
                }
            }
            _CompanyName = recordingConversationInfo[0].companyName;
            // check if disabledCompany
            //var isDisabled = (from c in _StagingDbContext.Companies
            //                  join cs in _StagingDbContext.CasCompanySettings on c.Id equals cs.CompanyId
            //                  where c.CompanyName.Equals(_CompanyName) && cs.SettingKey.Equals(CompanySettingType.DisableRecordingDownloadFromJob.ToString())
            //                  select cs.SettingValue).FirstOrDefault();


            //if (!string.IsNullOrEmpty(isDisabled) || isDisabled.ToLower().Equals("true"))
            //{
            //    throw new ForbiddenAccessException("Company disabled for downloading the recording.");
            //}
            string called1, called2, called3;
            if (primaryNumberIndex == 1)
            {
                called1 = recordingConversationInfo[0].ContactTel2;
                called2 = recordingConversationInfo[0].ContactTel1;
                called3 = recordingConversationInfo[0].ContactTel3;
            }
            else if (primaryNumberIndex == 4)
            {
                called1 = recordingConversationInfo[0].ContactTel3;
                called2 = recordingConversationInfo[0].ContactTel2;
                called3 = recordingConversationInfo[0].ContactTel1;
            }
            else
            {
                called1 = recordingConversationInfo[0].ContactTel1;
                called2 = recordingConversationInfo[0].ContactTel2;
                called3 = recordingConversationInfo[0].ContactTel3;
            }


            if (string.IsNullOrEmpty(called1) && string.IsNullOrEmpty(called2) && string.IsNullOrEmpty(called3))
            {
                throw new NotFoundException("Phone number not found for LeadTransitId: " + request.LeadTransitId);
            }

            //string areaPhoneCodesJsonStr = _config.GetValue<string>("RecordingsAreaPhoneCodeMapPath").ToString();
            //string text = File.ReadAllText(areaPhoneCodesJsonStr);
            //Dictionary<string, string> areaPhoneCodesDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);

            //bool isDualConsentRecording = _CommonFunctions.IsDualConsentRecording(!string.IsNullOrEmpty(called1) ? called1 : !string.IsNullOrEmpty(called2) ? called2 : called3, areaPhoneCodesDict);

            RecordingCorePath = _config.GetValue<string>("RecordingsBasePath");
            RecordingBasePath = RecordingCorePath + _dateTime.CreateDate.Year.ToString() +
                                ("\\") + (_dateTime.CreateDate.Month.ToString()) + ("\\") + (_dateTime.CreateDate.Day.ToString());
            Directory.CreateDirectory(RecordingBasePath);
            RecordSavePath = RecordingBasePath + "\\" + request.LeadTransitId.ToString() + ".wav";

            //logging to be done for base path

            StringBuilder wavFileName = new StringBuilder();
            StringBuilder mp3FileName = new StringBuilder();

            wavFileName.Append(RecordingBasePath + "\\" + request.LeadTransitId.ToString() + (".wav"));

            mp3FileName.Append(RecordingBasePath + "\\" + request.LeadTransitId.ToString() + ".mp3");

            if (File.Exists(wavFileName.ToString()) || File.Exists(mp3FileName.ToString()))
            {
                throw new FileAlreadyExistException("Recording already exists");
            }
            var time = await _StagingDbContext.CasLightningParameters.
                             Where(x => x.Name.Equals(LightningParameterType.CallRecordingTimeBuffer.ToString()) ||
                             x.Name.Equals(LightningParameterType.CallRecordingTimeShift.ToString())).AsNoTracking().ToListAsync(cancellationToken);

            int timeBuffer = Int32.Parse(time[0].Value);
            int timeShift = Int32.Parse(time[1].Value);

            //bool tempResult = _utilityService.FetchCdrRecording(_dateTime, _config, called1, called2, called3, request.LeadTransitId, RecordSavePath, timeBuffer, timeShift);
            //if (tempResult)
            //{
            //    //Execute python script to convert wav to mp3
            //    _utilityService.ConvertToMP3Recording(RecordSavePath);
            //    //_utilityService.MoveRecordingToGCS(RecordingBasePath, _config);
            //}

            bool tempResult = _utilityService.FetchCdrRecording(_dateTime, _config, called1, called2, called3, request.LeadTransitId, RecordSavePath, timeBuffer, timeShift);
            // bool tempResult = _utilityService.FetchCdrRecording(_dateTime, _config, called1, called2, called3, request.LeadTransitId, RecordingCorePath, timeBuffer, timeShift);
            if (tempResult)
            {
                //Execute python script to convert wav to mp3

                _utilityService.MoveRecordingToGCS(RecordingBasePath, _config);
            }
            //                _utilityService.uploadFiletoFTP(RecordingBasePath + "\\" + "81413021" + ".wav");

            //serializedCustomerList = JsonConvert.SerializeObject(recordingConversationInfo);
            //redisRecordingList = Encoding.UTF8.GetBytes(serializedCustomerList);
            //var options = new DistributedCacheEntryOptions()
            //    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
            //    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
            //await _distributedCache.SetAsync(cacheKey, redisRecordingList, options);

            // send email
            // _EmailService.SendEmail();

            if (recordingConversationInfo == null)
            {
                throw new NotFoundException("Call Recording", request.LeadTransitId);
            }
            return recordingQuery.SingleOrDefault();

        }
    }
}
