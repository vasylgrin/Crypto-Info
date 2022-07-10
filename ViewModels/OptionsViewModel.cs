using System.Windows.Input;

namespace MyCryptoApp.ViewModels
{
    internal class OptionsViewModels : BaseVM
    {
        public OptionsViewModels()
        {

        }

        public static ICommand Light
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Properties.Settings.Default.ColorMode = "Light";
                    Properties.Settings.Default.Save();
                });
            }
        }

        public static ICommand Dark
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Properties.Settings.Default.ColorMode = "Dark";
                    Properties.Settings.Default.Save();
                });
            }
        }
    }
}
