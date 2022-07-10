using MyCryptoApp.Models;
using System.Collections.ObjectModel;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyCryptoApp.ViewModels
{
    internal class HomePageViewModels : BaseVM
    {
        private string _GetTopToken;
        private ObservableCollection<Token> _Tokens = new();

        public string GetTopToken { get => _GetTopToken; set { _GetTopToken = value; OnPropertyChanged(); } }
        public ObservableCollection<Token> Tokens { get => _Tokens; set { _Tokens = value; OnPropertyChanged(); } }




        /// <summary>
        /// Displaying top 10 currencies tokens.
        /// </summary>
        public HomePageViewModels()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    string[] res = new string[11];
                    string line = "";
                    int i = 1;

                    using (WebClient webClient = new())
                    {
                        line = webClient.DownloadString("https://api.coincap.io/v2/assets");
                    }

                    while (i < 11)
                    {
                        Match match = Regex.Match(line, $"\"rank\":\"{i}\",(.*?)\"symbol\":\"(.*?)\",(.*?)\"name\":\"(.*?)\",");

                        Token token = new(i, match.Groups[4].Value, match.Groups[2].Value);

                        App.Current.Dispatcher.Invoke(() =>
                        {
                            Tokens.Add(token);
                        });

                        i++;
                    }

                    Task.Delay(10000).Wait();
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        Tokens?.Clear();
                    });
                }
            });
        }
    }
}
