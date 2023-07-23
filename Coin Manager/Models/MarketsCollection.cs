using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CoinManager.Models
{
    // TODO: Documentation to MarketsCollection
    public class MarketsCollection
    {
        #region Properties

        public ObservableCollection<Market> Container { get; set; }

        #endregion

        #region Constructors

        public MarketsCollection(string id) => LoadAll(id);

        #endregion

        #region Methods

        private void LoadAll(string id)
        {
            JObject jsonObject = Task.Run(() => ApiClient.GetMarkets(id)).Result;
            JArray jsonArray = (JArray)jsonObject["data"];
            List<Market> markets = jsonArray.ToObject<List<Market>>();

            Container = new ObservableCollection<Market>(markets);
        }

        #endregion

        #region Nested classes

        public sealed class Market
        {
            [JsonProperty("exchangeId")]
            public string Id { get; set; }

            [JsonProperty("quoteSymbol")]
            public string QuoteSymbol { get; set; }

            [JsonProperty("priceUsd")]
            public string Price { get; set; }
        }

        #endregion
    }
}
