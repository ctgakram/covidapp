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
        public int Qnt { get; set; }
        public int ExpQnt { get; set; }
        public int Reach { get; set; }
        public int Male { get; set; }
        public int Female { get; set; }

    }

}
