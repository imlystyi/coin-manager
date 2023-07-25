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
using System.Threading.Tasks;

namespace CoinManager.ViewModels
{
    /// <summary>
    /// Collection that stores the <see cref="Market"/> objects.
    /// </summary>
    public class MarketsCollection : CoinCollection<Market>
    {
        #region Fields

        readonly string _id;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MarketsCollection"/> class.
        /// </summary>
        public MarketsCollection(string id)
        {
            _id = id;
            LoadAll(_id);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the container.
        /// </summary>
        public void Update() => LoadAll(_id);

        private void LoadAll(string id)
        {
            JObject jsonObject = Task.Run(() => ApiClient.GetMarkets(id)).Result;
            JArray jsonArray = (JArray)jsonObject["data"];
            timestamp = (long)jsonObject["timestamp"];

            List<Market> markets = jsonArray.ToObject<List<Market>>();

            Container = new ObservableCollection<Market>(markets);
        }

        #endregion
    }
}
