using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stepometer.Service.HttpApi.Repository
{
    public interface IRestApiClient<TData> where TData : class
    {
        Task<List<TData>> GetDataAsync(string controllerUrl);
        Task<TData> PostDataAsync(string controllerUrl, TData data);
        Task<TData> PutDataAsync(string controllerUrl, TData data);
        Task<TData> DeleteDataAsync(string controllerUrl, TData data);
    }
}