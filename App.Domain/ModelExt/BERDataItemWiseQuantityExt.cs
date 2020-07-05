using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Domain.ModelExt
{
    public class BERDataItemWiseQuantityExt
    {
        public string Program { get; set; }
        public string Division { get; set; }
        public string District { get; set; }
        public string Item { get; set; }
        public string Activity { get; set; }
        public decimal Quantity { get; set; }
        public decimal ExpQuantity { get; set; }
        public int ReachCount { get; set; }
        public int ReachCountFemale { get; set; }

        public decimal ResQuantity { get; set; }
        public int ResReachCount { get; set; }

        public int Cat1NewReach { get; set; }
        public int Cat1OldReach { get; set; }

        public int Cat2NewReach { get; set; }
        public int Cat2OldReach { get; set; }

        public int Cat3NewReach { get; set; }
        public int Cat3OldReach { get; set; }

        public int Cat4NewReach { get; set; }
        public int Cat4OldReach { get; set; }

        public int Cat5NewReach { get; set; }
        public int Cat5OldReach { get; set; }

        public int Cat6NewReach { get; set; }
        public int Cat6OldReach { get; set; }

        public int Cat7NewReach { get; set; }
        public int Cat7OldReach { get; set; }

        public int Cat8NewReach { get; set; }
        public int Cat8OldReach { get; set; }

    }
}
