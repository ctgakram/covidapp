using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Domain.ModelExt
{
    public class MapModel
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }

        public MapModelIndex MapModelIndex { get; set; }
        public MapModelBrac MapModelBrac { get; set; }
    }

    public class MapModelBrac
    {
        public string Food { get; set; }
        public string Money { get; set; }
    }

    public class MapModelIndex
    {
        public int IndexPopulation { get; set; }
        public int IndexHealth { get; set; }
        public int IndexRelief { get; set; }
    }

}
