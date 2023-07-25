namespace CoinManager.Models
{
    /// <summary>
    /// Summarizes objects of relations on the cryptocurrency market.
    /// </summary>
    public interface ICoin
    {
        /// <summary>
        /// Identifier of the <see cref="ICoin"/> object.
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// The price of the <see cref="ICoin"/> object.
        /// </summary>
        string Price { get; }
    }
}
