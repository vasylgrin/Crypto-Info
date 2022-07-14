using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCryptoApp.Models
{
    internal class Token
    {
        /// <summary>
        /// The sequence number token.
        /// </summary>
        public int Number { get; set; }
        
        /// <summary>
        /// The fullname token.
        /// </summary>
        public string? FullName { get; set; }

        /// <summary>
        /// The abreviated token.
        /// </summary>
        public string? Symbol { get; set; }

        /// <summary>
        /// Currency price.
        /// </summary>
        public double Price { get; set; }
        
        /// <summary>
        /// Create token.
        /// </summary>
        /// <param name="number">Sequence number token.</param>
        /// <param name="fullName">Fullname token.</param>
        /// <param name="symbol">Abreviated token.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Token(int number, string fullName, string symbol)
        {
            if(number < 0)
                throw new ArgumentNullException("The sequence number cannot be less than or equal to null.", nameof(number));
            if(string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentNullException("The full name cannot be empty.", nameof(FullName));
            if(string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentNullException("The abreviated name cannot be empty.",nameof(symbol));
            Number = number;
            FullName = fullName;
            Symbol = symbol;

        }

        /// <summary>
        /// Create token.
        /// </summary>
        /// <param name="number">Sequence number token.</param>
        /// <param name="fullName">Fullname token.</param>
        /// <param name="symbol">Abreviated token.</param>
        /// <param name="price">Price token.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Token(int number, string fullName, string symbol, double price) : this(number, fullName, symbol)
        {
            if (price < 0)
                throw new ArgumentNullException("The price cannot be less than or equal to null.", nameof(price));
            Price = price;
        }

        public Token()
        {

        }

        public override string ToString()
        {
            return $"{Number}\t{FullName}\t{Symbol}\t{Math.Round(Price,4)}$";
        }
    }
}
