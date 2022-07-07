using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Current userId
        /// </summary>
        public string UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        public ClaimsPrincipal ClaimsPrincipal => _httpContextAccessor.HttpContext?.User;
        public string AuthKey => _httpContextAccessor.HttpContext.Request.Headers.Where(x=>x.Key.Equals("AuthKey")).Select(x=>x.Value).FirstOrDefault();
    }
}
