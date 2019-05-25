using System;
using System.Collections.Generic;
using System.Text;

namespace Smartex.Exception
{
    public class UnknownException : System.Exception
    {
        private static String errorMessage = "Ups.. coś poszło nie tak!";
        public UnknownException() : base(errorMessage)
        {
        }
        public UnknownException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
