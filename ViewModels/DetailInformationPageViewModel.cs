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
        #region Property
        private string? _TextToSend;
        private string? _GetSearchToken;
        private ObservableCollection<Token> _Tokens = new();
        private ObservableCollection<Market> _Market = new();
        string currentToken = "";
        JToken? data;
        readonly ObservableCollection<Market> marketList = new();

        public string? TextToSend { get => _TextToSend; set { _TextToSend = value; OnPropertyChanged("TextToSend"); } }
        public string? GetSearchToken { get => _GetSearchToken; set { _GetSearchToken = value; OnPropertyChanged(); } }
        public ObservableCollection<Token> Tokens { get => _Tokens; set { _Tokens = value; OnPropertyChanged(); } }
        public ObservableCollection<Market> Markets { get => _Market; set { _Market = value; OnPropertyChanged(); } }

        readonly BaseVM baseVM = new();
        #endregion

        /// <summary>
        /// Button for search token.
        /// </summary>
        public DelegateCommand SearchCommand => new((obj) =>
        {
            if (string.IsNullOrWhiteSpace(_TextToSend))
            {
                Markets.Clear();
                GetSearchToken = "Input string empty";
                return;
            }
            
            GetToken();
            GetMarkets();
            
        });

        void GetToken()
        {
            GetSearchToken = "";
            baseVM.GetRequest("https://api.coincap.io/v2/assets",out data);

            foreach (var item in data)
            {
                Token token = new(Convert.ToInt32(item["rank"]), item["id"].ToString(), item["symbol"].ToString(), Convert.ToDouble(item["priceUsd"]));
                Tokens.Add(token);
            }
            string variable = "";
            foreach (var tokens in Tokens)
            {
                if (tokens.Symbol.Contains(TextToSend.ToUpper()))
                {
                    variable = tokens.ToString();
                    currentToken = tokens.FullName.ToLower();
                    break;
                }
                if (tokens.FullName.ToUpper().Contains(TextToSend.ToUpper()))
                {
                    variable = tokens.ToString();
                    currentToken = tokens.FullName.ToLower();
                    break;
                }
            }
            if (!string.IsNullOrWhiteSpace(variable))
            {
                GetSearchToken = variable;
            }
            else
            {
                GetSearchToken = $"Token {TextToSend} not found";
            }
        }

        void GetMarkets()
        {
            Markets.Clear();
            
            if (string.IsNullOrWhiteSpace(currentToken))
                return;
            
            baseVM.GetRequest($"https://api.coincap.io/v2/assets/{currentToken}/markets",out data);

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
                if (item.TokenName.ToUpper().Contains(TextToSend.ToUpper()))
                {
                    Markets.Add(item);
                }
            }
        }  
    }
}
