using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoinManager.Models
{
    // TODO: Documentation to ApiClient
    public static class ApiClient
    {
        #region Fields

        private static readonly HttpClient _client = SingletonClient.Get();

        private const string URL_BASE = "https://api.coincap.io/v2";

        #endregion

        #region Methods

        public static async Task<string> GetExchangeUrl(string id)
        {
            string url = $"{URL_BASE}/exchanges/{id}";
            HttpResponseMessage httpResponse = await _client.GetAsync(url);

            if (!httpResponse.IsSuccessStatusCode)
                return string.Empty;

            string jsonString = await httpResponse.Content.ReadAsStringAsync();
            JObject jsonObject = JObject.Parse(jsonString);

            return jsonObject["data"]["exchangeUrl"].ToObject<string>();
        }

        public static async Task<JObject> GetCurrencies()
        {
            string url = $"{URL_BASE}/assets";
            HttpResponseMessage httpResponse = await _client.GetAsync(url);

            string jsonString = await httpResponse.Content.ReadAsStringAsync();
            return JObject.Parse(jsonString);
        }

        public static async Task<JObject> GetMarkets(string id)
        {
            string url = $"{URL_BASE}/assets/{id}/markets";
            HttpResponseMessage httpResponse = await _client.GetAsync(url);

            string jsonString = await httpResponse.Content.ReadAsStringAsync();
            return JObject.Parse(jsonString);
        }

        public static async Task<Currency> GetCurrencyInfo(string id)
        {
            string url = $"{URL_BASE}/assets/{id}";
            HttpResponseMessage httpResponse = await _client.GetAsync(url);

            string jsonString = await httpResponse.Content.ReadAsStringAsync();
            JObject jsonObject = JObject.Parse(jsonString);

            return jsonObject["data"].ToObject<Currency>();
        }

        public static async Task<decimal> DoCurrencyConversion(string baseId, string quoteId)
        {
            // Converting base currency in USD.
            string biuUrl = $"{URL_BASE}/rates/{baseId}";
            HttpResponseMessage biuHttpResponse = await _client.GetAsync(biuUrl);

            string biuJsonString = await biuHttpResponse.Content.ReadAsStringAsync();
            JObject biuJsonObject = JObject.Parse(biuJsonString);

            decimal baseInUsd = (decimal)biuJsonObject["data"]["rateUsd"];

            // Converting quote currency in USD.
            if (quoteId != "USD")
            {
                string qiuUrl = $"{URL_BASE}/rates/{quoteId}";
                HttpResponseMessage qiuHttpResponse = await _client.GetAsync(qiuUrl);

                string qiuJsonString = await qiuHttpResponse.Content.ReadAsStringAsync();
                JObject qiuJsonObject = JObject.Parse(qiuJsonString);

                decimal quoteInUsd = (decimal)qiuJsonObject["data"]["rateUsd"];

                return baseInUsd / quoteInUsd;
            }
            else
                return baseInUsd;
        }

        #endregion

        #region Nested classes

        private sealed class SingletonClient
        {
            private static HttpClient _singletonClient;

            public static HttpClient Get() => _singletonClient ?? (_singletonClient = new HttpClient());
        }

        #endregion
    }
}
