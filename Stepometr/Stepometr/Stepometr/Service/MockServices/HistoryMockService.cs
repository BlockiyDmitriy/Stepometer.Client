using Stepometer.Models;
using Stepometer.Service.HttpApi.ConvertService.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stepometer.Service.MockServices
{
    public class HistoryMockService : IHistoryService
    {
        public async Task<IList<StepometerModel>> GetHistoryData(int amountDayInYear)
        {
            IList<StepometerModel> resultData = new List<StepometerModel>();
            for (int i = 0; i < amountDayInYear; i++)
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
    }
}