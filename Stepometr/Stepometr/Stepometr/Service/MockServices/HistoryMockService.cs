using Stepometer.Models;
using Stepometer.Service.HttpApi.ConvertService.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stepometer.Service.MockServices
{
    public class HistoryMockService
    {
        public async Task<IList<StepometerModel>> GetHistoryData()
        {
            IList<StepometerModel> resultData = new List<StepometerModel>();
            for (int i = 0; i < 200; i++)
            {
                resultData.Add(new StepometerModel
                {
                    Steps = (int) (i % 2 == 0 ? i * 0.3d : i / 0.3d),
                    Date = DateTime.Today.AddDays(-i),
                    Calories = 1233,
                    Distance = 20000,
                    Speed = 8.3
                });
            }

            return resultData;
        }

        public Task<IList<HistoryUserParamWebModel>> GetHistoryUserParamData()
        {
            IList<HistoryUserParamWebModel> result = new List<HistoryUserParamWebModel>();
            return Task.FromResult(result);
        }
    }
}