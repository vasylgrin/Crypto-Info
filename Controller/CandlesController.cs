using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MyCryptoApp.Models;
using MyCryptoApp.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Media;

namespace MyCryptoApp.Controller
{
    internal class CandlesController
    {
        #region Property
        public string Response { get; set; }
        private JToken? data;
        readonly List<Candles> candles = new();
        readonly ChartValues<ObservableValue> val0 = new();
        readonly ChartValues<ObservableValue> val1 = new();
        readonly ChartValues<OhlcPoint> val2 = new();
        readonly BaseVM baseVM = new();
        #endregion

        public void PrintCandles(string? baseId, string? quoteId, out SeriesCollection seriesCollection)
        {
            if (string.IsNullOrWhiteSpace(baseId) || string.IsNullOrWhiteSpace(quoteId))
            {
                seriesCollection = new SeriesCollection();
                return;
            }

            Calculate(baseId, quoteId);
            seriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "",
                    Stroke = Brushes.Aqua,
                    Fill = Brushes.Transparent,
                    PointGeometrySize = 10,
                    PointGeometry = DefaultGeometries.Square,
                    Values = val0
                },
                new LineSeries
                {
                    Title = "",
                    Stroke = Brushes.Red,
                    Fill = Brushes.Transparent,
                    PointGeometrySize = 5,
                    Values = val1
                },
                new OhlcSeries
                {
                    Values = val2
                }
            };
        }  
        public void Calculate(string? baseId, string? quoteId)
        {
            GetCandles(baseId, quoteId);

            EMA ind0 = new(20);
            EMA ind1 = new(10);

            for (int i = 0; i < 30; i++)
            {
                ind0.Add(candles[i].Close);
                ind1.Add(candles[i].Close);

                val0.Add(new ObservableValue(ind0.Value[0]));
                val1.Add(new ObservableValue(ind1.Value[0]));

                val2.Add(new OhlcPoint(candles[i].Open, candles[i].High, candles[i].Low, candles[i].Close));
            }
        }
        void GetCandles(string? baseId, string? quoteId)
        {
            baseVM.GetRequest($"https://api.coincap.io/v2/candles?exchange=poloniex&interval=h8&baseId={baseId}&quoteId={quoteId}",out data);
            foreach (var item in data)
            {
                Candles candle = new(Convert.ToDouble(item["open"]), Convert.ToDouble(item["high"]), Convert.ToDouble(item["low"]), Convert.ToDouble(item["close"]));
                candles.Add(candle);
            }
        }       
    } 
    
}
