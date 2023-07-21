using Newtonsoft.Json;

namespace CoinManager.Models
{
    /// <summary>
    /// Represents cryptocurrency as a DTO model.
    /// </summary>
    public class Currency
    {
        #region Properties

        [JsonProperty("id")]
        /// <summary>
        /// Id of the cryptocurrency.
        /// </summary>
        public string Id { get; set; }

        [JsonProperty("rank")]
        /// <summary>
        /// Number that is directly associated with the cryptocurrency marketcap whereas the highest marketcap receives 1.
        /// </summary>
        public int Rank { get; set; }

        [JsonProperty("priceUsd")]
        /// <summary>
        /// Price of the cryptocurrency in some other currency.
        /// </summary>
        public decimal Price { get; set; }

        [JsonProperty("volumeUsd24Hr")]
        /// <summary>
        /// Volume of the cryptocurrency.
        /// </summary>
        public decimal Volume { get; set; }

        [JsonProperty("changePercent24Hr")]
        /// <summary>
        /// The cryptocurrency price change within 24 hours.
        /// </summary>
        public decimal PriceChange { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Currency"/> class.
        /// </summary>
        public Currency()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Currency"/> class with the specified price, volume, and price change parameters.
        /// </summary>
        /// <param name="id"><inheritdoc cref="Id"/></param>
        /// <param name="rank"><inheritdoc cref="Rank"/></param>
        /// <param name="price"><inheritdoc cref="Price"/></param>
        /// <param name="volume"><inheritdoc cref="Volume"/></param>
        /// <param name="priceChange"><inheritdoc cref="PriceChange"/></param>
        public Currency(string id, int rank, decimal price, decimal volume, decimal priceChange)
        {
            Id = id;
            Rank = rank;
            Price = price;
            Volume = volume;
            PriceChange = priceChange;
        }

        #endregion 
    }
}
