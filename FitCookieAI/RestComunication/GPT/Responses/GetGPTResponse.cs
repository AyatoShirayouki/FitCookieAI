using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace FitCookieAI.RestComunication.GPT.Responses
{
    public class GetGPTResponse
    {
        /*
         {"id":"cmpl-6eq4yDRSAKU7W75uAm3u2HSyPD7g3",
        "object":"text_completion",
        "created":1675191332,
        "model":"text-davinci-003",
        "choices":[{"text":"\n\nThe command for starting Docker is \"docker run\".","index":0,"logprobs":null,"finish_reason":"stop"}],
        "usage":{"prompt_tokens":4,"completion_tokens":12,"total_tokens":16}}
         */
        public string? id { get; set; }
        public string? Object { get; set; }
        public long created { get; set; }
        public string? model { get; set; }
        public List<Choice>? choices { get; set; }
        public Usage? usage { get; set; }


        [JsonProperty(PropertyName = "code")]
        public object? Code { get; set; }

        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public object? Body { get; set; }

        [JsonProperty(PropertyName = "error", NullValueHandling = NullValueHandling.Ignore)]
        public object? Error { get; set; }
        public bool JwtSuccess { get; set; }
        public List<string>? JwtErrors { get; set; }

        public class Choice
        {
            public string? text { get; set; }
            public long index { get; set; }
            public string? logprobs { get; set; }
            public string? finish_reason { get; set; }
        }
        public class Usage
        {
            public long prompt_tokens { get; set; }
            public long completion_tokens { get; set; }
            public long total_tokens { get; set; }
        }
    }
}
