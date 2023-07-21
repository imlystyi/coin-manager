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

        public ObservableCollection<Market> Markets { get; set; }

        #endregion

        #region Constructors

        public MarketsCollection(string id) => LoadAll(id);

        #endregion

        #region Methods

        private void LoadAll(string id)
        {
            JArray jsonArray = Task.Run(() => ApiClient.GetMarketsJArray(id)).Result;
            List<Market> markets = jsonArray.ToObject<List<Market>>();

            Markets = new ObservableCollection<Market>(markets);
        }

        #endregion

        #region Nested classes

        public sealed class Market
        {
            [JsonProperty("exchangeId")]
            public string Id;

            [JsonProperty("quoteSymbol")]
            public string QuoteSymbol;

            [JsonProperty("priceUsd")]
            public string Price;
        }

        #endregion
    }
}
