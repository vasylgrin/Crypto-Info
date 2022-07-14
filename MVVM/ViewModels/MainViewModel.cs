using MyCryptoApp.Views.Pages;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyCryptoApp.ViewModels
{
    internal class MainViewModel : BaseVM
    {
        #region Property
        
        private object _currentPage;
        private double _BorderOpacity;
        public object CurrentPage { get => _currentPage; set { _currentPage = value; OnPropertyChanged(); } }
        public double BorderOpacity { get => _BorderOpacity; set { _BorderOpacity = value; OnPropertyChanged(); } }

        #endregion
        
        public MainViewModel()
        {
            SlowOpacity(new Pages.Home());
        }

        public ICommand HomePageButton
        {
            get
            {
                return new DelegateCommand((obj) => SlowOpacity(new Pages.Home()));
            }
        }

        public ICommand DetailPageButton
        {
            get
            {
                return new DelegateCommand((obj) => SlowOpacity(new Pages.DetailInformation()));
            }
        }

        public ICommand OptionsButton
        {
            get
            {
                return new DelegateCommand((obj) => SlowOpacity(new Pages.Options()));
            }
        }

        public ICommand ConvertButton
        {
            get
            {
                return new DelegateCommand((obj) => SlowOpacity(new Convert()));
            }
        }

        /// <summary>
        /// Smooth page switching.
        /// </summary>
        /// <param name="obj">Page.</param>
        private async void SlowOpacity(object obj)
        {
            await Task.Factory.StartNew(() =>
            {
                // Іmooth fading of the page.
                for (double i = 1.0; i > 0.0; i -= 0.1)
                {
                    BorderOpacity = i;
                    Thread.Sleep(10);
                }

                // Switch page.
                CurrentPage = obj;

                // Smooth appearance of the page.
                for (double i = 0.0; i < 1.0; i += 0.1)
                {
                    BorderOpacity = i;
                    Thread.Sleep(10);
                }
            });
        }
    }
}
