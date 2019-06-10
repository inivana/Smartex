using Newtonsoft.Json;
using Smartex.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smartex.Server
{
    /**
    * Obiekt klasy stworzony na postawie danych przesyłanych z serwera w postaci JSON.
    * Zawiera dane jak i status odpowiedzi, na jego podstawie określane jest czy dane są kompletne.
    */
    class ServerAnswerRecievedUser
    {

        [JsonProperty(PropertyName = "result")]
        public UserPersonalInfo User { get; set; }
        [JsonProperty(PropertyName = "status")]
        public String Status { get; set; }

    }
}
