using System.Net.Http;
using System.Collections;
using System;
using Newtonsoft.Json.Linq;

namespace IQueryableTask.Client
{
    public class LinqToYahooClient
    {
        private readonly string prefixUrl = "https://query.yahooapis.com/v1/public/yql?q=";
        private readonly string queryPart = "select * from yahoo.finance.historicaldata where ";
        private readonly string postfixUrl = "&format=json&diagnostics=true&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&callback=";

        public IEnumerable Search<T>(string query)
        {
            HttpClient client = new HttpClient();

            Uri request = GenerateRequestUrl(query);
            var resultString = client.GetStringAsync(request).Result;

            var data = JObject.Parse(resultString).SelectToken("query").SelectToken("results").ToObject<YahooResponse<T>>() ;

            return data.Results;
        }

        private Uri GenerateRequestUrl(string query)
        {
            var encodedQuery = prefixUrl + EscapeUrl(queryPart + query) + postfixUrl;
            return new Uri(encodedQuery);
        }

        private string EscapeUrl(string raw)
        {
            return raw.Replace(" ", "%20").Replace("\"", "%22").Replace("=", "%3D");
        }

    }
}
