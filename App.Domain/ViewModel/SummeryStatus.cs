using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Domain.ViewModel
{
    public class SummeryStatus
    {
        public string Name { get; set; }
        public string TopItems { get; set; }
        public string TopDisplayItems { get; set; }
        public string Color { get; set; }
        //public List<DistrictMapInfo> ItemCounts { get; set; }
        public List<DistrictMapInfo> DistrictCnt { get; set; }
    }
    public class DistrictMapInfo
    {
        public string Display { get; set; }
        public string NameId { get; set; }
        public int Count { get; set; }
    }

}
