using Newtonsoft.Json;
using Smartex.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Smartex.Server
{
    /**
    * Klasa tworzona na postawie danych przesyłanych z serwera w postaci JSON.
    * Zawiera dane jak i status żądania serwera, na jego podstawie określane jest czy dane są kompletne.
    */
    class ServerAnswerRecievedPosts
    {
        [JsonProperty(PropertyName = "result")]
        public ObservableCollection<Post> PostList { get; set; }
        [JsonProperty(PropertyName = "status")]
        public String Status { get; set; }
    }
}
