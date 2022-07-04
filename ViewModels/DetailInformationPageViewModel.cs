using MyCryptoApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyCryptoApp.ViewModels
{
    internal class DetailInformationPageViewModel : BaseVM
    {
        private string _TextToSend;
        private string _GetSearchToken;
        private ObservableCollection<Token> _Tokens = new();

        public string TextToSend { get => _TextToSend; set { _TextToSend = value; OnPropertyChanged("TextToSend"); } }
        public string GetSearchToken { get => _GetSearchToken; set { _GetSearchToken = value; OnPropertyChanged(); } }

        public ObservableCollection<Token> Tokens { get => _Tokens; set { _Tokens = value; OnPropertyChanged(); } }


        /// <summary>
        /// Button for search token.
        /// </summary>
        public DelegateCommand SearchCommand => new DelegateCommand((obj) =>
        {
            if (string.IsNullOrWhiteSpace(_TextToSend))
                return;
            
            string tokenWc = "", marketWc = "";

            using (WebClient webClient = new WebClient())
            {
                tokenWc = webClient.DownloadString("https://api.coincap.io/v2/assets");
                marketWc = webClient.DownloadString($"https://api.coincap.io/v2/assets/bitcoin/markets");
            }

            Match tokenMatch = Regex.Match(tokenWc, $"\"symbol\":\"{_TextToSend.ToUpper()}\",(.*?)\"name\":\"(.*?)\",(.*?)\"priceUsd\":\"(.*?)\",(.*?)\"vwap24Hr\":\"(.*?)\"");

            Token token = new(1,tokenMatch.Groups[2].Value, tokenMatch.Groups[4].Value, Convert.ToDouble(tokenMatch.Groups[6].Value));
            int i = 1;
            Match marketMatch;
            while (true)
            {
                marketMatch = Regex.Match(marketWc, $";
                i++;
                if (i == 30)
                    break;
            }
            GetSearchToken = marketMatch.Value;

            //Tokens.Add(new Token(1, tokenMatch.Groups[2].Value, tokenMatch.Groups[4].Value, Convert.ToDouble(tokenMatch.Groups[6].Value)));
            //GetSearchToken = token.ToString();
        });
    }
}
