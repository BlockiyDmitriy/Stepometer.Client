using Stepometer.Models;
using System;
using System.Threading.Tasks;

namespace Stepometer.Service.LoaclDB
{
    public interface IDBService
    {
        #region Stepometer

        Task<StepometerModel> SetStepometerDataAsync(StepometerModel stepometerModel);
        Task<StepometerModel> GetStepometerDataAsync();
        Task<StepometerModel> UpdateStepometerDataAsync(StepometerModel stepometerModel);

        #endregion

        #region History

        Task<AvgHistoryWebModel> SetHistoryDataAsync(AvgHistoryWebModel avgHistoryModel);
        Task<AvgHistoryWebModel> GetHistoryDataAsync();
        Task<AvgHistoryWebModel> UpdateHistoryDataAsync(AvgHistoryWebModel avgHistoryModel);

        #endregion

        #region Last Activity Date
        Task<DateTimeOffset> GetLastActivityDate();
        Task UpdateLastActivityDate(DateTimeOffset date);
        #endregion
    }
}
