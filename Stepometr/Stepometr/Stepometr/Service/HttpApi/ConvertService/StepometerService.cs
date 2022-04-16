using Stepometer.Models;
using Stepometer.Service.HttpApi.ConvertService.Contracts;
using Stepometer.Service.HttpApi.UoW;
using Stepometer.Service.LoaclDB;
using Stepometer.Service.LoggerService;
using Stepometer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Stepometer.Service.HttpApi.ConvertService
{
    public class StepometerService : BaseService, IStepometerService
    {
        private readonly IDBService _dbService;
        public StepometerService(IUnitOfWork uOW) : base(uOW)
        {
            _dbService = DependencyResolver.Get<IDBService>();
        }

        public StepometerService()
        {
            _dbService = DependencyResolver.Get<IDBService>();
        }

        public async Task<List<StepometerModel>> GetData()
        {
            try
            {
                var lastActivityDate = await _dbService.GetLastActivityDate();
                _logService.Log($"Last activity date: {lastActivityDate}");

                List<StepometerModel> result = new();
                if (lastActivityDate == default)
                {
                    _logService.Log("First launch");
                    _logService.Log("Load data from server");
                    result = await UOW?.StepometerRestApiClient.GetDataAsync(Constants.Constants.GetDataSteps);

                    _logService.Log("Update local db data");
                    await _dbService.SetStepometerDataAsync(result.LastOrDefault());

                    _logService.Log("Update last activity date");
                    await _dbService.UpdateLastActivityDate(DateTimeOffset.Now);

                }
                else
                {
                    _logService.Log("Load data local db");
                    var stepometerModel = await _dbService.GetStepometerDataAsync();
                    result = new List<StepometerModel>();
                    result.Add(stepometerModel);
                }

                // If the app was installed before
                if (result.LastOrDefault()?.LastActivityDate.AddDays(1).ToUniversalTime() <= DateTime.Now.ToUniversalTime())
                {
                    _logService.Log("New day reset data");
                    result = new List<StepometerModel>();
                    result.Add(new StepometerModel());
                    return result;
                }

                return result;
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
                return new List<StepometerModel>() { new StepometerModel() };
            }
        }

        public async Task<StepometerModel> PostData(StepometerModel data)
        {
            try
            {
                var serverModel = await UOW?.StepometerRestApiClient.PostDataAsync(Constants.Constants.AddDataSteps, data);
                await _dbService.SetStepometerDataAsync(serverModel);

                _logService.Log("Update last activity date");
                await _dbService.UpdateLastActivityDate(DateTimeOffset.Now);
                return serverModel;
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
                return new StepometerModel();
            }

        }

        public async Task<StepometerModel> PutData(StepometerModel data)
        {
            try
            {
                var lastActivityDate = await _dbService.GetLastActivityDate();
                _logService.Log($"Last activity date: {lastActivityDate}");

                StepometerModel stepometerData = new();

                stepometerData = await _dbService.UpdateStepometerDataAsync(data);
                if (lastActivityDate.AddMinutes(2).ToUniversalTime() <= DateTime.Now.ToUniversalTime())
                {
                    _logService.Log("Update last activity date");
                    await _dbService.UpdateLastActivityDate(DateTimeOffset.Now);

                    _logService.Log("Push data to the server");
                    var res = await UOW?.StepometerRestApiClient.PostDataAsync(Constants.Constants.AddDataSteps, data);
                    if (res is not null)
                    {
                        stepometerData = res;
                    }
                }

                return stepometerData;
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
                return new StepometerModel();
            }
        }

        public Task<StepometerModel> DeleteData(StepometerModel data)
        {
            try
            {
                return UOW?.StepometerRestApiClient.DeleteDataAsync(Constants.Constants.DeleteDataSteps, data);
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
                return Task.FromResult(new StepometerModel());
            }
        }

        public void Dispose()
        {
            base.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~StepometerService()
        {
            Dispose(false);
        }
    }
}
