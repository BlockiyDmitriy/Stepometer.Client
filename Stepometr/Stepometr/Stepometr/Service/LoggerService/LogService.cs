using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Stepometer.Service.LoggerService
{
    public class LogService : ILogService
    {
        public void Log(string message)
        {
            Debug.WriteLine(DateTime.Now.ToString("dd.MM.yyyy:HH.mm") + " " + message);
        }

        public void TrackException(Exception e)
        {
            Log(e.Message);
        }

        public void TrackException(Exception e, string methodName)
        {
            Log(e.Message);
        }

        public void TrackEvent(string message)
        {
            Log(message);
        }

        public async void TrackResponse(HttpResponseMessage response)
        {
            TrackEvent("request " + response.RequestMessage.RequestUri.ToString());
            TrackEvent("response " + response.StatusCode + " " + await response.Content.ReadAsStringAsync());
        }

        public async Task TrackResponseAsync(HttpResponseMessage response)
        {
            TrackEvent("request " + response.RequestMessage.RequestUri.ToString());
            TrackEvent("response " + response.StatusCode + " " + await response.Content.ReadAsStringAsync());
        }
    }
}
