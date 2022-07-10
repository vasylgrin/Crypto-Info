using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;

namespace MyCryptoApp.ViewModels
{
    public class BaseVM : INotifyPropertyChanged
    {
        #region OnPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        #endregion


        public void GetRequest(string adress, out JToken data)
        {
            string Response = "";
            HttpWebRequest _request;
            _request = WebRequest.Create(adress) as HttpWebRequest;
            _request.Method = "GET";

            try
            {
                HttpWebResponse response = _request.GetResponse() as HttpWebResponse;
                var stream = response.GetResponseStream();
                if (stream != null) Response = new StreamReader(stream).ReadToEnd();
            }
            catch (Exception)
            {
            }

            var responses = Response;
            var nameJson = JObject.Parse(responses);
            data = nameJson["data"];
        }

    }
}
