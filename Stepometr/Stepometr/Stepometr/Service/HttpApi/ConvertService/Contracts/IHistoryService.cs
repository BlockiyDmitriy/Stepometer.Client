using Stepometer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stepometer.Service.HttpApi.ConvertService.Contracts
{
    public interface IHistoryService
    {
        public Task<AvgHistoryWebModel> GetHistoryData();
        public Task<IList<HistoryUserParamWebModel>> GetHistoryUserParamData();
    }
}
