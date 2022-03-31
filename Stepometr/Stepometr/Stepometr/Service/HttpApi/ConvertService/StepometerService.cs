using Stepometer.Models;
using Stepometer.Service.HttpApi.ConvertService.Contracts;
using Stepometer.Service.HttpApi.UoW;
using Stepometer.Service.LoaclDB;
using Stepometer.Utils;
using System;
using System.Collections.Generic;
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
                var stepometerModel = await _dbService.GetStepometerDataAsync();
                if (stepometerModel == null)
                {
                    return new List<StepometerModel>();
                }
                return await UOW?.StepometerRestApiClient.GetDataAsync(Constants.Constants.GetDataSteps);
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
                throw e;
            }
        }

        public Task<List<StepometerModel>> PostData(StepometerModel data)
        {
            try
            {
                return UOW?.StepometerRestApiClient.PostDataAsync(Constants.Constants.AddDataSteps, data);
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
                throw e;
            }

        }

        public Task<List<StepometerModel>> PutData(StepometerModel data)
        {
            try
            {
                return UOW?.StepometerRestApiClient.PutDataAsync(Constants.Constants.UpdateDataStepsById, data);
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
                throw e;
            }
        }

        public Task<List<StepometerModel>> DeleteData(StepometerModel data)
        {
            try
            {
                return UOW?.StepometerRestApiClient.DeleteDataAsync(Constants.Constants.DeleteDataSteps, data);
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
                throw e;
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
