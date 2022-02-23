using Stepometer.Models;
using Stepometer.Service.HttpApi.ConvertService.Contracts;
using Stepometer.Service.HttpApi.UoW;
using System;
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
            return UOW?.StepometerRestApiClient.GetDataAsync(_controllerUrl);
        }

        public void PostData(StepometerModel data)
        {
            if (data != null)
            {
                UOW?.StepometerRestApiClient.PostDataAsync(_controllerUrl, data);
            }
        }

        public void PutData(StepometerModel data)
        {
            if (data != null)
            {
                UOW?.StepometerRestApiClient.PutDataAsync(_controllerUrl, data);
            }
        }

        public void DeleteData(StepometerModel data)
        {
            if (data != null)
            {
                UOW?.StepometerRestApiClient.DeleteDataAsync(_controllerUrl, data);
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
