using Stepometer.Models;
using Stepometer.Service.HttpApi.ConvertService.Contracts;
using Stepometer.Service.HttpApi.UoW;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Stepometer.Service.HttpApi.ConvertService
{
    public class StepometerService : BaseService, IStepometerService
    {
        private readonly string _controllerUrl = Constants.Constants.StepometerControllerName;

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
                return UOW?.StepometerRestApiClient.GetDataAsync(_controllerUrl);
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
                return UOW?.StepometerRestApiClient.PostDataAsync(_controllerUrl, data);
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
                return UOW?.StepometerRestApiClient.PutDataAsync(_controllerUrl, data);
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
                return UOW?.StepometerRestApiClient.DeleteDataAsync(_controllerUrl, data);
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
