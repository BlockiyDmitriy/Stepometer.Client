using System.Collections.Generic;
using System.Threading.Tasks;
using Stepometer.Models;

namespace Stepometer.Service.HttpApi.ConvertService.Contracts
{
    public interface IStepometerService
    {
        Task<List<StepometerModel>> GetData();
        Task<List<StepometerModel>> PostData(StepometerModel data);
        Task<List<StepometerModel>> PutData(StepometerModel data);
        Task<List<StepometerModel>> DeleteData(StepometerModel data);
    }
}
