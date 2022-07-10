using MyCryptoApp.Views.Pages;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyCryptoApp.ViewModels
{
    internal class MainViewModel : BaseVM
    {
        private object _currentPage;
        public object CurrentPage { get => _currentPage; set { _currentPage = value; OnPropertyChanged(); } }

        private double _BorderOpacity;

        public double BorderOpacity
        {
            get { return _BorderOpacity; }
            set { _BorderOpacity = value; OnPropertyChanged(); }
        }


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

        private async void SlowOpacity(object obj)
        {
            await Task.Factory.StartNew(() =>
            {
                for (double i = 1.0; i > 0.0; i -= 0.1)
                {
                    BorderOpacity = i;
                    Thread.Sleep(10);
                }
                CurrentPage = obj;
                for (double i = 0.0; i < 1.0; i += 0.1)
                {
                    BorderOpacity = i;
                    Thread.Sleep(10);
                }
            });
        }
    }
}
