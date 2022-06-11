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
                Id = Guid.NewGuid().ToString(),
                Name = "Libby",
                LastName = "Walsh",
                SurName = "",
                PhoneNumber = "(696) 250-8946"
            });
            friends.Add(new FriendsModel()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Rishi ",
                LastName = "Krueger",
                SurName = "",
                PhoneNumber = "(767) 652-1870"
            });
            friends.Add(new FriendsModel()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Emery",
                LastName = "Hughes",
                SurName = "",
                PhoneNumber = "(543) 678-8902"
            });
            friends.Add(new FriendsModel()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Keegan",
                LastName = "Levine",
                SurName = "Keelev",
                PhoneNumber = "(845) 533-3899"
            });
            friends.Add(new FriendsModel()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Erik",
                LastName = "Griffin",
                SurName = "Griffindeo",
                PhoneNumber = "(617) 868-5131"
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