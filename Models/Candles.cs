using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCryptoApp.Models
{
    internal class Candles
    {
        public double Open { get; set; }
        public double Close { get; set; }
        public double High { get; set; }
        public double Low { get; set; }

        public Candles(double open, double close, double high, double low)
        {
            Open = open;
            Close = close;
            High = high;
            Low = low;
        }
    }
}
