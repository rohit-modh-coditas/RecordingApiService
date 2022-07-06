using Application.Common.Interfaces;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _eLogger;
        private readonly ICurrentUserService _currentUserService;

        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger, ILogger<LoggingBehaviour<TRequest, TResponse>> eLogger, ICurrentUserService currentUserService)
        {
            _eLogger = eLogger;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogInformation("CleanArchitecture Request: {Name} {@Request}", requestName, request);
            var response = await next();
            return response;
        }
    }
}
