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
using Newtonsoft.Json;

namespace CoinManager.Models
{
    /// <summary>
    /// Represents brief information ("brief") about cryptocurrency as a data model.
    /// </summary>
    public class BriefCurrency : ICoin
    {
        #region Fields

        [JsonProperty("priceUsd")]
        private readonly decimal? _price;

        [JsonProperty("changePercent24Hr")]
        private readonly decimal? _priceChangePercent;
        #endregion

        #region Properties

        /// <summary>
        /// Identifier of the brief.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Symbol of the brief cryptocurrency.
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// Number that is directly associated with the brief cryptocurrency marketcap whereas the highest marketcap receives 1.
        /// </summary>
        [JsonProperty("rank")]
        public int Rank { get; set; }

        /// <summary>
        /// Price of a brief cryptocurrency.
        /// </summary>
        public string Price => _price != null ? string.Format("{0:N3}$", _price) : "n/a";

        /// <summary>
        /// The cryptocurrency price change within 24 hours.
        /// </summary>
        public string PriceChangePercent => _price != null ? string.Format("{0:F4}%", _priceChangePercent) : "n/a";

        #endregion
    }
}