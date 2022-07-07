using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Recordings.Queries.GetRecordings;
using Microsoft.Extensions.Configuration;

namespace Application.ApplicationUser.Queries.UserAuth
{
    class UserAuthorizer:IAuthorizer<GetRecordingsQuery>
    {
        private readonly ITokenService _tokenService;
        private readonly IGoogleCloudStorageService _gcpService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IConfiguration _configuration;
       
        public UserAuthorizer(ITokenService tokenService, IConfiguration configuration,  ICurrentUserService currentUserService, IGoogleCloudStorageService gcpService)
        {
            _gcpService = gcpService;
            _tokenService = tokenService;
            _currentUserService = currentUserService;
            _configuration = configuration;
        }

        public Task<AuthorizationResult> AuthorizeAsync(GetRecordingsQuery instance,string token, CancellationToken cancellation = default)
        {
            if (!string.IsNullOrEmpty(token)) //instance.context.Request.Headers.TryGetValue("authkey", out var extractkey)
            {
                string payload = _gcpService.getSecret("recordingApiSecret");
                if (!payload.Equals(token))
                {
                    return Task.Run(() => AuthorizationResult.Fail());
                }
            }
            return Task.Run(() => AuthorizationResult.Succeed());

        }
    }
}
