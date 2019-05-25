using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smartex.Model
{
    class Event
    {
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "start_date")]
        public string StartDate { get; set; }
        [JsonProperty(PropertyName = "desc")]
        public string Desc { get; set; }
        [JsonProperty(PropertyName = "creation_date")]
        public string CreationDate { get; set; }
        [JsonProperty(PropertyName = "user_id")]
        public int UserID { get; set; }

        public List<Post> Posts { get; set; }
    }
}
