using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoinManager.Models
{
    /// <summary>
    /// Represents a client for working with the API.
    /// </summary>
    /// <remarks>
    /// For more details about the API, see the <b>CoinCap API 2.0</b> documentation: <see href="https://docs.coincap.io/"/>.
    /// </remarks>
    public static class ApiClient
    {
        #region Fields

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
            JObject jsonObject = await GetJObject(url);

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
            return await GetJObject(url);
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
            return await GetJObject(url);
        }

        /// <summary>
        /// Returns a list of all available rates.
        /// </summary>
        /// <remarks>
        /// After getting, must be converted to some collection.
        /// </remarks>
        /// <returns>Rates list as a <see cref="JObject"/>.</returns>
        public static async Task<JObject> GetRates()
        {
            string url = $"{URL_BASE}/rates";
            return await GetJObject(url);
        }

        /// <summary>
        /// Returns the URL of exchange resource.
        /// </summary>
        /// <param name="id">Exchange resource identifier.</param>
        /// <returns>URL as a <see cref="string"/>.</returns>
        public static async Task<string> GetExchangeUrl(string id)
        {
            HttpClient httpClient = SingletonHttpClient.GetInstance();

            string url = $"{URL_BASE}/exchanges/{id}";
            HttpResponseMessage httpResponse = await httpClient.GetAsync(url);

            if (!httpResponse.IsSuccessStatusCode)
                return string.Empty;

            string jsonString = await httpResponse.Content.ReadAsStringAsync();
            JObject jsonObject = JObject.Parse(jsonString);

            return jsonObject["data"]["exchangeUrl"].ToObject<string>();
        }

        /// <summary>
        /// Converts a basic currency to a quote currency.
        /// </summary>
        /// <param name="baseId">Basic currency identifier.</param>
        /// <param name="quoteId">Quote currency identifier.</param>
        /// <returns>Amount of converted currency as a <see cref="decimal"/>.</returns>
        public static async Task<decimal> DoCurrencyConversion(string baseId, string quoteId)
        {
            // Converting base currency in USD.
            string baseInUsdUrl = $"{URL_BASE}/rates/{baseId}";
            JObject baseInUsdJsonObject = await GetJObject(baseInUsdUrl);

            decimal baseInUsd = (decimal)baseInUsdJsonObject["data"]["rateUsd"];

            // Converting quote currency in USD.
            if (quoteId != "USD")
            {
                string quoteInUsdUrl = $"{URL_BASE}/rates/{quoteId}";
                JObject quoteInUsdJsonObject = await GetJObject(quoteInUsdUrl);

                decimal quoteInUsd = (decimal)quoteInUsdJsonObject["data"]["rateUsd"];

                return baseInUsd / quoteInUsd;
            }
            else
                return baseInUsd;
        }

        private static async Task<JObject> GetJObject(string url)
        {
            HttpClient httpClient = SingletonHttpClient.GetInstance();

            try
            {
                HttpResponseMessage httpResponse = await httpClient.GetAsync(url);
                string jsonString = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.IsSuccessStatusCode || httpResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    try
                    {
                        return JObject.Parse(jsonString);
                    }
                    catch (JsonReaderException)
                    {
                        throw new HttpRequestException("The API sent bad data.");
                    }
                }
                else
                    throw new HttpRequestException("There was a problem sending or processing the request. Try to check your internet connection or wait a minute.");
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException("There was a problem sending or processing the request. Try to check your internet connection or wait a minute.");
            }
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
