using Stepometer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stepometer.Service.Interfaces
{
    public interface IHistoryService
    {
        public Task<IList<StepometerModel>> GetHistoryData(int amountDayInYear);
    }
}