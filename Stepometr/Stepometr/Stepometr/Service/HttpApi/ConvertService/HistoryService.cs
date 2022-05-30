using Stepometer.Models;
using Stepometer.Service.HttpApi.ConvertService.Contracts;
using Stepometer.Service.HttpApi.Repository;
using Stepometer.Service.HttpApi.UoW;
using Stepometer.Service.LoaclDB;
using Stepometer.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Stepometer.Service.HttpApi.ConvertService
{
    public class HistoryService : BaseService, IHistoryService
    {
        private readonly IRestApiClient<HistoryUserParamWebModel> _historyApi;

        private readonly IDBService _dbService;

        public HistoryService(IUnitOfWork uOW) : base(uOW)
        {
            _dbService = DependencyResolver.Get<IDBService>();
        }

        public HistoryService()
        {
            _historyApi = UOW?.HistoryRestApiClient;

            _dbService = DependencyResolver.Get<IDBService>();
        }
        public async Task<IList<StepometerModel>> GetHistoryData(int amountDayInYear)
        {
            try
            {

                return new List<StepometerModel>() { new StepometerModel() };
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
                return new List<StepometerModel>() { new StepometerModel() };
            }
        }
    }
}
