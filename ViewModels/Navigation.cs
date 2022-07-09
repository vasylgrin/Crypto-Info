using MyCryptoApp.Views.Pages;
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
                return new DelegateCommand((obj) => CurrentPage = new Pages.Home());
            }
        }


        public ICommand DetailPageButton
        {
            get
            {
                return new DelegateCommand((obj) => CurrentPage = new Pages.DetailInformation());
            }
        }


        public ICommand OptionsButton
        {
            get
            {
                return new DelegateCommand((obj) => CurrentPage = new Pages.Options());
            }
        }

        public ICommand ConvertButton
        {
            get
            {
                return new DelegateCommand((obj) => CurrentPage = new Convert());
            }
        }
    }
}
