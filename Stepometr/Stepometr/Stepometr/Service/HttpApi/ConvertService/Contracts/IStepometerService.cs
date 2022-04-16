using System.Collections.Generic;
using System.Threading.Tasks;
using Stepometer.Models;

namespace Stepometer.Service.HttpApi.ConvertService.Contracts
{
    public interface IStepometerService
    {
        Task<List<StepometerModel>> GetData();
        Task<StepometerModel> PostData(StepometerModel data);
        Task<StepometerModel> PutData(StepometerModel data);
        Task<StepometerModel> DeleteData(StepometerModel data);
    }
}
