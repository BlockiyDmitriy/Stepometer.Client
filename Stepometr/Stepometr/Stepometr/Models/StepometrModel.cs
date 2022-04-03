using System;
using Newtonsoft.Json;

namespace Stepometer.Models
{
    public class StepometerModel
    {
        public int Id { get; set; }
        public int Steps { get; set; } = 0;
        public double Duration { get; set; } = 0.0;
        public DateTime Date { get; set; } = new DateTime();
        public int Distance { get; set; } = 0;
        public int Calories { get; set; } = 0;
        public double Speed { get; set; } = 0.0;
        public DateTimeOffset LastActivityDate { get; set; } = new DateTimeOffset();
        public AccModel Account { get; set; }
    }
}