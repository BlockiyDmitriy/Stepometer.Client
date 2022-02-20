using System;
using Newtonsoft.Json;

namespace Stepometer.Models
{
    public class StepometerModel
    {
        public string Id { get; set; }
        public long Steps { get; set; }
        public DateTime Time { get; set; }
        public long Distance { get; set; }
        public long Calories { get; set; }
        public double Speed { get; set; }
        public DateTimeOffset LastActivityDate { get; set; }
    }
}