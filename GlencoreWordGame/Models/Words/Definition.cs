using Newtonsoft.Json;
using System.Collections.Generic;

namespace GlencoreWordGame.Models
{
    public class Definition
    {
        [JsonProperty("definition")]
        public string wordDefinition { get; set; }

        [JsonProperty("example")]
        public string Example { get; set; }

        [JsonProperty("synonyms")]
        public List<string> Synonyms { get; set; }

        [JsonProperty("antonyms")]
        public List<string> Antonyms { get; set; }
    }
}
