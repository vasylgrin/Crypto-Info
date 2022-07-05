using System.Windows.Input;

namespace MyCryptoApp.ViewModels
{
    internal class MainViewModel : Navigation
    {

        public MainViewModel()
        {
            CurrentPage = new Pages.Home();
        }    
    }
}
