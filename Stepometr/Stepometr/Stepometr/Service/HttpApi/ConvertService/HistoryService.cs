using Stepometer.Models;
using Stepometer.Service.HttpApi.ConvertService.Contracts;
using Stepometer.Service.HttpApi.Repository;
using Stepometer.Service.HttpApi.UoW;
using Stepometer.Service.LoaclDB;
using Stepometer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Stepometer.Service.HttpApi.ConvertService
{
    public class HistoryService : BaseService, IHistoryService
    {
        private readonly IRestApiClient<HistoryUserParamWebModel> _historyApi;
        private readonly IRestApiClient<AvgHistoryWebModel> _avgHistoryApi;

        private readonly IDBService _dbService;

        public HistoryService(IUnitOfWork uOW) : base(uOW)
        {
            _dbService = DependencyResolver.Get<IDBService>();
        }

        public HistoryService()
        {
            _historyApi = UOW?.HistoryRestApiClient;
            _avgHistoryApi = UOW?.AvgHistoryRestApiClient;

            _dbService = DependencyResolver.Get<IDBService>();
        }
        public async Task<IList<StepometerModel>> GetHistoryData(int amountDayInYear)
        {
            try
            {
                var result = await _avgHistoryApi.GetDataAsync(Constants.Constants.GetHistory);
                var data = result.FirstOrDefault();

                IList<StepometerModel> resultData = new List<StepometerModel>();
                
                foreach (var item in data.AvgDataStepsPerDay)
                {
                    resultData.Add(new StepometerModel
                    {
                        Steps = Convert.ToInt32(item.Steps),
                        Date = item.Date,
                        Calories = item.Calories,
                        Distance = item.Distance,
                        Speed = item.Speed
                    });
                }

                return resultData;
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
                return new List<StepometerModel>() { new StepometerModel() };
            }
        }

        public async Task<IList<HistoryUserParamWebModel>> GetHistoryData()
        {
            try
            {
                var res = await _historyApi.GetDataAsync(Constants.Constants.GetHistoryUserParam);

                return res;
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
                return new List<HistoryUserParamWebModel>() { new HistoryUserParamWebModel() };
            }
        }
    }
}
