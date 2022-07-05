using MyCryptoApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
        private ObservableCollection<Market> _Market = new();
        string responses, currentToken = "";
        JToken data;
        ObservableCollection<Market> marketList = new();

        public string TextToSend { get => _TextToSend; set { _TextToSend = value; OnPropertyChanged("TextToSend"); } }
        public string GetSearchToken { get => _GetSearchToken; set { _GetSearchToken = value; OnPropertyChanged(); } }
        public ObservableCollection<Token> Tokens { get => _Tokens; set { _Tokens = value; OnPropertyChanged(); } }
        public ObservableCollection<Market> Markets { get => _Market; set { _Market = value; OnPropertyChanged(); } }
        public string Response { get; set; }

        /// <summary>
        /// Button for search token.
        /// </summary>
        public DelegateCommand SearchCommand => new DelegateCommand((obj) =>
        {
            if (string.IsNullOrWhiteSpace(_TextToSend))
                return;
            
            GetToken();
            GetMarkets();
            
        });

        void GetToken()
        {
            GetRequest("https://api.coincap.io/v2/assets");

            foreach (var item in data)
            {
                Token token = new(Convert.ToInt32(item["rank"]), item["id"].ToString(), item["symbol"].ToString(), Convert.ToDouble(item["priceUsd"]));
                Tokens.Add(token);
            }

            foreach (var tokens in Tokens)
            {
                if (tokens.Symbol.Contains(TextToSend.ToUpper()))
                {
                    GetSearchToken = tokens.ToString();
                    currentToken = tokens.FullName.ToLower();
                    break;
                }
            }
        }

        void GetMarkets()
        {
            Markets.Clear();
            GetRequest($"https://api.coincap.io/v2/assets/{currentToken}/markets");

            foreach (var item in data)
            {
                marketList.Add(new Market(
                    item["exchangeId"].ToString(), 
                    item["baseId"].ToString(), 
                    item["baseSymbol"].ToString(), 
                    Math.Round(Convert.ToDouble(item["priceUsd"]),4), 
                    item["quoteId"].ToString(), 
                    item["quoteSymbol"].ToString()
                    ));
            }

            foreach (var item in marketList)
            {
                if (item.TokenSymbol.ToUpper().Contains(TextToSend.ToUpper()))
                {
                    Markets.Add(item);
                }
            }
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
