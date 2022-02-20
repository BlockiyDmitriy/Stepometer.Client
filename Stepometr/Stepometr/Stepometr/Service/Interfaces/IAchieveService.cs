using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Stepometer.Models;

namespace Stepometer.Service.Interfaces
{
    public interface IAchieveService
    {
        Task<IList<AchieveModel>> GetAllAchieve();
        Task<AchieveModel> GetAchieveById(string id);
    }
}
