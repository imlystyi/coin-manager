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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CoinManager.Views
{
    /// <summary>
    /// Page that allows to convert one currency to another.
    /// </summary>
    public sealed partial class ConversionPage : Page
    {
        #region Fields

        /// <summary>
        /// Current instance of the <see cref="RatesCollection"/> class that binded to the basic currency and the current <see cref="ConversionPage"/> instance.
        /// </summary>
        public RatesCollection BasicCollection;

        /// <summary>
        /// Current instance of the <see cref="RatesCollection"/> class that binded to the quote currency and the current <see cref="ConversionPage"/> instance.
        /// </summary>
        public RatesCollection QuoteCollection;

        private decimal _basicAmount;
        private decimal _quoteAmount;

        private string _basicId;
        private string _quoteId;
        private string _basicSymbol;
        private string _quoteSymbol;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConversionPage"/> class.
        /// </summary>
        public ConversionPage()
        {
            try
            {
                InitializeComponent();

                BasicCollection = new RatesCollection();
                QuoteCollection = new RatesCollection();
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

        private void Convert()
        {
            try
            {
                _quoteAmount = Task.Run(() => ApiClient.DoCurrencyConversion(_basicId, _quoteId)).Result * _basicAmount;
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

        #region Event handlers

        private void AmountEntryButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (decimal.TryParse(AmountEntryBox.Text, out decimal outAmount))
                    _basicAmount = outAmount;

                if (CheckValues())
                {
                    Convert();

                    BasicCurrencyAmount.Text = _basicAmount.ToString("N") + $" {_basicSymbol}";
                    QuoteCurrencyAmount.Text = _quoteAmount.ToString("N") + $" {_basicSymbol}";
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

                Task.Run(errorDialog.ShowAsync);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(MainPage));

        private void BasicCurrenciesList_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (e.ClickedItem is Rate item)
                {
                    _basicId = item.Id;
                    _basicSymbol = item.Symbol;
                }

                AmountEntryButton_Click(null, null);

                if (CheckValues())
                {
                    Convert();

                    BasicCurrencyAmount.Text = _basicAmount.ToString("N") + $" {_basicSymbol}";
                    QuoteCurrencyAmount.Text = _quoteAmount.ToString("N") + $" {_quoteSymbol}";
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

                Task.Run(errorDialog.ShowAsync);
            }
        }

        private void BasicCurrencySearchButton_Click(object sender, RoutedEventArgs e)
        {
            string mark = BasicCurrencySearchBox.Text;

            try
            {
                BasicCollection.FilterByMark(mark);
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

        private bool CheckValues() => _basicAmount != default && _quoteId != default && _basicId != default;

        private void QuoteCurrenciesList_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (e.ClickedItem is Rate item)
                {
                    _quoteId = item.Id;
                    _quoteSymbol = item.Symbol;
                }

                AmountEntryButton_Click(null, null);

                if (CheckValues())
                {
                    Convert();

                    BasicCurrencyAmount.Text = _basicAmount.ToString("N") + $" {_basicSymbol}";
                    QuoteCurrencyAmount.Text = _quoteAmount.ToString("N") + $" {_quoteSymbol}";
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

                Task.Run(errorDialog.ShowAsync);
            }
        }

        private void QuoteCurrencySearchButton_Click(object sender, RoutedEventArgs e)
        {
            string mark = QuoteCurrencySearchBox.Text;

            try
            {
                QuoteCollection.FilterByMark(mark);
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
