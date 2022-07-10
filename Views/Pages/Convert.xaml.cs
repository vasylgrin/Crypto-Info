using LiveCharts;
using MyCryptoApp.Controller;
using MyCryptoApp.Models;
using MyCryptoApp.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MyCryptoApp.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для Convert.xaml
    /// </summary>
    public partial class Convert : UserControl
    {
        #region Property
        private SeriesCollection _seriesCollection = new();
        private ObservableCollection<Token> _Tokens = new();
        private JToken? data;
        private string? baseIdSearch = "", quoteIdSearch = "";
        
        public SeriesCollection SeriesCollection { get => _seriesCollection; set { _seriesCollection = value; } }
        public Func<double, string> YFormatter { get; set; }
        internal ObservableCollection<Token> Tokens { get => _Tokens; set { _Tokens = value; } }

        public double ErrorOpacityProperty { get; private set; }
        public string ErrorText { get; private set; }

        readonly CandlesController candlesController = new();
        readonly BaseVM baseVM = new();
        #endregion


        public Convert()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CalculateAsync();
            BorderOpacityClose.Opacity = 0;
        }



        public void CalculateAsync()
        {
            SeriesCollection.Clear();
            GetToken(BaseIdTextBlock.Text, out baseIdSearch);
            GetToken(QuoteIdTextBlock.Text, out quoteIdSearch);
            candlesController.PrintCandles(baseIdSearch, quoteIdSearch, out _seriesCollection);
            YFormatter = value => value.ToString("C");
            DataContext = this;
        }
        
        
        public void GetToken(string textToSearch,out string tokenOut)
        {
            tokenOut = "";
            if (string.IsNullOrWhiteSpace(textToSearch))
            {
                ErrorOpacity("Enter token pls");
                return;
            }

            baseVM.GetRequest("https://api.coincap.io/v2/assets",out data);

            foreach (var item in data)
            {
                Token token = new(System.Convert.ToInt32(item["rank"]), item["id"].ToString(), item["symbol"].ToString(), System.Convert.ToDouble(item["priceUsd"]));
                Tokens.Add(token);

            }

            string variable = "";

            foreach (var tokens in Tokens)
            {
                if (tokens.Symbol.Contains(textToSearch.ToUpper()))
                {
                    variable = tokens.FullName.ToLower();
                    break;
                }
                if (tokens.FullName.ToUpper().Contains(textToSearch.ToUpper()))
                {
                    variable = tokens.FullName.ToLower(); ;
                    break;
                }
            }

            if (string.IsNullOrWhiteSpace(variable))
            {
                ErrorOpacity("This candle not found");
            }
            else
                tokenOut = variable;
        }

        public void ErrorOpacity(string errorText)
        {
            Task.Factory.StartNew(() =>
            {
                for (double i = 0.0; i < 1; i += 0.1)
                {
                    Dispatcher.Invoke(() =>
                    {
                        BorderErrorOpacity.Opacity = i;

                    });
                    Thread.Sleep(50);
                }
                Dispatcher.Invoke(() =>
                {
                    TextBlockError.Text = errorText;

                });
                Thread.Sleep(1000);
                for (double i = 1; i > 0.0; i -= 0.1)
                {
                    Dispatcher.Invoke(() =>
                    {
                        BorderErrorOpacity.Opacity = i;

                    }); 
                    Thread.Sleep(50);
                }
            });           
        }
    }
}
