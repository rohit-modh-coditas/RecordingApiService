using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Behaviours
{
    public class RequestAuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : IRequest<TResponse>
    {

        private readonly IEnumerable<IAuthorizer<TRequest>> _authorizers;
        private readonly ICurrentUserService _currentUserService;

        public RequestAuthorizationBehavior(IEnumerable<IAuthorizer<TRequest>> authorizers, ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
            _authorizers = authorizers;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            bool Authenticated = false;
            foreach (var authorizer in _authorizers)
            {
                var result = await authorizer.AuthorizeAsync(request, cancellationToken);
                //var result = await authorizer.AuthorizeAsync();
                if (!result.IsAuthorized)
                    throw new UnAuthorizedAccessException(result.FailureMessage);
                else { Authenticated = true; }
            }
            var response = next();
            var ex = response.Exception;
            if (ex == null)
            {

            }
            if (Authenticated)
            {
                var userId = _currentUserService.UserId ?? string.Empty;
                bool inRole = _currentUserService.ClaimsPrincipal.IsInRole("Admin");
                if (!inRole)
                {
                    throw new UnAuthorizedAccessException("Restricted access..");
                }
            }
            return await response;
        }
    }
}
