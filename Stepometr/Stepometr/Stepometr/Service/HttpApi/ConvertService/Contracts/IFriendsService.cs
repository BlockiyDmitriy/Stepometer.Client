using Stepometer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stepometer.Service.HttpApi.ConvertService.Contracts
{
    public interface IFriendsService
    {
        Task<List<FriendsModel>> GetAllFriends();
        Task<FriendsModel> GetFriendsById(string id);
    }
}
