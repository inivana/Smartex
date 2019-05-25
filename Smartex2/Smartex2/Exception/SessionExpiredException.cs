using System;
using System.Collections.Generic;
using System.Text;

namespace Smartex.Exception
{
    public class SessionExpiredException : System.Exception
    {
        private static String errorMessage = "Sesja wygasła. Zaloguj się ponownie!";
        public SessionExpiredException() : base(errorMessage)
        {
        }
        public SessionExpiredException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
