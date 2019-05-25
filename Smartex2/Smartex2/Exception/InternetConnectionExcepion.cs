using System;
using System.Collections.Generic;
using System.Text;

namespace Smartex.Exception
{
    public class InternetConnectionExcepion : System.Exception
    {
        private static String errorMessage = "Brak dostępu do sieci(rybackiej)!";
        public InternetConnectionExcepion() : base(errorMessage)
        {
        }
        public InternetConnectionExcepion(string errorMessage) : base(errorMessage)
        {
        }
    }
}
