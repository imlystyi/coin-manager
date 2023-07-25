using Newtonsoft.Json;

namespace CoinManager.Models
{
    /// <summary>
    /// Represents cryptomarket of a specified cryptocurrency as a data model.
    /// </summary>
    public class Market : ICoin
    {
        #region Fields

        [JsonProperty("priceUsd")]
        private readonly decimal? _price;

        #endregion

        #region Properties

        /// <summary>
        /// Identifier of the cryptomarket.
        /// </summary>
        [JsonProperty("exchangeId")]
        public string Id { get; set; }

        /// <summary>
        /// The price of a specified cryptocurrency.
        /// </summary>
        public string Price => _price != null ? string.Format("{0:N3}$", _price) : "n/a";

        /// <summary>
        /// The quote currency symbol.
        /// </summary>
        [JsonProperty("quoteSymbol")]
        public string QuoteSymbol { get; set; }

        #endregion
    }
}