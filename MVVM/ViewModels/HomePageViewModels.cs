using MyCryptoApp.Models;
using System.Collections.ObjectModel;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyCryptoApp.ViewModels
{
    internal class HomePageViewModels : BaseVM
    {
        #region Propertys

        private ObservableCollection<Token> _Tokens = new();
        public ObservableCollection<Token> Tokens { get => _Tokens; set { _Tokens = value; OnPropertyChanged(); } }

        #endregion

        /// <summary>
        /// Displaying top 10 currencies tokens.
        /// </summary>
        public HomePageViewModels()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    #region Variables

                    string line = "";
                    int i = 1;

                    #endregion

                    // Getting api json in a string.
                    using (WebClient webClient = new())
                    {
                        line = webClient.DownloadString("https://api.coincap.io/v2/assets");
                    }

                    // We select the first 11 tokens and display them.
                    while (i < 11)
                    {
                        Match match = Regex.Match(line, $"\"rank\":\"{i}\",(.*?)\"symbol\":\"(.*?)\",(.*?)\"name\":\"(.*?)\",");

                        Token token = new(i, match.Groups[4].Value, match.Groups[2].Value);

                        System.Windows.Application.Current.Dispatcher.Invoke(() =>
                        {
                            Tokens.Add(token);
                        });

                        i++;
                    }

                    // Delay for 10 seconds
                    Task.Delay(10000).Wait();

                    // Clearing the token list.
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        Tokens?.Clear();
                    });
                }
            });
        }
    }
}
