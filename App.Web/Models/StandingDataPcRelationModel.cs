using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppProj.Web.Models
{
    public class StandingDataPcRelationModel
    {
        public int ParentId { get; set; }
        public string Type { get; set; }
        public IEnumerable<StandingDataPcRelationModelDetail> Allowed { get; set; }
        public IEnumerable<StandingDataPcRelationModelDetail> NotAllowed { get; set; }
    }

    public class StandingDataPcRelationModelDetail
    {
        public int Id { get; set; }
        public int ChildId { get; set; }
        public string ChildName { get; set; }
    }

}