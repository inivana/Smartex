using Newtonsoft.Json;
using Smartex.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smartex.Server
{
    class ServerAnswerRecievedEvents
    {
        [JsonProperty(PropertyName = "result")]
        public List<Event> EventList { get; set; }
        [JsonProperty(PropertyName = "status")]
        public String Status { get; set; }

    }
}
