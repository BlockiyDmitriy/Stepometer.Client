using System;
using System.Collections.Generic;
using System.Text;

namespace Stepometer.Models
{
    public class HistoryUserParamWebModel
    {
        public int Id { get; set; }
        public int Growth { get; set; }
        public int Weight { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public AccModel Account { get; set; }
    }
}
