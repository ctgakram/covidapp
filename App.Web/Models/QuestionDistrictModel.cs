using AppProj.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppProj.Web.Models
{
    public class QuestionDistrictModel
    {
        public int QuestionId { get; set; }
        public IEnumerable<DistrictQuestion> DistrictByQuestion { get; set; }
        public IEnumerable<StandingData> RestDistricts { get; set; }
    }
}