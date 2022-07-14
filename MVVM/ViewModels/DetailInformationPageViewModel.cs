using MyCryptoApp.Controller;
using MyCryptoApp.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MyCryptoApp.ViewModels
{
    internal class DetailInformationPageViewModel : JsonController
    {
        #region Property

        private string? _TokenToSearch;
        private string? _GetSearchToken;
        private ObservableCollection<Token> _Tokens = new();
        private ObservableCollection<Market> _Market = new();
        Token currentToken = new();

        public string? TokenToSearch { get => _TokenToSearch; set { _TokenToSearch = value; OnPropertyChanged("TokenToSearch"); } }
        public string? GetSearchToken { get => _GetSearchToken; set { _GetSearchToken = value; OnPropertyChanged(); } }
        public ObservableCollection<Token> Tokens { get => _Tokens; set { _Tokens = value; OnPropertyChanged(); } }
        public ObservableCollection<Market> Markets { get => _Market; set { _Market = value; OnPropertyChanged(); } }

        #endregion

        /// <summary>
        /// Button for search token.
        /// </summary>
        public DelegateCommand SearchTokenButton => new((obj) =>
        {
            Calculate();
        });

        /// <summary>
        /// Calculation.
        /// </summary>
        private async void Calculate()
        {
            await Task.Factory.StartNew(() =>
            {
                // Checking for null.
                if (string.IsNullOrWhiteSpace(TokenToSearch))
                {
                    Markets.Clear();
                    GetSearchToken = "Input string empty";
                    return;
                }

                // We receive the token and check whether it is valid.
                if (GetToken("https://api.coincap.io/v2/assets", TokenToSearch, out currentToken))
                {
                    GetSearchToken = $"{TokenToSearch} not found";
                    return;
                }
                else
                    GetSearchToken = currentToken.ToString();

                // We get a list of stores where you can buy a token.
                GetMarkets(currentToken.FullName.ToLower(), out ObservableCollection<Market> marketList);
                Markets = marketList;
            });
        }
    }
}
