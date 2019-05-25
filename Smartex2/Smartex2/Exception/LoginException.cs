using System;
using System.Collections.Generic;
using System.Text;

namespace Smartex.Exception
{
    public class LoginException : System.Exception
    {
        private static String errorMessage = "Podany login lub hasło jest błędne!";
        public LoginException() : base(errorMessage)
        {
        }
        public LoginException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
