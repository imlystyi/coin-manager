/* Copyright 2023 imlystyi
* Licensed under the Apache License, Version 2.0 (the "License"); 
* you may not use this file except in compliance with the License. 
* You may obtain a copy of the License at 
* http://www.apache.org/licenses/LICENSE-2.0
* Unless required by applicable law or agreed to in writing, 
* software distributed under the License is distributed on an "AS IS" 
* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express 
* or implied. See the License for the specific language governing 
* permissions and limitations under the License. */
using CoinManager.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CoinManager.ViewModels
{
    /// <summary>
    /// Collection that stores the <see cref="Currency"/> objects.
    /// </summary>
    public class CurrenciesCollection : CoinCollection<BriefCurrency>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrenciesCollection"/> class.
        /// </summary>
        public CurrenciesCollection() => Container = GetCurrenciesAsObservableCollection();

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
                Container = new ObservableCollection<BriefCurrency>(GetCurrenciesAsIEnumerable().Where(cc =>
                cc.Id.ToLower().Contains(mark) ||
                cc.Symbol.ToLower().Contains(mark)).Take(10));
        }

        /// <summary>
        /// Updates the container.
        /// </summary>
        public void Update() => Container = GetCurrenciesAsObservableCollection();

        private ObservableCollection<BriefCurrency> GetCurrenciesAsObservableCollection() => new ObservableCollection<BriefCurrency>(GetCurrenciesAsIEnumerable().Take(10));

        private IEnumerable<BriefCurrency> GetCurrenciesAsIEnumerable()
        {
            JObject jsonObject = Task.Run(ApiClient.GetCurrencies).Result;
            JArray jsonArray = (JArray)jsonObject["data"];
            timestamp = (long)jsonObject["timestamp"];

            return jsonArray.ToObject<IEnumerable<BriefCurrency>>();
        }

        #endregion
    }
}
