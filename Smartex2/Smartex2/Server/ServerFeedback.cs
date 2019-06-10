using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smartex.Server
{
    /**
    * Obiekt klasy stworzony by weryfikować powodzenie żądań PUT,DELETE;
    */
    class ServerFeedback
    {
        [JsonProperty(PropertyName = "status")]
        public String Status { get; set; }
    }
}
