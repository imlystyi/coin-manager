﻿/* Copyright 2023 imlystyi
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
    /// Represents cryptomarket of a specified cryptocurrency as a data model.
    /// </summary>
    public class Market : ICoin
    {
        #region Fields

        [JsonProperty("priceUsd")]
        private readonly decimal? _price;

        #endregion

        #region Properties

        /// <summary>
        /// Identifier of the cryptomarket.
        /// </summary>
        [JsonProperty("exchangeId")]
        public string Id { get; set; }

        /// <summary>
        /// The price of a specified cryptocurrency.
        /// </summary>
        public string Price => _price != null ? string.Format("{0:N3}$", _price) : "n/a";

        /// <summary>
        /// The quote currency symbol.
        /// </summary>
        [JsonProperty("quoteSymbol")]
        public string QuoteSymbol { get; set; }

        #endregion
    }
}