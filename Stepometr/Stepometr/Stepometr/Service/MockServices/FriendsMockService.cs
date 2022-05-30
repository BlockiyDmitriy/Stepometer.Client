using Stepometer.Models;
using Stepometer.Service.HttpApi.ConvertService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stepometer.Service.MockServices
{
    public class FriendsMockService : IFriendsService
    {
        private List<FriendsModel> _friends;

        public FriendsMockService()
        {
            _friends = new List<FriendsModel>();
        }

        public Task<List<FriendsModel>> GetAllFriends()
        {
            var friends = new List<FriendsModel>();
            friends.Add(new FriendsModel()
            {
                Id = "0",
                Name = "name1",
                LastName = "lastName1",
                SurName = "surName1",
                PhoneNumber = "1111111"
            });
            friends.Add(new FriendsModel()
            {
                Id = "1",
                Name = "name2",
                LastName = "lastName2",
                SurName = "surName2",
                PhoneNumber = "2222222"
            });
            friends.Add(new FriendsModel()
            {
                Id = "2",
                Name = "name3",
                LastName = "lastName3",
                SurName = "surName3",
                PhoneNumber = "3333333"
            });
            friends.Add(new FriendsModel()
            {
                Id = "3",
                Name = "name4",
                LastName = "lastName4",
                SurName = "surName4",
                PhoneNumber = "4444444"
            });
            friends.Add(new FriendsModel()
            {
                Id = "4",
                Name = "name5",
                LastName = "lastName5",
                SurName = "surName5",
                PhoneNumber = "5555555"
            });
            _friends.AddRange(friends);

            return Task.FromResult(friends);
        }

        public Task<FriendsModel> GetFriendsById(string id)
        {
            var result = _friends.Where(m => m.Id == id).ToList().FirstOrDefault();
            return Task.FromResult(result);
        }
    }
}