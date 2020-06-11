using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppProj.Web.Helpers
{
    public class PostData
    {
        public string strStaffPIN { get; set; }
    }

    public class StaffProfile
    {
        public string pin { get; set; }
        public string StaffName { get; set; }
        public string Designationname { get; set; }
        public string EmailID { get; set; }
        public string MobileNo { get; set; }
        public Nullable<DateTime> dateofbirth { get; set; }
        public string sex { get; set; }
        public string projectname { get; set; }
        public string branchname { get; set; }
        public string districtname { get; set; }

        public string Grade { get; set; }
        public string PermanentAddressDistrictName { get; set; }
        public Nullable<System.DateTime> JoiningDate { get; set; }
        public Nullable<System.DateTime> TransferDate { get; set; }
        public string UpazilaName { get; set; }
    }
}