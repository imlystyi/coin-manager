using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoinManager.Models
{
    // TODO: Documentation to ApiClient
    /// <summary>
    /// Represents a client for working with the API.
    /// </summary>
    /// <remarks>
    /// For more details about the API, see the <b>CoinCap API 2.0</b> documentation: <see href="https://docs.coincap.io/"/>.
    /// </remarks>
    public static class ApiClient
    {
        #region Fields

        private static readonly HttpClient _client = SingletonHttpClient.GetInstance();

        private const string URL_BASE = "https://api.coincap.io/v2";

        #endregion

        #region Methods

        /// <summary>
        /// Returns information about a specified cryptocurrency.
        /// </summary>
        /// <param name="id">Cryptocurrency identifier.</param>
        /// <returns>Cryptocurrency as a <see cref="Currency"/>.</returns>
        public static async Task<Currency> GetCurrency(string id)
        {
            string url = $"{URL_BASE}/assets/{id}";
            HttpResponseMessage httpResponse = await _client.GetAsync(url);

            string jsonString = await httpResponse.Content.ReadAsStringAsync();
            JObject jsonObject = JObject.Parse(jsonString);

            return jsonObject["data"].ToObject<Currency>();
        }

        /// <summary>
        /// Returns a list of all available cryptocurrencies.
        /// </summary>
        /// <remarks>
        /// After getting, must be converted to some collection.
        /// </remarks>
        /// <returns>Cryptocurrencies list as a <see cref="JObject"/>.</returns>
        public static async Task<JObject> GetCurrencies()
        {
            string url = $"{URL_BASE}/assets";
            HttpResponseMessage httpResponse = await _client.GetAsync(url);

            string jsonString = await httpResponse.Content.ReadAsStringAsync();
            return JObject.Parse(jsonString);
        }

        /// <summary>
        /// Returns a list of markets in which a specified cryptocurrency can be bought.
        /// </summary>
        /// <remarks>
        /// After getting, must be converted to some collection.
        /// </remarks>
        /// <param name="id">Cryptocurrency identifier.</param>
        /// <returns>Cryptocurrencies list as a <see cref="JObject"/>.</returns>
        public static async Task<JObject> GetMarkets(string id)
        {
            string url = $"{URL_BASE}/assets/{id}/markets";
            HttpResponseMessage httpResponse = await _client.GetAsync(url);

            string jsonString = await httpResponse.Content.ReadAsStringAsync();
            return JObject.Parse(jsonString);
        }

        /// <summary>
        /// Returns the URL of exchange resource.
        /// </summary>
        /// <param name="id">Exchange resource identifier.</param>
        /// <returns>URL as a <see cref="string"/>.</returns>
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

        /// <summary>
        /// Converts a basic cryptocurrency to a quote cryptocurrency.
        /// </summary>
        /// <param name="baseId">Basic cryptocurrency identifier.</param>
        /// <param name="quoteId">Quote cryptocurrency identifier.</param>
        /// <returns>Amount of converted cryptocurrency as a <see cref="decimal"/>.</returns>
        public static async Task<decimal> DoCurrencyConversion(string baseId, string quoteId)
        {
            // Converting base currency in USD.
            string baseInUsdUrl = $"{URL_BASE}/rates/{baseId}";
            HttpResponseMessage biuHttpResponse = await _client.GetAsync(baseInUsdUrl);

            string baseInUsdJsonString = await biuHttpResponse.Content.ReadAsStringAsync();
            JObject baseInUsdJsonObject = JObject.Parse(baseInUsdJsonString);

            decimal baseInUsd = (decimal)baseInUsdJsonObject["data"]["rateUsd"];

            // Converting quote currency in USD.
            if (quoteId != "USD")
            {
                string quoteInUsdUrl = $"{URL_BASE}/rates/{quoteId}";
                HttpResponseMessage quoteInUsdHttpResponse = await _client.GetAsync(quoteInUsdUrl);

                string quoteInUsdJsonString = await quoteInUsdHttpResponse.Content.ReadAsStringAsync();
                JObject qiuJsonObject = JObject.Parse(quoteInUsdJsonString);

                decimal quoteInUsd = (decimal)qiuJsonObject["data"]["rateUsd"];

                return baseInUsd / quoteInUsd;
            }
            else
                return baseInUsd;
        }

        #endregion

        #region Nested classes

        private sealed class SingletonHttpClient
        {
            private static HttpClient _singletonHttpClient;

            public static HttpClient GetInstance() => _singletonHttpClient ?? (_singletonHttpClient = new HttpClient());
        }

        #endregion
    }
}
