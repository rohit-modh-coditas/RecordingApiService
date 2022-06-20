using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class FileAlreadyExistException : Exception
    {
        public FileAlreadyExistException() : base()
        {

        }
        public FileAlreadyExistException(string message)
           : base(message)
        {
        }

    }
}
