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
        //private readonly IIdentityService _identityService;

        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger, ILogger<LoggingBehaviour<TRequest, TResponse>> eLogger, ICurrentUserService currentUserService)
        {
            _eLogger = eLogger;
            _logger = logger;
            _currentUserService = currentUserService;
            //_identityService = identityService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogInformation("CleanArchitecture Request: {Name} {@UserId} {@UserName} {@Request}",
               requestName, request);
            _eLogger.LogInformation("CleanArchitecture Request: {Name} {@UserId} {@UserName} {@Request}",
               requestName, request);
            var response = await next();
            

            _logger.LogInformation("CleanArchitecture Request: {Name} {@UserId} {@UserName} {@Request}",
               requestName, request);
            _eLogger.LogInformation("CleanArchitecture Request: {Name} {@UserId} {@UserName} {@Request}",
               requestName, request);
            return response;
        }

        //public async Task Process(TRequest request, CancellationToken cancellationToken)
        //{
        //    var requestName = typeof(TRequest).Name;
        //    var userId = _currentUserService.UserId ?? string.Empty;
        //  //  var userId = "U123";
        //    string userName = string.Empty;

        //    if (!string.IsNullOrEmpty(userId))
        //    {
        //        //userName = await _identityService.GetUserNameAsync(userId);
        //    }

        //    _logger.LogInformation("CleanArchitecture Request: {Name} {@UserId} {@UserName} {@Request}",
        //        requestName, userId, userName, request);
        //    var response = await StringNormalizationExtensions();
        //}
    }
}
