using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoinManager.Models
{
    public static class ApiClient
    {
        #region Fields

        private static readonly HttpClient _client = new HttpClient();

        private const string URL_BASE = "";

        #endregion
    }
}
