using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Stepometer.Service.HttpApi.Repository
{
    public class RestApiClient<TData> : IRestApiClient<TData> where TData : class
    {
        internal HttpClient HttpClient { get; private set; }
        private readonly StringBuilder _baseUrlStringBuilder = new StringBuilder(Constants.Constants.BaseUrl);
        public RestApiClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<TData> GetDataAsync(string controllerUrl)
        {
            var response = await HttpClient.GetAsync(_baseUrlStringBuilder.Append(controllerUrl).ToString());
            if (response.IsSuccessStatusCode)
            {
                var result = SerializeDeserialize<TData>.ConvertFromJson(await response.Content.ReadAsStringAsync());
                return result;
            }

            return null;
        }

        public async void PostDataAsync(string controllerUrl, TData data)
        {
            if (data != null)
            {
                await HttpClient.PostAsync(_baseUrlStringBuilder.Append(controllerUrl).ToString(),
                    new StringContent(SerializeDeserialize<TData>.ConvertToJson(data)));
            }
        }

        public async void PutDataAsync(string controllerUrl, TData data)
        {
            if (data != null)
            {
                await HttpClient.PutAsync(_baseUrlStringBuilder.Append(controllerUrl).ToString(),
                    new StringContent(SerializeDeserialize<TData>.ConvertToJson(data)));
            }
        }

        public async void DeleteDataAsync(string controllerUrl, TData data)
        {
            if (data != null)
            {
                await HttpClient.DeleteAsync(_baseUrlStringBuilder.Append(controllerUrl).ToString());
            }
        }
    }
}