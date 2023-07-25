using CoinManager.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CoinManager.ViewModels
{
    /// <summary>
    /// Collection that stores the <see cref="Rate"/> objects.
    /// </summary>
    public class RatesCollection : CoinCollection<Rate>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RatesCollection"/> class.
        /// </summary>
        public RatesCollection() => Container = GetCurrenciesAsObservableCollection();

        #endregion

        #region Methods

        /// <summary>
        /// Filters elements in the container by specified mark.
        /// </summary>
        /// <param name="mark">Find mark.</param>
        public void FilterByMark(string mark)
        {
            mark = mark.ToLower();

            if (string.IsNullOrEmpty(mark))
                Update();
            else
                Container = new ObservableCollection<Rate>(GetRatesAsIEnumerable().Where(cc =>
                cc.Id.ToLower().Contains(mark) ||
                cc.Symbol.ToLower().Contains(mark)).Take(10));
        }

        /// <summary>
        /// Updates the container.
        /// </summary>
        public void Update() => Container = GetCurrenciesAsObservableCollection();

        private IEnumerable<Rate> GetRatesAsIEnumerable()
        {
            JObject jsonObject = Task.Run(ApiClient.GetRates).Result;
            JArray jsonArray = (JArray)jsonObject["data"];
            timestamp = (long)jsonObject["timestamp"];

            return jsonArray.ToObject<IEnumerable<Rate>>();
        }
        private ObservableCollection<Rate> GetCurrenciesAsObservableCollection() => new ObservableCollection<Rate>(GetRatesAsIEnumerable().Take(10));

        #endregion
    }
}
