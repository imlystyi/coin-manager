using Newtonsoft.Json;

namespace CoinManager.Models
{
    /// <summary>
    /// Represents brief information ("brief") about cryptocurrency as a data model.
    /// </summary>
    public class BriefCurrency : ICoin
    {
        #region Fields

        [JsonProperty("priceUsd")]
        private readonly decimal? _price;

        #endregion

        #region Properties

        /// <summary>
        /// Identifier of the brief.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Symbol of the brief cryptocurrency.
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// Number that is directly associated with the brief cryptocurrency marketcap whereas the highest marketcap receives 1.
        /// </summary>
        [JsonProperty("rank")]
        public int Rank { get; set; }

        /// <summary>
        /// Price of a brief cryptocurrency.
        /// </summary>
        public string Price => _price != null ? string.Format("{0:N3}$", _price) : "n/a";

        /// <summary>
        /// The cryptocurrency price change within 24 hours.
        /// </summary>
        [JsonProperty("changePercent24Hr")]
        public decimal PriceChangePercent { get; set; }

        #endregion
    }
}