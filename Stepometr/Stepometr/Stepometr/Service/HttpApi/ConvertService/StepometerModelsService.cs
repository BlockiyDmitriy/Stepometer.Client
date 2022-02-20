using Stepometer.Models;
using Stepometer.Service.HttpApi.ConvertService.Contracts;
using Stepometer.Service.HttpApi.UoW;
using System;
using System.Threading.Tasks;

namespace Stepometer.Service.HttpApi.ConvertService
{
    public class StepometerModelsService : AbstractService, IStepometerModelsService
    {
        private readonly string _controllerUrl = Constants.Constants.StepometerControllerName;
        public StepometerModelsService(IUnitOfWork uOW) : base(uOW)
        {
        }
        public StepometerModelsService()
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

        ~StepometerModelsService()
        {
            Dispose(false);
        }
    }
}
