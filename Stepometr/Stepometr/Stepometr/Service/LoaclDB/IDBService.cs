using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Stepometer.Models;
using Stepometer.Models.Login;

namespace Stepometer.Service.LoaclDB
{
    public interface IDBService
    {
        #region Login

        Task<LoginModel> SetNewLoginData(LoginModel loginModel);
        Task<LoginModel> GetLoginData(LoginModel loginModel);

        #endregion

        #region Stepometer

        Task<StepometerModel> SetStepometerDataAsync(StepometerModel stepometerModel);
        Task<StepometerModel> GetStepometerDataAsync();
        Task<StepometerModel> UpdateStepometerDataAsync(StepometerModel stepometerModel);

        #endregion

        #region Last Activity Date
        Task<DateTimeOffset> GetLastActivityDate();
        Task UpdateLastActivityDate(DateTimeOffset date);
        #endregion
    }
}
