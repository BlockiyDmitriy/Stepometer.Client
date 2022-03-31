using Stepometer.Models;
using System;
using System.Threading.Tasks;
using Stepometer.Service.Interfaces;

namespace Stepometer.Service.MockServices
{
    public class StepometerMockService : IStepometerService
    {
        private StepometerModel _stepometerModel;

        public StepometerMockService()
        {
            _stepometerModel = new StepometerModel();
        }

        public async Task<StepometerModel> LoadCurrentStepsData()
        {
            var stepometerModel = new StepometerModel()
            {
                Id = Guid.NewGuid().ToString(),
                Distance = 12,
                Calories = 1201,
                Speed = 5.6,
                Steps = 0,
                Date = DateTime.Today

            };
            _stepometerModel = stepometerModel;
            return _stepometerModel;
        }

        public async Task<StepometerModel> UpdateCurrentStepsData(long steps)
        {
            var stepometerModel = new StepometerModel()
            {
                Id = Guid.NewGuid().ToString(),
                Distance = _stepometerModel.Distance,
                Calories = _stepometerModel.Calories,
                Speed = _stepometerModel.Speed,
                Steps = steps,
                Date = _stepometerModel.Date,
                LastActivityDate = DateTimeOffset.Now
            };

            _stepometerModel = stepometerModel;
            return stepometerModel;
        }
    }
}
