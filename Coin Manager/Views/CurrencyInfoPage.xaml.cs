using CoinManager.Models;
using System;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace CoinManager.Views
{
    /// <summary>
    /// Page that shows the detailed info about cryptocurrency.
    /// </summary>
    public sealed partial class CurrencyInfoPage : Page
    {
        #region Fields

        /// <summary>
        /// Current instance of the <see cref="Currency"/> class that binded to the current <see cref="CurrencyInfoPage"/> instance.
        /// </summary>
        public Currency DisplayedCurrency;

        /// <summary>
        /// Current instance of the <see cref="MarketsCollection"/> class that binded to the current <see cref="CurrencyInfoPage"/> instance.
        /// </summary>
        public MarketsCollection Collection;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyInfoPage"/> class.
        /// </summary>
        public CurrencyInfoPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string id = e.Parameter.ToString();
            DisplayedCurrency = Task.Run(() => ApiClient.GetCurrencyInfo(id)).Result;

            Collection = new MarketsCollection(id);

            ReloadLastRefreshTime();

            base.OnNavigatedTo(e);
        }

        private void ReloadLastRefreshTime() => LastRefreshTime.Text = ($"Last refresh time (in UTC):\n{Collection.FormattedLastRefreshDate}");

        #endregion

        private async void MarketsList_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is MarketsCollection.Market item)
            {
                string url = Task.Run(() => ApiClient.GetExchangeUrl(item.Id)).Result;

                if (string.IsNullOrEmpty(url))
                    return;

                Uri uri = new Uri(url);
                bool success = await Launcher.LaunchUriAsync(uri);

                if (!success)
                {
                    ContentDialog noWifiDialog = new ContentDialog()
                    {
                        Title = "Invalid launch",
                        Content = "Coin Manager could not launch the browser.",
                        CloseButtonText = "Ok"
                    };

                    await noWifiDialog.ShowAsync();
                }
            }
        }
    }
}
