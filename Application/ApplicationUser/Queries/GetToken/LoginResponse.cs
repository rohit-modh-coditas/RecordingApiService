using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ApplicationUser.Queries.GetToken
{
    public class LoginResponse
    {
        public ApplicationUserDto User { get; set; }

        public string Token { get; set; }
    }
}
