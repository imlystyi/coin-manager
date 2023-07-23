using CoinManager.Models;
using System.Threading.Tasks;
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
        /// Current instance of <see cref="Currency"/> class that binded to current <see cref="CurrencyInfoPage"/> instance.
        /// </summary>
        public Currency DisplayedCurrency;

        /// <summary>
        /// Current instance of <see cref="MarketsCollection"/> class that binded to current <see cref="CurrencyInfoPage"/> instance.
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

            base.OnNavigatedTo(e);
        }

        #endregion
    }
}
