using System;
using System.Collections.Generic;
using System.Text;

namespace Smartex.Exception
{
    public class UserAlreadyRegisteredException : System.Exception
    {
        private static String errorMessage = "Użytkownik o podanym logini!";
        public UserAlreadyRegisteredException() : base(errorMessage)
        {
        }
        public UserAlreadyRegisteredException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
