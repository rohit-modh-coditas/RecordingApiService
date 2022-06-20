using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class AuthorizationResult
    {
        public AuthorizationResult() { }
        private AuthorizationResult(bool isAuthorized, string failureMessage, string token)
        {
            IsAuthorized = isAuthorized;
            FailureMessage = failureMessage;
            Token = token;
        }
        private AuthorizationResult(string userName, string role, string name)
        {
            UserName = userName;
        }
        public bool IsAuthorized { get; }
        public string FailureMessage { get; set; }

        public string UserName{ get; set; }
        
        public string Token{ get; set; }

        public static AuthorizationResult Fail()
        {
            return new AuthorizationResult(false, null, string.Empty);
        }

        public static AuthorizationResult Fail(string failureMessage)
        {
            return new AuthorizationResult(false, failureMessage, string.Empty);
        }

        public static AuthorizationResult Succeed()
        {
            return new AuthorizationResult(true, null, string.Empty);
        }
        public static AuthorizationResult Succeed(string token)
        {
            return new AuthorizationResult(true, null, token);
        }

        public static AuthorizationResult Succeed(string UserName, string Role, string token)
        {
            return new AuthorizationResult(UserName, Role, token);
        }
    }
}
