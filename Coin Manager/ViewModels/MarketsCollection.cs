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
