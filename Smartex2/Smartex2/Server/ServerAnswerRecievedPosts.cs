using Newtonsoft.Json;
using Smartex.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Smartex.Server
{
    class ServerAnswerRecievedPosts
    {
        [JsonProperty(PropertyName = "result")]
        public ObservableCollection<Post> PostList { get; set; }
        [JsonProperty(PropertyName = "status")]
        public String Status { get; set; }
    }
}
