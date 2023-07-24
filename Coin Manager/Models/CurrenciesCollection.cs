using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

// TODO: Documentation to CurrenciesCollection
namespace CoinManager.Models
{
    public class CurrenciesCollection : INotifyPropertyChanged
    {
        #region Fields

        public event PropertyChangedEventHandler PropertyChanged;

        private long _timestamp;

        private ObservableCollection<BriefCurrency> _container;

        #endregion

        #region Properties

        public ObservableCollection<BriefCurrency> Container
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

        public CurrenciesCollection() => Container = GetCurrenciesAsObservableCollection();        

        #endregion

        #region Methods

        public void FindByMark(string mark)
        {
            mark = mark.ToLower();

            if (string.IsNullOrEmpty(mark))
                Update();
            else
                Container = new ObservableCollection<BriefCurrency>(GetCurrenciesAsIEnumerable().Where(cc => cc.Id.ToLower().Contains(mark) || cc.Symbol.ToLower().Contains(mark)).Take(10));
        }

        public void Update() => Container = GetCurrenciesAsObservableCollection();

        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private ObservableCollection<BriefCurrency> GetCurrenciesAsObservableCollection() => new ObservableCollection<BriefCurrency>(GetCurrenciesAsIEnumerable().Take(10));

        // TODO: Edit "FormattedLastRefreshDate"
        private IEnumerable<BriefCurrency> GetCurrenciesAsIEnumerable()
        {
            JObject jsonObject = Task.Run(ApiClient.GetCurrencies).Result;

            JArray jsonArray = (JArray)jsonObject["data"];
            _timestamp = (long)jsonObject["timestamp"];

            return jsonArray.ToObject<IEnumerable<BriefCurrency>>();
        }

        private DateTime TimestampToDateTime(long timestamp)
        {
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return unixEpoch.AddMilliseconds(timestamp);
        }

        #endregion

        #region Nested classes

        public sealed class BriefCurrency
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("symbol")]
            public string Symbol { get; set; }

            [JsonProperty("rank")]
            public int Rank { get; set; }

            [JsonProperty("priceUsd")]
            public decimal Price { get; set; }

            [JsonProperty("changePercent24Hr")]
            public decimal PriceChangePercent { get; set; }
        }

        #endregion
    }
}
