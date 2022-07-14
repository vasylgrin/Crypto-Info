using MyCryptoApp.Models;
using MyCryptoApp.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyCryptoApp.Controller
{
    internal class JsonController : BaseVM
    {
        /// <summary>
        /// Method for receiving a token that returns false if the token was not found.
        /// </summary>
        /// <param name="addres">The address at which the token is searched.</param>
        /// <param name="searchToken">Token lookup data.</param>
        /// <param name="token">Found token.</param>
        /// <returns></returns>
        protected bool GetToken(string addres, string searchToken,out Token token)
        {
            #region Variables
            
            Token variable = new();
            token = new Token();
            List<Token> Tokens = new();

            #endregion

            // We receive json data from the sent address.
            GetRequest(addres,out JToken data);

            // Add all tokens to the list.
            foreach (var item in data)
            {
                Token t = new(Convert.ToInt32(item["rank"]), item["id"].ToString(), item["symbol"].ToString(), Convert.ToDouble(item["priceUsd"]));
                Tokens.Add(t);
            }

            // We check whether the name or abbreviation matches the search name.
            foreach (var tokens in Tokens)
            {
                if (tokens.Symbol.Contains(searchToken.ToUpper()))
                {
                    variable = tokens;
                    break;
                }

                if (tokens.FullName.ToUpper().Contains(searchToken.ToUpper()))
                {
                    variable = tokens;
                    break;
                }
            }

            // Checking for null
            if (variable.FullName != null)
            {
                token = variable;
                return false;
            }
            else
                return true;

        }

        /// <summary>
        /// A method to get stores to buy a token.
        /// </summary>
        /// <param name="currentToken">The name of the search token.</param>
        /// <param name="Markets">Stores that have the required token.</param>
        protected void GetMarkets(string currentToken, out ObservableCollection<Market> Markets)
        {
            #region Variables
            
            List<Market> marketList = new();
            Markets = new();

            #endregion

            // We receive json data from the sent address.
            GetRequest($"https://api.coincap.io/v2/assets/{currentToken}/markets", out JToken data);

            // We add all stores that have this token.
            foreach (var item in data)
            {
                marketList.Add(new Market(
                    item["exchangeId"].ToString(),
                    item["baseId"].ToString(),
                    item["baseSymbol"].ToString(),
                    Math.Round(Convert.ToDouble(item["priceUsd"]), 4),
                    item["quoteId"].ToString(),
                    item["quoteSymbol"].ToString()
                    ));
            }

            // Filter the data.
            foreach (var item in marketList)
            {
                if (item.TokenSymbol.ToUpper() == currentToken.ToUpper())
                {
                    Markets.Add(item);
                }
                if (item.TokenName.ToUpper() == currentToken.ToUpper())
                {
                    Markets.Add(item);
                }
            }
        }

        /// <summary>
        /// The method that sends the request through the API to receives the JToken object.
        /// </summary>
        /// <param name="adress">Address for sending the request.</param>
        /// <param name="data">Query result.</param>
        protected void GetRequest(string adress, out JToken data)
        {
            string Response = "";
            HttpWebRequest? _request = WebRequest.Create(adress) as HttpWebRequest;
            _request.Method = "GET";

            try
            {
                HttpWebResponse? response = _request.GetResponse() as HttpWebResponse;
                var stream = response.GetResponseStream();
                if (stream != null) Response = new StreamReader(stream).ReadToEnd();
            }
            catch (Exception)
            {
            }

            var nameJson = JObject.Parse(Response);
            data = nameJson["data"];
        }
    }
}
