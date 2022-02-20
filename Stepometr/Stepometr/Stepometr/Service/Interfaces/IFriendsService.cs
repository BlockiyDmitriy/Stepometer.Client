using System.Collections.Generic;
using System.Threading.Tasks;
using Stepometer.Models;

namespace Stepometer.Service.Interfaces
{
    public interface IFriendsService
    {
        Task<List<FriendsModel>> GetAllFriends();
        Task<FriendsModel> GetFriendsById(string id);
    }
}