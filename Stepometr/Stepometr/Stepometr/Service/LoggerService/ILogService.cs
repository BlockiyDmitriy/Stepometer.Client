using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Stepometer.Service.LoggerService
{
    public interface ILogService
    {
        public void Log(string message);
        public void TrackException(Exception e);
        public void TrackException(Exception e, string methodName);
        public void TrackEvent(string message);
        public void TrackResponse(HttpResponseMessage response);
        public Task TrackResponseAsync(HttpResponseMessage response);
    }
}
