using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppProj.Web.Models
{
    public class ApplicantApiReturnModel
    {
        public string Message { get; set; }
        public bool Success { get; set; }

        public string OtpKey { get; set; }

        public Nullable<DateTime> DateFor { get; set; }
        
        public IEnumerable<SelectListItem> Kiosks { get; set; }
        public IEnumerable<SelectListItem> Genders { get; set; }
        public IEnumerable<SelectListItem> Relations { get; set; }
        public IEnumerable<SelectListItem> BloodGroups { get; set; }
    }

    public class ApplicantPost
    {
        public string Key { get; set; }
        public string OtpKey { get; set; }
        public string OTP { get; set; }
        public int KioskId { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int GenderId { get; set; }
        public int RelationId { get; set; }
        public int BloodGroupId { get; set; }
        public int Age { get; set; }
        

        public string HrEmail { get; set; }
        public string Staff_Program { get; set; }
        public string Staff_Name { get; set; }
        public string Staff_Pin { get; set; }
        public string Staff_Designation { get; set; }
        public string Staff_Division { get; set; }
        public string Staff_District { get; set; }
        public string Staff_AreaOffice { get; set; }
        public string Staff_Sex { get; set; }
        public string Staff_Age { get; set; }
        public string Staff_Mobile { get; set; }
        public string Staff_Email { get; set; }
        public string Staff_Relation { get; set; }

        public string By_Name { get; set; }
        public string By_Mobile { get; set; }
        public string By_Email { get; set; }
    }
}