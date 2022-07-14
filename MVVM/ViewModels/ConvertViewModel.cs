using LiveCharts;
using MyCryptoApp.Controller;
using MyCryptoApp.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyCryptoApp.ViewModels
{
    internal class ConvertViewModel : CandlesController
    {
        #region Property
        private string? _firstToken;
        private string? _secondToken;
        private double? _firstValueToken = 1;
        private string? _printConvertToken;
        double firstTokenPrice;
        double commision;
        private double _opacity;
        private string? _errorText;
        Token currentToken = new();
        Token secondToken = new();
        private SeriesCollection _seriesCollection = new();
        private Token? baseIdSearch = new();
        private Token quoteIdSearch = new();
        readonly CandlesController candlesController = new();

        public string? FirstToken { get { return _firstToken; } set { _firstToken = value; OnPropertyChanged(); } }
        public string? SecondToken { get { return _secondToken; } set { _secondToken = value; OnPropertyChanged(); } }
        public double? FirstValueToken { get { return _firstValueToken; } set { _firstValueToken = value; OnPropertyChanged(); } }
        public string? PrintConvertToken { get => _printConvertToken; set { _printConvertToken = value; OnPropertyChanged(); } }
        public double ErrorOpacityProperty { get => _opacity; set { _opacity = value; OnPropertyChanged(); } }
        public string? ErrorText { get => _errorText; set { _errorText = value; OnPropertyChanged(); } }
        public SeriesCollection SeriesCollection { get => _seriesCollection; set { _seriesCollection = value; OnPropertyChanged(); } }
        public Func<double, string> YFormatter { get; set; }
        #endregion

        public ICommand ConvertToken
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    await CalculateAsync();
                });
            }
        }

        /// <summary>
        /// Calculateing.
        /// </summary>
        /// <returns></returns>
        private async Task CalculateAsync()
        {
            #region Checking for null

            if (string.IsNullOrWhiteSpace(_firstToken))
            {
                ErrorBorderOpacity("That we convert\ncannot be empty");
                return;
            }
            if (string.IsNullOrWhiteSpace(_secondToken))
            {
                ErrorBorderOpacity("What do we convert\nto cannot be empty");
                return;
            }

            #endregion

            await TokenConvert();
            await PrintCandlesGraph();
        }

        /// <summary>
        /// A method that receives two tokens for conversion.
        /// </summary>
        private async Task TokenConvert()
        {
            await Task.Factory.StartNew(() =>
            {
                #region Getting tokens and checking for null

                if (GetToken("https://api.coincap.io/v2/assets", FirstToken, out currentToken))
                {
                    ErrorBorderOpacity($"{FirstToken} not found");
                    return;
                }
                if (GetToken("https://api.coincap.io/v2/assets", SecondToken, out secondToken))
                {
                    ErrorBorderOpacity($"{SecondToken} not found");
                    return;
                }

                #endregion

                #region Сalculation of how many tokens can be converted

                commision = currentToken.Price / 100 * 5;
                firstTokenPrice = currentToken.Price - commision;

                double? res = (firstTokenPrice * _firstValueToken / secondToken.Price);
                PrintConvertToken = Math.Round((double)res, 4).ToString();

                #endregion
            });
        }

        /// <summary>
        /// A method that receives two tokens and outputs a candlestick chart.
        /// </summary>
        private async Task PrintCandlesGraph()
        {
            await Task.Factory.StartNew(() =>
            {
                // We receive two tokens.
                if (GetToken("https://api.coincap.io/v2/assets", FirstToken, out baseIdSearch))
                    return;
                if (GetToken("https://api.coincap.io/v2/assets", SecondToken, out quoteIdSearch))
                    return;

                // Checking for null adn we call the PrintCandles method, to which we pass two tokens and receive a graphic of candles.
                if (PrintCandles(baseIdSearch.FullName.ToLower(), quoteIdSearch.FullName.ToLower(), out SeriesCollection _SeriesCollection))
                {
                    ErrorBorderOpacity("Candle not found.");
                    SeriesCollection.Clear();
                    return;
                }

                // Graphics output.
                SeriesCollection.Clear();
                SeriesCollection = _SeriesCollection;
                YFormatter = value => value.ToString("C");
            });
        }

        /// <summary>
        /// Method for the smooth appearance of the border. 
        /// </summary>
        /// <param name="errorText">Error text</param>
        private async void ErrorBorderOpacity(string errorText)
        {
            await Task.Factory.StartNew(() =>
            {
                ErrorText = "";

                // Slow manifestation of the border.
                for (double i = 0.0; i < 1; i += 0.1)
                {
                    ErrorOpacityProperty = i;
                    Thread.Sleep(50);
                }

                // Assignment of error text and pause.
                ErrorText = errorText;
                Thread.Sleep(5000);

                // The slow disappearance of the border.
                for (double i = 1; i > 0.0; i -= 0.1)
                {
                    ErrorOpacityProperty = i;
                    Thread.Sleep(50);
                }
            });
        }
    }
}