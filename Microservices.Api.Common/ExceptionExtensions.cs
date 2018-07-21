using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Api.Common
{
    public static class ExceptionExtensions
    {
        public static string ExceptionAsString(this Exception ex)
        {
            if (ex == null) return "Exception is empty";
            var text = $"msg:{ex.Message}";
            while (ex.InnerException != null)
            {
                text += $"\r\n\tinner:{ex.InnerException.Message}";
                ex = ex.InnerException;
            }

            return text;
        }
    }
}
