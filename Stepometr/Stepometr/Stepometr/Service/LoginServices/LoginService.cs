using Stepometer.Models.Login;
using Stepometer.Service.HttpApi.ConvertService;
using Stepometer.Service.HttpApi.UoW;
using Stepometer.Service.LoaclDB;
using Stepometer.Service.LoggerService;
using Stepometer.Utils;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Stepometer.Service.LoginServices
{
    public class LoginService : BaseService, ILoginService
    {
        private readonly IDBService _dbService;
        private readonly ILogService _logService;

        public LoginService(IUnitOfWork uOW) : base(uOW)
        {
            _dbService = DependencyResolver.Get<IDBService>();
            _logService = DependencyResolver.Get<ILogService>();
        }
        public LoginService()
        {
            _dbService = DependencyResolver.Get<IDBService>();
            _logService = DependencyResolver.Get<ILogService>();
        }

        public async Task<bool> CreateNewAccount(RegisterModel registerModel)
        {
            try
            {
                var isCreatedAccount = await UOW?.LoginRestApiClient.Register(Constants.Constants.Register, registerModel);
                
                return isCreatedAccount;
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
                return false;
            }
        }

        public async Task<bool> Login(LoginModel loginModel)
        {
            try
            {
                var isLoginAccount = await UOW?.LoginRestApiClient.Login(Constants.Constants.Login, loginModel);

                return isLoginAccount;
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
                return false;
            }
        }

        public async Task<string> GetToken(LoginModel loginModel)
        {
            try
            {
                var token = await UOW?.LoginRestApiClient.GetToken(Constants.Constants.GetToken, loginModel);

                _logService.TrackEvent("Get token: " + token);

                return token;
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
                return string.Empty;
            }
        }
    }
}
