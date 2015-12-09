using Newtonsoft.Json;
using System.Collections.Generic;

namespace IQueryableTask.Client
{
    public class YahooResponse<T>
    {
        [JsonProperty("Quote")]
        public List<T> Results { get; set; }
    }
}
