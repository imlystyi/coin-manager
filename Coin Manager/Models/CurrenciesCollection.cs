using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        public ObservableCollection<BriefCurrency> Currencies { get; set; }

        #endregion

        #region Constructors

        public CurrenciesCollection() => LoadAll();

        #endregion

        #region Methods

        public void FindByMark(string mark)
        {
            if (string.IsNullOrEmpty(mark))
                LoadAll();
            else
                Currencies = new ObservableCollection<BriefCurrency>(Currencies.Where(ii => ii.Id.Contains(mark) || ii.Symbol.Contains(mark)).Take(10));
        }

        private void LoadAll()
        {
            JArray jsonArray = Task.Run(ApiClient.GetCurrenciesJArray).Result;
            List<BriefCurrency> currencies = jsonArray.ToObject<List<BriefCurrency>>();

            Currencies = new ObservableCollection<BriefCurrency>(currencies.Take(10));
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
