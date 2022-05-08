using Stepometer.Models.Login;
using Stepometer.Service.LoaclDB;
using Stepometer.Service.LoggerService;
using Stepometer.Utils;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Stepometer.Service.LoginServices
{
    public class LoginService : ILoginService
    {
        private readonly IDBService _dbService;
        private readonly ILogService _logService;

        public LoginService()
        {
            _dbService = DependencyResolver.Get<IDBService>();
            _logService = DependencyResolver.Get<ILogService>();
        }

        public async Task<LoginModel> CreateNewAccount(LoginModel loginModel)
        {
            try
            {
                var accData = await _dbService.SetNewLoginData(loginModel);

                if (accData is not null && !accData.IsExistAccount)
                {
                    //Clean local db
                }
                if (accData == null)
                {
                    throw new Exception("Error create account");
                }
                return accData;
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
                return null;
            }
        }
    }
}
