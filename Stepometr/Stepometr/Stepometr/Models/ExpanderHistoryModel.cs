using System;
using System.Collections.Generic;
using System.Text;

namespace Stepometer.Models
{
    public class ExpanderHistoryModel
    {
        public string Title { get; set; }
        public IList<StepometerModel> ExpanderContent { get; set; }
    }
}
