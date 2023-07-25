using CoinManager.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CoinManager.ViewModels
{
    /// <summary>
    /// Abstract collection from which collections related to cryptocurrencies are built.
    /// </summary>
    /// <typeparam name="T">An object that implements the <see cref="ICoin"/> interface.</typeparam>
    public abstract class CoinCollection<T> : INotifyPropertyChanged where T : ICoin
    {
        #region Fields

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<T> _container;

        /// <summary>
        /// The collection last update time in milliseconds.
        /// </summary>
        /// <remarks>Given in UNIX format.</remarks>
        public long timestamp;

        #endregion

        #region Properties

        /// <summary>
        /// A dynamic container that stores items and sends a signal when there are updated.
        /// </summary>
        public ObservableCollection<T> Container
        {
            get => _container;
            set
            {
                _container = value;
                OnPropertyChanged(nameof(Container));
            }
        }

        /// <summary>
        /// The last refresh date of the <see cref="Container"/>.
        /// </summary>
        public string FormattedLastRefreshDate => TimestampToDateTime(timestamp).ToString("dd MM yyyy, HH:mm:ss");

        #endregion

        #region Methods

        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private DateTime TimestampToDateTime(long timestamp)
        {
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return unixEpoch.AddMilliseconds(timestamp);
        }

        #endregion
    }
}
