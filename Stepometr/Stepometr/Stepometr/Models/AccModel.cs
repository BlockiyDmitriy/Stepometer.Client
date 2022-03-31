using System;
using System.Collections.Generic;
using System.Text;

namespace Stepometer.Models
{
    public class AccModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Lastname { get; set; }

        public ICollection<FriendsModel> Friends { get; set; }
        public ICollection<HistoryUserParamWebModel> HistoryUserParams { get; set; }
        public ICollection<AchieveModel> Achieves { get; set; }
        public ICollection<StepometerModel> DataSteps { get; set; }
    }
}
