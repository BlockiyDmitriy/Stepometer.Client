using Stepometer.Models.Login;
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

        #region Login
        Task<bool> Register(string controllerUrl, RegisterModel registerModel);
        Task<bool> Login(string controllerUrl, LoginModel registerModel);
        Task<string> GetToken(string controllerUrl, LoginModel registerModel);
        #endregion
    }
}