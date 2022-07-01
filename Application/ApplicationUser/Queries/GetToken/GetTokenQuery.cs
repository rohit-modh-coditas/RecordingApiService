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
    public class GetTokenQuery : IRequest<ServiceResult<LoginResponse>>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class GetTokenQueryHandler : IRequestHandler<GetTokenQuery, ServiceResult<LoginResponse>>
    {
        //private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;

        public GetTokenQueryHandler(ITokenService tokenService)
        {
            
            _tokenService = tokenService;
        }

        public async Task<ServiceResult<LoginResponse>> Handle(GetTokenQuery request, CancellationToken cancellationToken)
        {
            //var user = await _identityService.CheckUserPassword(request.Email, request.Password);

            //if (user == null)
            //    return ServiceResult.Failed<LoginResponse>(ServiceError.ForbiddenError);

            

            return ServiceResult.Success(new LoginResponse
            {
              
                Token = Task.Run(()=>_tokenService.CreateJwtSecurityToken(request.Email)).Result
            });
        }
    }
}
