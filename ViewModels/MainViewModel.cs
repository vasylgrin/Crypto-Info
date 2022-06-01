using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyCryptoApp.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnProopertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        
        private string _GetTopToken;
        public string GetTopToken
        {
            get { return _GetTopToken; }
            set
            {
                _GetTopToken = value;
                OnProopertyChanged();
            }
        }


        private int _Clicks;
        public int Clicks
        {
            get
            {
                return _Clicks;
            }

            set
            {
                _Clicks = value;
                OnProopertyChanged(nameof(Clicks));
            }
        }

        public ICommand ClickAdd
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Clicks++;
                    //var text = SearchTextBox
                });
            }
        }

        public MainViewModel()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    string[] res = new string[11];
                    string line = "";
                    int i = 1;

                    using (WebClient webClient = new WebClient())
                    {
                        line = webClient.DownloadString("https://api.coincap.io/v2/assets");
                    }

                    while (i < 11)
                    {
                        Match match = Regex.Match(line, $"\"rank\":\"{i}(.*?)\",(.*?)\"name\":\"(.*?)\",");
                        res[i] = $"Rank: {i} " + " " + match.Groups[3].Value + "\n";
                        i++;
                    }

                    foreach (var ress in res)
                    {
                        GetTopToken += ress;
                    }

                    Task.Delay(100000).Wait();
                    GetTopToken = string.Empty;
                }
            });
        }

        private string _GetSearchToken;
        public string GetSearchToken
        {
            get { return _GetSearchToken; }
            set
            {
                _GetSearchToken = value;
                OnProopertyChanged();
            }
        }
        private string _textToSend { get; set; }

        public string TextToSend { get { return _textToSend; } set { _textToSend = value; OnProopertyChanged("TextToSend"); } }

        public DelegateCommand SearchCommand => new DelegateCommand((obj) =>
        {
            string line = "";
            int i = 1;

            using (WebClient webClient = new WebClient())
            {
                line = webClient.DownloadString("https://api.coincap.io/v2/assets");
            }

            Match match = Regex.Match(line, $"\"symbol\":\"{_textToSend.ToUpper()}(.*?)\",(.*?)\"name\":\"(.*?)\",");

            GetSearchToken = match.Groups[3].Value;
        });
      
    }
}
