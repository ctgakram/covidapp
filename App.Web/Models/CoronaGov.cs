using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppProj.Web.Models
{
    public class CoronaGov
    {
        public List<features> features { get; set; }
       // public geometry[] geometry { get; set; }
    }

    //public class geometry
    //{
    //    public properties properties { get; set; }
    //}

    public class features
    {
        public attributes attributes { get; set; }
    }

    public class attributes
    {
        public string district_city_eng { get; set; }
        public int cases { get; set; }
    }
}