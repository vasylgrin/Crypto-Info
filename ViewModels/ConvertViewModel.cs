using MyCryptoApp.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Windows.Input;

namespace MyCryptoApp.ViewModels
{
    internal class ConvertViewModel : BaseVM
    {
        #region Property
        private string? _firstToken;
        private string? _secondToken;
        private JToken? data;
        private ObservableCollection<Token> _Tokens = new();
        private string? _printConvertToken;
        private double? _firstValueToken = 1;
        double firstTokenPrice = 0, secondTokenPrice = 0, commision;

        public string FirstToken { get { return _firstToken; } set { _firstToken = value; OnPropertyChanged(); } }
        public string SecondToken { get { return _secondToken; } set { _secondToken = value; OnPropertyChanged(); } }
        public ObservableCollection<Token> Tokens { get => _Tokens; set { _Tokens = value; OnPropertyChanged(); } }
        public string? PrintConvertToken { get => _printConvertToken; set { _printConvertToken = value; OnPropertyChanged(); } }
        public double? FirstValueToken { get { return _firstValueToken; } set { _firstValueToken = value; OnPropertyChanged(); } }
        public string Response { get; private set; }
        #endregion

        public ICommand ConvertToken
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (string.IsNullOrWhiteSpace(_firstToken) || string.IsNullOrWhiteSpace(_secondToken))
                        return;

                    string tok1 = "";
                    GetToken(_firstToken, out tok1, out firstTokenPrice);

                    commision = firstTokenPrice / 100 * 5;
                    firstTokenPrice -= commision;

                    string tok2 = "";
                    GetToken(_secondToken, out tok2, out secondTokenPrice);

                    double res = (double)(firstTokenPrice * _firstValueToken / secondTokenPrice);
                    PrintConvertToken = Math.Round(res, 4).ToString();
                });
            }
        }

        public ConvertViewModel()
        {

        }

        public void GetToken(string searchToken, out string printToken, out double tokenPrice)
        {
            GetRequest("https://api.coincap.io/v2/assets");

            foreach (var item in data)
            {
                Token token = new(Convert.ToInt32(item["rank"]), item["id"].ToString(), item["symbol"].ToString(), Convert.ToDouble(item["priceUsd"]));
                Tokens.Add(token);

            }
            string tok = "";
            double price = 0;
            foreach (var tokens in Tokens)
            {
                if (tokens.Symbol.Contains(searchToken.ToUpper()))
                {
                    tok = tokens.ToString();
                    price = tokens.Price;
                    break;
                }
            }
            printToken = tok;
            tokenPrice = price;
        }

        void GetRequest(string adress)
        {
            HttpWebRequest _request;
            _request = WebRequest.Create(adress) as HttpWebRequest;
            _request.Method = "GET";

            try
            {
                HttpWebResponse response = _request.GetResponse() as HttpWebResponse;
                var stream = response.GetResponseStream();
                if (stream != null) Response = new StreamReader(stream).ReadToEnd();
            }
            catch (Exception)
            {
            }

            var responses = Response;
            var nameJson = JObject.Parse(responses);
            data = nameJson["data"];
        }
    }
}