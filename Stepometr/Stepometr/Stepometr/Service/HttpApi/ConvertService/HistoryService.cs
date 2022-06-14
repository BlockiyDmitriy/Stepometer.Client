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
        public async Task<AvgHistoryWebModel> GetHistoryData()
        {
            try
            {
                var result = await _avgHistoryApi.GetDataAsync(Constants.Constants.GetHistory);

                var data = new AvgHistoryWebModel();

                if (result != null)
                {
                    data = await _dbService.SetHistoryDataAsync(result.FirstOrDefault());
                }
                else
                {
                    data = await _dbService.GetHistoryDataAsync();
                }

                return data;
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
                return new AvgHistoryWebModel();
            }
        }

        public async Task<IList<HistoryUserParamWebModel>> GetHistoryUserParamData()
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
