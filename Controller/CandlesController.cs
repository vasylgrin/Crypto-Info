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
    internal class CandlesController : JsonController
    {
        #region Property
        
        readonly List<Candles> candles = new();
        readonly ChartValues<ObservableValue> val0 = new();
        readonly ChartValues<ObservableValue> val1 = new();
        readonly ChartValues<OhlcPoint> val2 = new();

        #endregion

        /// <summary>
        /// Output of candles on the chart.
        /// </summary>
        /// <param name="baseId">Name of the main token.</param>
        /// <param name="quoteId">Сurrency token.</param>
        /// <param name="seriesCollection">The result of the graph.</param>
        protected bool PrintCandles(string? baseId, string? quoteId, out SeriesCollection seriesCollection)
        {
            SeriesCollection _seriesCollection = new();

            // Checking for null.
            if (string.IsNullOrWhiteSpace(baseId) || string.IsNullOrWhiteSpace(quoteId))
            {
                seriesCollection = new SeriesCollection();
                return true;
            }

            // Checking for null and we add candles to the collections.
            if (AddCandle(baseId, quoteId))
            {
                seriesCollection = new SeriesCollection();
                return true;
            }

            // Adding candles to the chart.
            System.Windows.Application.Current.Dispatcher.Invoke(()=>
            {
                _seriesCollection = new SeriesCollection
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
            });

            seriesCollection = _seriesCollection;
            return false;
        }

        /// <summary>
        /// We choose the number of candles and add them to the collection.
        /// </summary>
        /// <param name="baseId">Name of the main token.</param>
        /// <param name="quoteId">Сurrency token.</param>
        private bool AddCandle(string? baseId, string? quoteId)
        {
            // Сhecking for null and receiving candles.
            if (GetAllCandles(baseId, quoteId))
            {
                return true;
            }

            // Created two EMA.
            EMA ind0 = new(20);
            EMA ind1 = new(10);

            // We choose the number of candles and add them to the collection.
            for (int i = 0; i < 30; i++)
            {
                ind0.Add(candles[i].Close);
                ind1.Add(candles[i].Close);

                val0.Add(new ObservableValue(ind0.Value[0]));
                val1.Add(new ObservableValue(ind1.Value[0]));

                val2.Add(new OhlcPoint(candles[i].Open, candles[i].High, candles[i].Low, candles[i].Close));
            }
            return false;
        }

        /// <summary>
        /// Getting all the candles for two tokens.
        /// </summary>
        /// <param name="baseId">Name of the main token.</param>
        /// <param name="quoteId">Сurrency token.</param>
        private bool GetAllCandles(string? baseId, string? quoteId)
        {           
            // We receive json data from the sent address.
            GetRequest($"https://api.coincap.io/v2/candles?exchange=poloniex&interval=h8&baseId={baseId}&quoteId={quoteId}",out JToken data);

            // We add all the candles to the list of candles.
            foreach (var item in data)
            {
                Candles candle = new(Convert.ToDouble(item["open"]), Convert.ToDouble(item["high"]), Convert.ToDouble(item["low"]), Convert.ToDouble(item["close"]));
                candles.Add(candle);
            }

            // Сhecking for null.
            if (candles.Count == 0)
                return true;
            else 
                return false;
        }       
    } 
    
}
