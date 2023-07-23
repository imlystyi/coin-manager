﻿using Newtonsoft.Json;

namespace CoinManager.Models
{
    /// <summary>
    /// Represents cryptocurrency as a data model.
    /// </summary>
    public class Currency
    {
        #region Fields

        [JsonProperty("priceUsd")]
        private readonly decimal _price;

        [JsonProperty("changePercent24Hr")]
        private readonly decimal _priceChange;

        [JsonProperty("volumeUsd24Hr")]
        private readonly decimal _volume;

        [JsonProperty("supply")]
        private readonly decimal _supply;

        [JsonProperty("maxSupply")]
        private readonly decimal _maxSupply;

        [JsonProperty("marketCapUsd")]
        private readonly decimal _marketCap;

        #endregion

        #region Properties

        /// <summary>
        /// Id of the cryptocurrency.
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
        /// Price of the cryptocurrency in some other currency.
        /// </summary>
        public string Price { get => string.Format("{0:N3}$", _price); }

        /// <summary>
        /// The cryptocurrency price change within 24 hours.
        /// </summary>
        public string PriceChange { get => string.Format("{0:F2}%", _priceChange); }

        /// <summary>
        /// Volume of the cryptocurrency.
        /// </summary>
        public string Volume { get => string.Format("{0:N3}$", _volume); }

        /// <summary>
        /// Available supply for trading.
        /// </summary>
        public string Supply { get => string.Format("{0:N3}$", _supply); }

        /// <summary>
        /// Total quantity of asset issued
        /// </summary>
        public string MaxSupply { get => string.Format("{0:N3}$", _maxSupply); }

        /// <summary>
        /// Total power of the cryptocurrency, that is calculated by multiplication the supply and the price.
        /// </summary>

        public string MarketCap { get => string.Format("{0:N3}$", _marketCap); }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Currency"/> class.
        /// </summary>
        public Currency()
        {
        }

        #endregion 
    }
}
