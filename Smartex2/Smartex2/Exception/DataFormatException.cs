using System;
using System.Collections.Generic;
using System.Text;

namespace Smartex.Exception
{
    public class DataFormatException : System.Exception
    {
        private static String errorMessage = "Podane dane mają niewłasciwy format!";
        public DataFormatException() : base(errorMessage)
        {
        }
        public DataFormatException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
