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
            foreach (var authorizer in _authorizers)
            {
                var result = await authorizer.AuthorizeAsync(request, cancellationToken);
                if (!result.IsAuthorized)
                    throw new UnAuthorizedAccessException(result.FailureMessage);
            }
            var response = next();
            return await response;
        }
    }
}
