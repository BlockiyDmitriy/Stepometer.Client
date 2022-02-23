using System.Threading.Tasks;

namespace Stepometer.Service.HttpApi.Repository
{
    public interface IRestApiClient<TData> where TData : class
    {
        Task<TData> GetDataAsync(string controllerUrl);
        Task PostDataAsync(string controllerUrl, TData data);
        Task PutDataAsync(string controllerUrl, TData data);
        Task DeleteDataAsync(string controllerUrl, TData data);
    }
}