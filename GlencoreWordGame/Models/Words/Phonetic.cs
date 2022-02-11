using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlencoreWordGame.Models
{
    public class Phonetic
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("audio")]
        public string Audio { get; set; }
    }
}
