using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCryptoApp.Models
{
    internal class Market
    {
        public string? Name { get; set; }
        public Token? Token { get; set; }
        public double? PriceUsd { get; set; }
        public string? QuoteId { get; set; }
        
        public Market(string? name, Token? token, double? priceUsd, string? quoteId)
        {
            Name = name;
            Token = token;
            PriceUsd = priceUsd;
            QuoteId = quoteId;
        }
    }
}
