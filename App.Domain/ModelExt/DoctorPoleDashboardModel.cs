using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Domain.ModelExt
{
    public class DoctorPoleDashboardModel
    {
        public int TotalCases { get; set; }
        public List<DoctorPoleDashboardDetailModel> ProgramWiseCases { get; set; }
        public List<DoctorPoleDashboardDetailModel> SexWiseCases { get; set; }
        public List<DoctorPoleDashboardDetailModel> AgeWiseCases { get; set; }
        public List<DoctorPoleDashboardDetailModel> DistrictWiseCases { get; set; }
        public List<DoctorPoleDashboardDetailModel> DistrictWiseCases_MinVariable { get; set; }

        public int TotalPositive { get; set; }
        public int TotalNegetive { get; set; }
        public int TotalResolved { get; set; }
        public int TotalTestPending { get; set; }
        public int TotalHomeIsolation { get; set; }
        public int TotalHospitalized { get; set; }
        public int CurrentPositive { get; set; }
        public int TotalDeath { get; set; }
        public List<DoctorPoleDashboardDetailModel> ProgramWiseCurrentPositiveCases { get; set; }
        public List<DoctorPoleDashboardDetailModel> AgeWiseCurrentPositiveCases { get; set; }
        public List<DoctorPoleDashboardDetailModel> DistrictWiseCurrentPositiveCases { get; set; }
        public List<DoctorPoleDashboardDetailModel> SexWiseCurrentPositiveCases { get; set; }

        public List<DoctorPoleDashboardDetailModel> StatusOfPositiveCases { get; set; }
        public List<DoctorPoleDashboardDetailModel> TestingStatus { get; set; }
        
    }

    public class DoctorPoleDashboardDetailModel
    {
        public string Particular { get; set; }
        public decimal Value { get; set; }
        public decimal Percent { get; set; }
    }
}
