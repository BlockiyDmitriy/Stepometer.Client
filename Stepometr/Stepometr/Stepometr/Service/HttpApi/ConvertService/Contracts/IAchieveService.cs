using Stepometer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stepometer.Service.HttpApi.ConvertService.Contracts
{
    public interface IAchieveService
    {
        Task<IList<AchieveModel>> GetAllAchieve();
        Task<AchieveModel> GetAchieveById(string id);
    }
}
