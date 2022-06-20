using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class UserAlreadyExistException : Exception
    {
        public UserAlreadyExistException():base()
        {

        }

        public UserAlreadyExistException(string Email) : base($"Email \"{Email}\" Already exists. Please signin with correct credentials.")
        {

        }
    }
}
