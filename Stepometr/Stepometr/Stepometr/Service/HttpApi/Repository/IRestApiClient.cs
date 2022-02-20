using System.Threading.Tasks;

namespace Stepometer.Service.HttpApi.Repository
{
    public interface IRestApiClient<TData> where TData : class
    {
        Task<TData> GetDataAsync(string controllerUrl);
        void PostDataAsync(string controllerUrl, TData data);
        void PutDataAsync(string controllerUrl, TData data);
        void DeleteDataAsync(string controllerUrl, TData data);
    }
}