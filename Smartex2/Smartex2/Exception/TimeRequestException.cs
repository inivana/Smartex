using System;
using System.Collections.Generic;
using System.Text;

namespace Smartex.Exception
{
    public class TimeRequestException : System.Exception
    {
        private static String errorMessage = "Server jest obciążony spórbuj za pare minut!";
        public TimeRequestException() : base(errorMessage)
        {
        }
        public TimeRequestException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
