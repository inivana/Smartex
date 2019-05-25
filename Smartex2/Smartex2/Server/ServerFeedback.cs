using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smartex.Server
{
    class ServerFeedback
    {
        [JsonProperty(PropertyName = "status")]
        public String Status { get; set; }


    }
}
