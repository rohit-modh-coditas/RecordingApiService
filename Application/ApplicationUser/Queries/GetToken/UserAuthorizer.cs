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

namespace Application.ApplicationUser.Queries.GetToken
{
    class UserAuthorizer:IAuthorizer<UserAuthQuery>
    {
        private readonly IStoreDbContext _storeDbContext;
        private readonly ICurrentUserService _currentUserService;

        public UserAuthorizer(IStoreDbContext storeDbContext, ICurrentUserService currentUserService)
        {
            _storeDbContext = storeDbContext;
            _currentUserService = currentUserService;
        }

        public async Task<AuthorizationResult> AuthorizeAsync(UserAuthQuery instance, CancellationToken cancellation = default)
        {
          
            var userId = _currentUserService.UserId;
            var userLogin = await _storeDbContext.AspNetUsers
                .FirstOrDefaultAsync(x => x.Email == instance.Email && x.PasswordHash == instance.Password, cancellation);
            string token = "";
            if (userLogin != null)
                //get token
                
                return AuthorizationResult.Succeed(token);

            return AuthorizationResult.Fail("You are not a User.");
        }
    }
}
