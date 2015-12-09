using Newtonsoft.Json;
using System.Text;

namespace IQueryableTask.Client
{
    public class Quote
    {
        public  string Symbol { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        [JsonProperty]
        private string Date { get; set; }
        [JsonProperty]
        private string Open { get; set; }
        [JsonProperty]
        private string High { get; set; }
        [JsonProperty]
        private string Low { get; set; }
        [JsonProperty]
        private string Close { get; set; }
        [JsonProperty]
        private string Volume { get; set; }

        public override string ToString()
        {
            var delimiter = ",";
            var stringBuilder = new StringBuilder();
            return stringBuilder.Append("Symbol:" + Symbol).Append(delimiter).Append("Date:" + Date).Append(delimiter).Append("Volume:" + Volume).ToString();
        }
    }
}
