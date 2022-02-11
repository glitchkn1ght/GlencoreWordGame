using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlencoreWordGame.Models
{
    public class Meaning
    {
        [JsonProperty("partOfSpeech")]
        public string PartOfSpeech { get; set; }

        [JsonProperty("definitions")]
        public List<Definition> Definitions { get; set; }
    }

}
