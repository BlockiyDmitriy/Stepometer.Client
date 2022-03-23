using Stepometer.Models;
using Stepometer.Service.HttpApi.ConvertService.Contracts;
using Stepometer.Service.HttpApi.UoW;
using System;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Stepometer.Service.HttpApi.ConvertService
{
    public class StepometerService : BaseService, IStepometerService
    {
        public StepometerService(IUnitOfWork uOW) : base(uOW)
        {
        }

        public StepometerService()
        {
        }

        public Task<StepometerModel> GetData()
        {
            try
            {
                return UOW?.StepometerRestApiClient.GetDataAsync(Constants.Constants.GetDataSteps);
            }
            catch (Exception e)
            {
                _logService.TrackException(e, MethodBase.GetCurrentMethod()?.Name);
                throw e;
            }
        }

        public Task<StepometerModel> PostData(StepometerModel data)
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

        public Task<StepometerModel> PutData(StepometerModel data)
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

        public Task<StepometerModel> DeleteData(StepometerModel data)
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
