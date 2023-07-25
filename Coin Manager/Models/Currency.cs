using Newtonsoft.Json;

namespace CoinManager.Models
{
    /// <summary>
    /// Represents cryptocurrency as a data model.
    /// </summary>
    public class Currency : ICoin
    {
        #region Fields

        [JsonProperty("priceUsd")]
        private readonly decimal? _price;

        [JsonProperty("changePercent24Hr")]
        private readonly decimal? _priceChange;

        [JsonProperty("volumeUsd24Hr")]
        private readonly decimal? _volume;

        [JsonProperty("supply")]
        private readonly decimal? _supply;

        [JsonProperty("maxSupply")]
        private readonly decimal? _maxSupply;

        [JsonProperty("marketCapUsd")]
        private readonly decimal? _marketCap;

        #endregion

        #region Properties

        /// <summary>
        /// Identifier of the cryptocurrency.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Symbol of the cryptocurrency.
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// Number that is directly associated with the cryptocurrency marketcap whereas the highest marketcap receives 1.
        /// </summary>
        [JsonProperty("rank")]
        public int Rank { get; set; }

        /// <summary>
        /// The price of the cryptocurrency.
        /// </summary>
        public string Price => (_price != null) ? string.Format("{0:N3}$", _price) : "n/a";

        /// <summary>
        /// The cryptocurrency price change within 24 hours.
        /// </summary>
        public string PriceChange => (_priceChange != null) ? string.Format("{0:F2}%", _priceChange) : "n/a";

        /// <summary>
        /// The volume of the cryptocurrency.
        /// </summary>
        public string Volume => (_volume != null) ? string.Format("{0:N3}$", _volume) : "n/a";

        /// <summary>
        /// Available supply for trading.
        /// </summary>
        public string Supply => (_supply != null) ? string.Format("{0:N3}$", _supply) : "n/a";

        /// <summary>
        /// Total quantity of asset issued
        /// </summary>
        public string MaxSupply => (_maxSupply != null) ? string.Format("{0:N3}$", _maxSupply) : "n/a";

        /// <summary>
        /// Total power of the cryptocurrency, that is calculated by multiplication the supply and the price.
        /// </summary>
        public string MarketCap => (_marketCap != null) ? string.Format("{0:N3}$", _marketCap) : "n/a";

        #endregion
    }
}
