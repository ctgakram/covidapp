using AppProj.Domain.ModelExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppProj.Web.Models
{
    public class DashboardModel
    {
        public DashboardModel()
        {
            mapdatas = new List<mapdata>();
        }
        public int Reach { get; set; }
        public int Suspect { get; set; }
        public int SuspectFemale { get; set; }
        public int SuspectApp { get; set; }
        public string Districts { get; set; }
        public List<mapdata> mapdatas { get; set; }


        public DashboardModelBDC DashboardModelBDCs { get; set; }
        public DashboardModelHnpp DashboardModelHnpps { get; set; }

        public List<BepDataSummeryModelReach> BepDataSummeryModelReaches = new List<BepDataSummeryModelReach>();
        public List<BepDataSummeryModelMaterial> BepDataSummeryModelMaterials = new List<BepDataSummeryModelMaterial>();
        public List<BepDataSummeryModelMaterial> BepDataSummeryModelMaterialDistributions = new List<BepDataSummeryModelMaterial>();
        
    }

    public class DashboardModelHnpp
    {
        public int Leaflet { get; set; }
        public int Sticker { get; set; }
        public int MeetingBrac { get; set; }
        public int MeetingGovt { get; set; }
    }
    public class DashboardModelBDC
    {
        public int Patient { get; set; }
        public int Hospital { get; set; }
        public int Bed { get; set; }
        public int Qrn { get; set; }
        public int QrnCurrent { get; set; }
        public int QrnReleased { get; set; }
        public int Test { get; set; }
        public int Death { get; set; }
        public decimal PlanRice { get; set; }
        public decimal PlanMoney { get; set; }
        public decimal DisRice { get; set; }
        public int DisRiceFamily { get; set; }
        public decimal DisMoney { get; set; }
        public int DisMoneyFamily { get; set; }

    }

    public class mapdata
    {
        public mapdata() {

        }
        public KeyValuePair<string,string> keyelement { get; set; }
        public string color { get; set; }
    }
    

}