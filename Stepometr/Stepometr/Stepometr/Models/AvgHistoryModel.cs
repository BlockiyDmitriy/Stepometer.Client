using System;
using System.Collections.Generic;
using System.Text;

namespace Stepometer.Models
{
    public class AvgHistoryWebModel
    {
        public IList<StepometerModel> AvgDataStepsPerDay { get; set; }
        public AvgPeriodDataModel AvgDataStepsWeek { get; set; }
        public AvgPeriodDataModel AvgDataStepsMonth { get; set; }
        public AvgPeriodDataModel AvgDataStepsHalfYear { get; set; }
        public AvgPeriodDataModel AvgDataStepsYear { get; set; }
    }
    public class AvgPeriodDataModel
    {
        public double AvgDistance { get; set; } = 0.0;
        public double AvgTimeActivity { get; set; } = 0.0;
        public double AvgCalories { get; set; } = 0.0;
        public double AvgSteps { get; set; } = 0.0;
        public double AvgSpeed { get; set; } = 0.0;

    }
}
