using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Stepometer.Models;

namespace Stepometer.Service.LoaclDB
{
    public interface IDBService
    {
        #region Stepometer

        Task<StepometerModel> SetStepometerDataAsync(StepometerModel stepometerModel);
        Task<StepometerModel> GetStepometerDataAsync();
        Task<StepometerModel> UpdateStepometerDataAsync(StepometerModel stepometerModel);
        Task UpdateLastActivityDate(DateTimeOffset date);

        #endregion
    }
}
