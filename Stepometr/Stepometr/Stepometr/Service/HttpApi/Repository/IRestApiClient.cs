using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stepometer.Service.HttpApi.Repository
{
    public interface IRestApiClient<TData> where TData : class
    {
        Task<List<TData>> GetDataAsync(string controllerUrl);
        Task<List<TData>> PostDataAsync(string controllerUrl, TData data);
        Task<List<TData>> PutDataAsync(string controllerUrl, TData data);
        Task<List<TData>> DeleteDataAsync(string controllerUrl, TData data);
    }
}