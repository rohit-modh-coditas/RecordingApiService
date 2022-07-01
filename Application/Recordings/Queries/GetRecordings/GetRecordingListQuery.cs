using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Recordings.Queries.GetRecordings
{
    public class GetRecordingListQuery : IRequest<RecordingListVm>
    {
        public HttpContext context { get; set; }
    }
    public class GetRecordingListQueryHandler : IRequestHandler<GetRecordingListQuery, RecordingListVm>
    {
        private readonly I10XStagingDbContext _StagingDbContext;
        // private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _distributedCache;
        private readonly IMapper _mapper;


        public GetRecordingListQueryHandler(I10XStagingDbContext stagingDbContext, IDistributedCache distributedCache, IMapper mapper)
        {
            _StagingDbContext = stagingDbContext;

            _distributedCache = distributedCache;
            _mapper = mapper;
        }

        public async Task<RecordingListVm> Handle(GetRecordingListQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = "callRecording100";
            string serializedCustomerList;
            var redisRecordingList = await _distributedCache.GetAsync(cacheKey);
            if (redisRecordingList != null)
            {
                serializedCustomerList = Encoding.UTF8.GetString(redisRecordingList);
                var res = JsonConvert.DeserializeObject<IEnumerable<ConversationRecordingViewModel>>(serializedCustomerList);
                if (res == null)
                {
                    throw new NotFoundException("Call Recording");
                }
                return new RecordingListVm
                {
                    recordList = res.ToList()
                };
            }
            else
            {

                var res = await _StagingDbContext.TCalls.Take(100)
                         .AsNoTracking().ProjectTo<ConversationRecordingViewModel>(_mapper.ConfigurationProvider)
                         .ToListAsync(cancellationToken);

                serializedCustomerList = JsonConvert.SerializeObject(res);
                redisRecordingList = Encoding.UTF8.GetBytes(serializedCustomerList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await _distributedCache.SetAsync(cacheKey, redisRecordingList, options);
                if (res == null)
                {
                    throw new NotFoundException("Call Recording");
                }
                return new RecordingListVm
                {
                    recordList = res
                };
            }
        }

        //public async Task<RecordingListVm> Handle(GetRecordingListQuery request, CancellationToken cancellationToken)
        //{
        //    var res = await _StagingDbContext.TCalls.Take(100)
        //                .AsNoTracking().ProjectTo<ConversationRecordingViewModel>(_mapper.ConfigurationProvider)
        //                .ToListAsync(cancellationToken);

        //    //serializedCustomerList = JsonConvert.SerializeObject(res);
        //    //redisRecordingList = Encoding.UTF8.GetBytes(serializedCustomerList);
        //    //var options = new DistributedCacheEntryOptions()
        //    //    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
        //    //    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
        //    //await _distributedCache.SetAsync(cacheKey, redisRecordingList, options);
        //    if (res == null)
        //    {
        //        throw new NotFoundException("Call Recording");
        //    }
        //    return new RecordingListVm
        //    {
        //        recordList = res
        //    };
        //}


    }
}
