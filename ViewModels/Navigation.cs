using MVVM;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyCryptoApp.ViewModels
{
    public class Navigation : BaseVM
    {
        private object _currentPage;

        public object CurrentPage { get => _currentPage; set { _currentPage = value; OnPropertyChanged(); } }
        public Navigation()
        {

        }

        public Navigation(object currentPage)
        {
            CurrentPage = currentPage;
        }

        public ICommand HomePageButton
        {
            get
            {
                return new RelayCommand((obj) => CurrentPage = new Pages.Home());
            }
        }


        public ICommand DetailPageButton
        {
            get
            {
                return new RelayCommand((obj) => CurrentPage = new Pages.DetailInformation());
            }
        }


        public ICommand OptionsButton
        {
            get
            {
                return new RelayCommand((obj) => CurrentPage = new Pages.Options());
            }
        }
    }
}
