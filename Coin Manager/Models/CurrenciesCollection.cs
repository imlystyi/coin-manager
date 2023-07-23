using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

// TODO: Documentation to CurrenciesCollection
namespace CoinManager.Models
{
    public class CurrenciesCollection
    {
        #region Property

        public ObservableCollection<BriefCurrency> Container { get; set; }

        public string FormattedLastRefreshDate { get; set; }

        #endregion

        #region Constructors

        public CurrenciesCollection() => LoadAll();

        #endregion

        #region Methods

        public void FindByMark(string mark)
        {
            if (string.IsNullOrEmpty(mark))
                Update();
            else
                Container = new ObservableCollection<BriefCurrency>(Container.Where(ii => ii.Id.Contains(mark) || ii.Symbol.Contains(mark)).Take(10));
        }

        public void Update()
        {
            LoadAll();
        }

        private void LoadAll()
        {
            JObject jsonObject = Task.Run(ApiClient.GetCurrencies).Result;

            JArray jsonArray = (JArray)jsonObject["data"];
            List<BriefCurrency> currencies = jsonArray.ToObject<List<BriefCurrency>>();

            Container = new ObservableCollection<BriefCurrency>(currencies.Take(10));
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
