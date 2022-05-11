using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Stepometer.Models.Login;
using Stepometer.Service.LoggerService;
using Stepometer.Utils;

namespace Stepometer.Service.HttpApi.Repository
{
    public class RestApiClient<TData> : IRestApiClient<TData> where TData : class
    {
        private  ILogService _logService { get; }

        internal HttpClient HttpClient { get; private set; }

        private StringBuilder _baseUrlStringBuilder = new StringBuilder(Constants.Constants.BaseUrl);

        public RestApiClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
            _logService = DependencyResolver.Get<ILogService>();
        }

        public async Task<List<TData>> GetDataAsync(string controllerUrl) 
        {
            try
            {
                var response = await HttpClient.GetAsync(_baseUrlStringBuilder.Append(controllerUrl).ToString());

                await _logService.TrackResponseAsync(response);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception();
                }

                return SerializeDeserialize<TData>.ConvertFromJson(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
                return null;
            }
        }

        public async Task<TData> PostDataAsync(string controllerUrl, TData data)
        {
            try
            {
                _baseUrlStringBuilder = new StringBuilder(Constants.Constants.BaseUrl);
                var response = await HttpClient.PostAsync(_baseUrlStringBuilder.Append(controllerUrl).ToString(),
                    new StringContent(SerializeDeserialize<TData>.ConvertToJson(data), Encoding.UTF8, "application/json"));

                await _logService.TrackResponseAsync(response);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception();
                }

                return SerializeDeserialize<TData>.ConvertFromJson(await response.Content.ReadAsStringAsync()).LastOrDefault();
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
                return null;
            }

        }

        public async Task<TData> PutDataAsync(string controllerUrl, TData data)
        {
            try
            {
                _baseUrlStringBuilder = new StringBuilder(Constants.Constants.BaseUrl);
                var response = await HttpClient.PutAsync(_baseUrlStringBuilder.Append(controllerUrl).ToString(),
                    new StringContent(SerializeDeserialize<TData>.ConvertToJson(data)));

                await _logService.TrackResponseAsync(response);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception();
                }

                return SerializeDeserialize<TData>.ConvertFromJson(await response.Content.ReadAsStringAsync()).LastOrDefault();
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
                return null;
            }
        }

        public async Task<TData> DeleteDataAsync(string controllerUrl, TData data)
        {
            try
            {
                _baseUrlStringBuilder = new StringBuilder(Constants.Constants.BaseUrl);
                var response = await HttpClient.DeleteAsync(_baseUrlStringBuilder.Append(controllerUrl).ToString());

                await _logService.TrackResponseAsync(response);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception();
                }

                return SerializeDeserialize<TData>.ConvertFromJson(await response.Content.ReadAsStringAsync()).LastOrDefault();
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
                return null;
            }
        }

        public async Task<bool> Register(string controllerUrl, RegisterModel registerModel)
        {
            try
            {
                _baseUrlStringBuilder = new StringBuilder(Constants.Constants.BaseUrl);
                var response = await HttpClient.PostAsync(_baseUrlStringBuilder.Append(controllerUrl).ToString(),
                    new StringContent(SerializeDeserialize<RegisterModel>.ConvertToJson(registerModel), Encoding.UTF8, "application/json"));

                await _logService.TrackResponseAsync(response);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception();
                }
                return true;
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
                return false;
            }
        }

        public async Task<bool> Login(string controllerUrl, LoginModel loginModel)
        {
            try
            {
                _baseUrlStringBuilder = new StringBuilder(Constants.Constants.BaseUrl);

                var authData = string.Format("{0}:{1}", loginModel.Email, loginModel.Password);
                var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);


                //var response = await HttpClient.PostAsync(_baseUrlStringBuilder.Append(controllerUrl).ToString(),
                //    new StringContent(SerializeDeserialize<LoginModel>.ConvertToJson(loginModel), Encoding.UTF8, "application/x-www-form-urlencoded"));

                await _logService.TrackResponseAsync(response);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception();
                }
                return true;
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
                return false;
            }
        }

        public async Task<string> GetToken(string controllerUrl, LoginModel loginModel)
        {
            try
            {
                _baseUrlStringBuilder = new StringBuilder(Constants.Constants.BaseUrl);
                var response = await HttpClient.GetAsync(_baseUrlStringBuilder.Append(controllerUrl).ToString());

                await _logService.TrackResponseAsync(response);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception();
                }

                return SerializeDeserialize<string>.ConvertFromJson(await response.Content.ReadAsStringAsync()).FirstOrDefault();
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
                return string.Empty;
            }
        }
    }
}