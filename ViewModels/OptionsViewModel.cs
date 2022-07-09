using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static ICommand Gray
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
