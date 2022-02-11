using Newtonsoft.Json;
using System.Collections.Generic;

namespace GlencoreWordGame.Models.Response
{
    public class WordResponse : BaseResponse
    {
        [JsonProperty("word")]
        public string Word { get; set; }

        [JsonProperty("phonetic")]
        public string Phonetic { get; set; }

        [JsonProperty("phonetics")]
        public List<Phonetic> Phonetics { get; set; }

        [JsonProperty("origin")]
        public string Origin { get; set; }

        [JsonProperty("meanings")]
        public List<Meaning> Meanings { get; set; }
    }
}
