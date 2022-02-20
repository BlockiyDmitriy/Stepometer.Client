using System.Threading.Tasks;
using Stepometer.Models;

namespace Stepometer.Service.Interfaces
{
    public interface IStepometerService
    {
        public Task<StepometerModel> LoadCurrentStepsData();
        public Task<StepometerModel> UpdateCurrentStepsData(long steps);
    }
}
