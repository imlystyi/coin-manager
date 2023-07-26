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
using CoinManager.ViewModels;
using System;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml;
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
            try
            {
                InitializeComponent();
            }
            catch (Exception exception)
            {
                ContentDialog errorDialog = new ContentDialog()
                {
                    Title = "Error",
                    Content = exception.Message,
                    CloseButtonText = "Ok"
                };

                Task.Run(errorDialog.ShowAsync);
            }
        }

        #endregion

        #region Methods

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                string id = e.Parameter.ToString();
                DisplayedCurrency = Task.Run(() => ApiClient.GetCurrency(id)).Result;

                Collection = new MarketsCollection(id);

                ReloadLastRefreshTime();
            }
            catch (Exception exception)
            {
                ContentDialog errorDialog = new ContentDialog()
                {
                    Title = "Error",
                    Content = exception.Message,
                    CloseButtonText = "Ok"
                };

                Task.Run(errorDialog.ShowAsync);
            }

            base.OnNavigatedTo(e);
        }

        private void ReloadLastRefreshTime() => LastRefreshTimeValue.Text = ($"{Collection.FormattedLastRefreshDate} (UTC)");

        #endregion

        #region Event handlers 

        private void BackButton_Click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(MainPage));

        private void FastConversionButton_Click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(ConversionPage));

        private async void MarketsList_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (e.ClickedItem is Market item)
                {
                    string url = Task.Run(() => ApiClient.GetExchangeUrl(item.Id)).Result;

                    if (string.IsNullOrEmpty(url))
                        return;

                    Uri uri = new Uri(url);
                    bool success = await Launcher.LaunchUriAsync(uri);

                    if (!success)
                    {
                        ContentDialog errorDialog = new ContentDialog()
                        {
                            Title = "Invalid launch",
                            Content = "Coin Manager could not launch the browser.",
                            CloseButtonText = "Ok"
                        };

                        await errorDialog.ShowAsync();
                    }
                }
            }
            catch (Exception exception)
            {
                ContentDialog errorDialog = new ContentDialog()
                {
                    Title = "Error",
                    Content = exception.Message,
                    CloseButtonText = "Ok"
                };

                await errorDialog.ShowAsync();
            }
        }
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Collection.Update();
                ReloadLastRefreshTime();
            }
            catch (Exception exception)
            {
                ContentDialog errorDialog = new ContentDialog()
                {
                    Title = "Error",
                    Content = exception.Message,
                    CloseButtonText = "Ok"
                };

                Task.Run(errorDialog.ShowAsync);
            }
        }

        #endregion
    }
}
