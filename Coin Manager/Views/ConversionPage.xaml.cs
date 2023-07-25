using CoinManager.Models;
using CoinManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CoinManager.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
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
