using Stepometer.Models;
using Stepometer.Service.HttpApi.ConvertService.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stepometer.Service.MockServices
{
    public class StepometerMockService : IStepometerService
    {
        private StepometerModel _stepometerModel;

        public StepometerMockService()
        {
            _stepometerModel = new StepometerModel();
        }

        public Task<StepometerModel> DeleteData(StepometerModel data)
        {
            throw new NotImplementedException();
        }

        public Task<List<StepometerModel>> GetData()
        {
            throw new NotImplementedException();
        }

        public async Task<StepometerModel> LoadCurrentStepsData()
        {
            var stepometerModel = new StepometerModel()
            {
                //Id = Guid.NewGuid().ToString(),
                Distance = 12,
                Calories = 1201,
                Speed = 5.6,
                Steps = 0,
                Date = DateTime.Today

            };
            _stepometerModel = stepometerModel;
            return _stepometerModel;
        }

        public Task<StepometerModel> PostData(StepometerModel data)
        {
            throw new NotImplementedException();
        }

        public Task<StepometerModel> PutData(StepometerModel data)
        {
            throw new NotImplementedException();
        }

        public async Task<StepometerModel> UpdateCurrentStepsData(long steps)
        {
            var stepometerModel = new StepometerModel()
            {
                //Id = Guid.NewGuid().ToString(),
                Distance = _stepometerModel.Distance,
                Calories = _stepometerModel.Calories,
                Speed = _stepometerModel.Speed,
                Steps = Convert.ToInt32(steps),
                Date = _stepometerModel.Date,
                LastActivityDate = DateTimeOffset.Now
            };

            _stepometerModel = stepometerModel;
            return stepometerModel;
        }
    }
}
