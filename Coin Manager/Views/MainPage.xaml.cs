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
using CoinManager.Views;
using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CoinManager
{
    /// <summary>
    /// Main page that shows the top 10 currencies by their rank and contains root controls.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Fields

        /// <summary>
        /// Current instance of the <see cref="CurrenciesCollection"/> class that binded to the current <see cref="MainPage"/> instance.
        /// </summary>
        public CurrenciesCollection Collection;

        #endregion

        #region Contructors 

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            try
            {
                Collection = new CurrenciesCollection();

                InitializeComponent();

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

        #region Methods

        private void ReloadLastRefreshTime() => LastRefreshTime.Text = ($"(UTC):\n{Collection.FormattedLastRefreshDate}");

        #endregion

        #region Event handlers

        private void CurrenciesList_ItemClicked(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is BriefCurrency item)
                Frame.Navigate(typeof(CurrencyInfoPage), item.Id);
        }

        private void FastConversionButton_Click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(ConversionPage));

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

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string mark = CurrencySearchBox.Text;
                Collection.FilterByMark(mark);

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
