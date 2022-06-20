using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ApplicationUser.Queries.GetToken
{
    public class UserAuthQuery : IRequest<AuthorizationResult>
    {
        public string Email { get; set; }

        public string Password { get; set; }

    }
    public class UserAuthQueryHandler : IRequestHandler<UserAuthQuery, AuthorizationResult>
    {
        private readonly ITokenService _tokenService;
        public UserAuthQueryHandler(ITokenService tokenService)
        {

            _tokenService = tokenService;
        }

        public  Task<AuthorizationResult> Handle(UserAuthQuery request, CancellationToken cancellationToken)
        {
            //
            string token =  _tokenService.CreateJwtSecurityToken(request);
            return Task.Run(()=>AuthorizationResult.Succeed(token));
        }
    }
}
