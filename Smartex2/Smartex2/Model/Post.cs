using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smartex.Model
{
    class Post
    {
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }
        [JsonProperty(PropertyName = "user_id")]
        public int UserID { get; set; }
        [JsonProperty(PropertyName = "event_id")]
        public int EventID { get; set; }
        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }
        [JsonProperty(PropertyName = "creation_date")]
        public string CreationDate { get; set; }

    }
}
