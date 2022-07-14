using System;

namespace MyCryptoApp.Models
{
    internal class Candles
    {
        /// <summary>
        /// Open candle.
        /// </summary>
        public double Open { get; set; }
        /// <summary>
        /// Close candle.
        /// </summary>
        public double Close { get; set; }
        /// <summary>
        /// High candle.
        /// </summary>
        public double High { get; set; }
        /// <summary>
        /// Low candle.
        /// </summary>
        public double Low { get; set; }

        /// <summary>
        /// Create new candle.
        /// </summary>
        /// <param name="open">Open cnadle.</param>
        /// <param name="close">Close candle.</param>
        /// <param name="high">High candle.</param>
        /// <param name="low">Low candle.</param>
        public Candles(double open, double close, double high, double low)
        {
            if (open < 0)
                throw new ArgumentNullException("Open cannot be less than zero.", nameof(open));
            if (close < 0)
                throw new ArgumentNullException("Close cannot be less than zero.", nameof(close));
            if (high < 0)
                throw new ArgumentNullException("High cannot be less than zero.", nameof(high));
            if (low < 0)
                throw new ArgumentNullException("Low cannot be less than zero.", nameof(low));

            Open = open;
            Close = close;
            High = high;
            Low = low;
        }
    }
}
