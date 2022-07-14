using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCryptoApp.Models
{
    internal class Market
    {
        /// <summary>
        /// Market name.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Token Name.
        /// </summary>
        public string? TokenName { get; set; }
        /// <summary>
        /// Token Abreviated.
        /// </summary>
        public string? TokenSymbol { get; set; }
        /// <summary>
        /// Price token
        /// </summary>
        public double? PriceUsd { get; set; }
        /// <summary>
        /// Quote name
        /// </summary>
        public string? QuoteId { get; set; }
        /// <summary>
        /// Quote abreviated.
        /// </summary>
        public string? QuoteSymbol { get; set; }

        /// <summary>
        /// Create market.
        /// </summary>
        /// <param name="name">Market name.</param>
        /// <param name="tokenName">Token name.</param>
        /// <param name="tokenAbreviated">Token abreviated.</param>
        /// <param name="priceUsd">Price token.</param>
        /// <param name="quoteId">Quote name.</param>
        /// <param name="quoteSymbol">Quote abreviated.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Market(string? name, string? tokenName, string tokenAbreviated, double? priceUsd, string? quoteId, string quoteSymbol)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("The market name cannot be empty.", nameof(name));
            if (string.IsNullOrWhiteSpace(tokenName))
                throw new ArgumentNullException("The token name cannot be empty.", nameof(tokenName));
            if (string.IsNullOrWhiteSpace(tokenAbreviated))
                throw new ArgumentNullException("The token abreviated name cannot be empty.", nameof(tokenAbreviated));
            if (string.IsNullOrWhiteSpace(tokenName))
                throw new ArgumentNullException("The token name cannot be empty.", nameof(tokenName));
            if(priceUsd < 0)
                throw new ArgumentNullException("The price cannot be empty.", nameof(priceUsd));
            if (string.IsNullOrWhiteSpace(quoteId))
                throw new ArgumentNullException("The quote name name cannot be empty.", nameof(quoteId));
            if (string.IsNullOrWhiteSpace(quoteSymbol))
                throw new ArgumentNullException("The quote abreviated cannot be empty.", nameof(quoteSymbol));
            Name = name;
            TokenName = tokenName;
            TokenSymbol = tokenAbreviated;
            PriceUsd = priceUsd;
            QuoteId = quoteId;
            QuoteSymbol = quoteSymbol;
        }
    }
}
