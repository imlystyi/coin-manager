using Newtonsoft.Json;

namespace CoinManager.Models
{
    /// <summary>
    /// Represents cryptocurrency rate as a data model.
    /// </summary>
    public class Rate : ICoin
    {
        #region Fields

        [JsonProperty("rateUsd")]
        private readonly decimal? _price;

        #endregion

        #region Properties

        /// <summary>
        /// Identifier of the rate.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Symbol of the rate cryptocurrency.
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// The price of the rate cryptocurrency.
        /// </summary>
        public string Price => _price != null ? string.Format("{0:N3}$", _price) : "n/a";

        #endregion
    }
}
