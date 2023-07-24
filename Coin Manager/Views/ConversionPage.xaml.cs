using CoinManager.Models;
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
        /// 
        /// </summary>
        public CurrenciesCollection BasicCollection;

        /// <summary>
        /// 
        /// </summary>
        public CurrenciesCollection QuoteCollection;

        private decimal _basicAmount;
        private decimal _quoteAmount;

        private string _basicId;
        private string _quoteId;
        private string _basicSymbol;
        private string _quoteSymbol;

        #endregion

        #region Constructors

        public ConversionPage()
        {
            InitializeComponent();

            BasicCollection = new CurrenciesCollection();
            QuoteCollection = new CurrenciesCollection();
        }

        #endregion

        private void AmountEntryButton_Click(object sender, RoutedEventArgs e)
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

        private void BasicCurrencySearchButton_Click(object sender, RoutedEventArgs e)
        {
            string mark = BasicCurrencySearchBox.Text;
            BasicCollection.FindByMark(mark);
        }

        private void QuoteCurrencySearchButton_Click(object sender, RoutedEventArgs e)
        {
            string mark = QuoteCurrencySearchBox.Text;
            QuoteCollection.FindByMark(mark);
        }

        private bool CheckValues() => _basicAmount != default && _quoteId != default && _basicId != default;

        private void QuoteCurrenciesList_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is CurrenciesCollection.BriefCurrency item)
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

        private void BasicCurrenciesList_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is CurrenciesCollection.BriefCurrency item)
            {
                _basicId = item.Id;
                _basicSymbol = item.Symbol;
            }

            AmountEntryButton_Click(null, null);

            if (CheckValues())
            {
                Convert();

                BasicCurrencyAmount.Text = _basicAmount.ToString("N") + $" {_basicSymbol}";
                QuoteCurrencyAmount.Text = _quoteAmount.ToString("N") + $" {_basicSymbol}";
            }
           
        }

        private void Convert()
        {
            _quoteAmount = Task.Run(() => ApiClient.DoCurrencyConversion(_basicId, _quoteId)).Result;
        }
    }
}
