using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace CoinManager.Models
{
    // TODO: Documentation to MarketsCollection
    public class MarketsCollection
    {
        #region Fields

        public event PropertyChangedEventHandler PropertyChanged;

        private long _timestamp;

        public ObservableCollection<Market> _container;

        #endregion

        #region Properties

        public ObservableCollection<Market> Container 
        {
            get => _container;
            set
            {
                _container = value;
                OnPropertyChanged(nameof(Container));
            }
        }

        public string FormattedLastRefreshDate => TimestampToDateTime(_timestamp).ToString("dd MMMM yyyy, HH:mm:ss");

        #endregion

        #region Constructors

        public MarketsCollection(string id) => LoadAll(id);

        #endregion

        #region Methods

        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void LoadAll(string id)
        {
            JObject jsonObject = Task.Run(() => ApiClient.GetMarkets(id)).Result;
            JArray jsonArray = (JArray)jsonObject["data"];
            List<Market> markets = jsonArray.ToObject<List<Market>>();

            Container = new ObservableCollection<Market>(markets);
        }

        private DateTime TimestampToDateTime(long timestamp)
        {
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return unixEpoch.AddMilliseconds(timestamp);
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
            public decimal Price { get; set; }
        }

        #endregion
    }
}
