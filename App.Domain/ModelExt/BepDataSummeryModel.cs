using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Domain.ModelExt
{
    public class BepDataSummeryModelReach
    {
        public string Activity { get; set; }
        public int Male { get; set; }
        public int Female { get; set; }
        public int Boy { get; set; }
        public int Girl { get; set; }
        public int PWD { get; set; }
        public int HHS { get; set; }
        public int Pregnant { get; set; }
    }

    public class BepDataSummeryModelMaterial
    {
        public string Activity { get; set; }
        public string Item { get; set; }
        public decimal Qnt { get; set; }
        public decimal ExpQnt { get; set; }
        public int Reach { get; set; }
        public int Male { get; set; }
        public int Female { get; set; }

    }

    public class BepDataSummeryModelFixedMaterial
    {
        public decimal Festun { get; set; }
        public decimal Miking { get; set; }

        public decimal Sanitizer { get; set; }
        public decimal Gloves { get; set; }
        public decimal Aproan { get; set; }
        public decimal Mask { get; set; }

    }

}
