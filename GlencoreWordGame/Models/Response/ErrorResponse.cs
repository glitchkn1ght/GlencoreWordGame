using Newtonsoft.Json;

namespace GlencoreWordGame.Models.Response
{
    public class ErrorResponse
    {
        public int HttpStatusCode { get; set; }
        
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("resolution")]
        public string Resolution { get; set; }
    }
}
