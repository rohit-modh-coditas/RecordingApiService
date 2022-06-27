using System;

namespace Application.Common.Exceptions
{
    public class InvalidValueException : Exception
    {
        public InvalidValueException()
            : base()
        {
        }

        public InvalidValueException(string message, string key)
          : base(string.Format(message, key))
        {
        }
    }
}