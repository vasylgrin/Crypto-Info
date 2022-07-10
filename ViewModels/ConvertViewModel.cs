using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MyCryptoApp.Controller;
using MyCryptoApp.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

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
        private double _opacity;
        private string? _errorText;


        public string? FirstToken { get { return _firstToken; } set { _firstToken = value; OnPropertyChanged(); } }
        public string? SecondToken { get { return _secondToken; } set { _secondToken = value; OnPropertyChanged(); } }
        public ObservableCollection<Token> Tokens { get => _Tokens; set { _Tokens = value; OnPropertyChanged(); } }
        public string? PrintConvertToken { get => _printConvertToken; set { _printConvertToken = value; OnPropertyChanged(); } }
        public double? FirstValueToken { get { return _firstValueToken; } set { _firstValueToken = value; OnPropertyChanged(); } }
        public double ErrorOpacityPropertyMVVM { get => _opacity;  set { _opacity = value; OnPropertyChanged(); } }      
        public string? ErrorTextMVVM { get => _errorText;  set { _errorText = value; OnPropertyChanged(); } }
        readonly BaseVM baseVM = new();
        #endregion

        public ICommand ConvertToken
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    TokenConvert();
                });
            }
        }    
        public async void TokenConvert()
        {
            await Task.Factory.StartNew(() =>
            {
                if (string.IsNullOrWhiteSpace(_firstToken))
                {
                    ErrorOpacity("That we convert\ncannot be empty");
                    return;
                }
                
                if (string.IsNullOrWhiteSpace(_secondToken))
                {
                    ErrorOpacity("What do we convert\nto cannot be empty");
                    return;
                }

                string tok1 = "";
                GetToken(_firstToken, out firstTokenPrice);

                commision = firstTokenPrice / 100 * 5;
                firstTokenPrice -= commision;

                string tok2 = "";
                GetToken(_secondToken, out secondTokenPrice);

                double? res = (firstTokenPrice * _firstValueToken / secondTokenPrice);
                PrintConvertToken = Math.Round((double)res, 4).ToString();
            });
        }
        public void GetToken(string searchToken, out double tokenPrice)
        {
            tokenPrice = 0;
            baseVM.GetRequest("https://api.coincap.io/v2/assets", out data);

            foreach (var item in data)
            {
                Token token = new(Convert.ToInt32(item["rank"]), item["id"].ToString(), item["symbol"].ToString(), Convert.ToDouble(item["priceUsd"]));
                Tokens.Add(token);

            }
            double price = 0;
            foreach (var tokens in Tokens)
            {
                if (tokens.Symbol.Contains(searchToken.ToUpper()))
                {
                    price = tokens.Price;
                    break;
                }
                if (tokens.FullName.ToUpper().Contains(searchToken.ToUpper()))
                {
                    price = tokens.Price;
                    break;
                }
            }

            if (price != 0)
            {
                tokenPrice = price;
            }
            else
                ErrorOpacity($"Token {searchToken} not found");
        }                
        public async void ErrorOpacity(string errorText)
        {
            await Task.Factory.StartNew(() =>
            {
                ErrorTextMVVM = "";
                for (double i = 0.0; i < 1; i += 0.1)
                {
                    ErrorOpacityPropertyMVVM = i;
                    Thread.Sleep(50);
                }
                ErrorTextMVVM = errorText;
                Thread.Sleep(5000);
                for (double i = 1; i > 0.0; i -= 0.1)
                {
                    ErrorOpacityPropertyMVVM = i;
                    Thread.Sleep(50);
                }
            });           
        }
    }
}