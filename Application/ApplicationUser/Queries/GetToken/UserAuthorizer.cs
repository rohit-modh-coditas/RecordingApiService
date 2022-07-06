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

namespace Application.ApplicationUser.Queries.GetToken
{
    class UserAuthorizer:IAuthorizer<GetRecordingsQuery>
    {
        private readonly ITokenService _tokenService;
        private readonly IGoogleCloudStorageService _gcpService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IConfiguration _configuration;

        public UserAuthorizer(ITokenService tokenService, IConfiguration configuration, ICurrentUserService currentUserService, IGoogleCloudStorageService gcpService)
        {
            _gcpService = gcpService;
            _tokenService = tokenService;
            _currentUserService = currentUserService;
            _configuration = configuration;
        }

        //public async Task<AuthorizationResult> AuthorizeAsync(UserAuthQuery instance, CancellationToken cancellation = default)
        //{
          
        //    var userId = _currentUserService.UserId;
        //    var userLogin = await _storeDbContext.AspNetUsers
        //        .FirstOrDefaultAsync(x => x.Email == instance.Email && x.PasswordHash == instance.Password, cancellation);
        //    string token = "";
        //    if (userLogin != null)
        //        //get token
                
        //        return AuthorizationResult.Succeed(token);

        //    return AuthorizationResult.Fail("You are not a User.");
        //}

        //public Task<AuthorizationResult> AuthorizeAsync(GetRecordingsQuery instance, CancellationToken cancellation = default)
        //{
        //    if (!string.IsNullOrEmpty(instance.AuthToken))
        //    {
        //        //get token
        //        string token = _tokenService.CreateJwtSecurityToken(instance.AuthToken);
        //        return Task.Run(() => AuthorizationResult.Succeed(token));
        //    }
        //    throw new NotImplementedException();
        //}

        public Task<AuthorizationResult> AuthorizeAsync(GetRecordingsQuery instance, CancellationToken cancellation = default)
        {
            
            if (instance.context.Request.Headers.TryGetValue("Auth-Key", out var extractkey))
            {
                // string token =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Auth:Secret"]));
                string payLoad = _gcpService.getSecret("RecordingAPISecret");
                if (!payLoad.Equals(extractkey))
                {
                    return Task.Run(() => AuthorizationResult.Fail());
                }
            }
            return Task.Run(() => AuthorizationResult.Succeed());

        }
    }
}
