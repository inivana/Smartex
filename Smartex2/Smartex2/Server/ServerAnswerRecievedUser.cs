using Newtonsoft.Json;
using Smartex.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smartex.Server
{
    class ServerAnswerRecievedUser
    {

        [JsonProperty(PropertyName = "result")]
        public UserPersonalInfo User { get; set; }
        [JsonProperty(PropertyName = "status")]
        public String Status { get; set; }

    }
}
