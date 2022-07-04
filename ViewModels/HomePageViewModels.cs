using MVVM;
using MyCryptoApp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyCryptoApp.ViewModels
{
    internal class HomePageViewModels : Navigation
    {
        private string _GetTopToken;
        private ObservableCollection<Token> _Tokens = new();
        private Token _selectToken;


        public string GetTopToken { get => _GetTopToken; set { _GetTopToken = value; OnPropertyChanged(); } }
        public ObservableCollection<Token> Tokens { get => _Tokens; set { _Tokens = value; OnPropertyChanged(); } }
        public Token SelectToken 
        { 
            get => _selectToken; 
            set 
            {
                new Navigation(new Pages.Home());
                OnPropertyChanged(); 
            } 
        }

        

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

                        Token token = new Token(i, match.Groups[4].Value, match.Groups[2].Value);
                        
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
