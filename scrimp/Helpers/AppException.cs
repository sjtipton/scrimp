using System;
using System.Globalization;

namespace scrimp.Helpers
{
    public class AppException : Exception
    {
        public Guid Id { get; set; }

        public AppException() : base() { }

        public AppException(string message) : base(message) { }

        public AppException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
