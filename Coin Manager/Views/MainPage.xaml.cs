using CoinManager.Models;
using CoinManager.Views;
using System;
using System.Collections.Generic;
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

namespace CoinManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public CurrenciesCollection Collection { get; }

        public MainPage()
        {
            Collection = new CurrenciesCollection();
            InitializeComponent();
            ReloadLastRefreshTime();
        }

        private void ReloadLastRefreshTime() => LastRefreshTime.Text = ($"Last refresh time (in UTC):\n{Collection.FormattedLastRefreshDate}");
        private void CurrenciesList_ItemClicked(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is CurrenciesCollection.BriefCurrency item)
                Frame.Navigate(typeof(CurrencyInfoPage), item.Id);
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Collection.Update();
            ReloadLastRefreshTime();
        }

        private void FastConversionButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string mark = SearchBox.Text;

            Collection.FindByMark(mark);

            ReloadLastRefreshTime();
        }
    }
}
