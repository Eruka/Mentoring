using System.Net.Http;
using System.Collections;
using System;
using Newtonsoft.Json.Linq;

namespace IQueryableTask.Client
{
    public class LinqToYahooClient
    {
        private readonly string exampleUrl = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.historicaldata%20where%20symbol%20%3D%20%22EPAM%22%20and%20startDate%20%3D%20%222015-09-22%22%20and%20endDate%20%3D%20%222015-09-25%22&format=json&diagnostics=true&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&callback=";
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
            return new Uri(exampleUrl);

            var encodedQuery = prefixUrl + Uri.EscapeDataString(queryPart + query) + postfixUrl;
            return new Uri(prefixUrl + encodedQuery + postfixUrl);
        }

    }
}
