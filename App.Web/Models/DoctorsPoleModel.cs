using AppProj.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppProj.Web.Models
{
    public class DoctorsPoleShortModel
    {
        public string PIN_ { get; set; }
        
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{5})$",
                   ErrorMessage = "Wrong mobile number has given")]
        public string MobileNo_ { get; set; }
    }
    public class DoctorsPoleModel
    {
        public int Id { get; set; }
        public string PIN { get; set; }
        public string EntryByDept { get; set; }
        public Nullable<System.DateTime> FirstDoctorCallTime { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{5})$",
                   ErrorMessage = "Wrong mobile number has given")]
        public string MobileNo { get; set; }
        public Nullable<System.DateTime> Dob { get; set; }
        public Nullable<int> GenderId { get; set; }
        public int ProgramId { get; set; }
        public string AreaOffice { get; set; }
        public int DivisionId { get; set; }
        public int DistrictId { get; set; }
        public int EffectedPersonId { get; set; }
        public string CommentByHR { get; set; }
        public bool IsTravel { get; set; }
        public bool IsTravelCountry { get; set; }
        public bool IsTravelAbroad { get; set; }
        public bool IsContract { get; set; }
        public bool IsFever { get; set; }
        public bool IsDryCough { get; set; }
        public bool IsSoreThroat { get; set; }
        public bool IsBreathing { get; set; }
        public string OtherSymptoms { get; set; }
        public Nullable<bool> AdvConTreat { get; set; }
        public Nullable<bool> Adv { get; set; }
        public Nullable<bool> AdvQurentine { get; set; }
        public Nullable<bool> AdvHomeIsolation { get; set; }
        public Nullable<bool> AdvReferHospital { get; set; }
        public Nullable<bool> IsAntibioticTaken { get; set; }
        public int StatusId { get; set; }
        public string Comments { get; set; }
        public Nullable<int> CoMorbidityId { get; set; }
        public Nullable<System.DateTime> SampleTakenDate { get; set; }
        public Nullable<int> TestResultId { get; set; }
        public Nullable<System.DateTime> TestResultDate { get; set; }
        public Nullable<int> AdmittedTypeId { get; set; }
        public Nullable<System.DateTime> NextFollowupDate { get; set; }
        public string HospitalName { get; set; }
        public Nullable<System.DateTime> AdmissionDate { get; set; }
        public Nullable<System.DateTime> DischargeDate { get; set; }
        public string AlternatePhoneNo { get; set; }
        public string AlternateName { get; set; }        
        public int EntryById { get; set; }
        public System.DateTime EntryTime { get; set; }
        public string AdviceTxt { get; set; }
        public string AntibioticTxt { get; set; }
        public Nullable<int> IsolationOfficeId { get; set; }
        public Nullable<System.DateTime> DeathTime { get; set; }
        public Nullable<int> CouncilorUserId { get; set; }

        [Required(ErrorMessage = "Required")]
        [Range(0, 120, ErrorMessage = "(age within 0 to 120)")]
        public int Age { get; set; }

        public string StaffName { get; set; }
        public string Designation { get; set; }
        public Nullable<System.DateTime> SampleTakenDate1 { get; set; }
        public Nullable<int> TestResultId1 { get; set; }
        public Nullable<System.DateTime> TestResultDate1 { get; set; }
        public Nullable<System.DateTime> SampleTakenDate2 { get; set; }
        public Nullable<int> TestResultId2 { get; set; }
        public Nullable<System.DateTime> TestResultDate2 { get; set; }

        public string ProgramName { get; set; }
        public string DivisionName { get; set; }
        public string DistrictName { get; set; }
        public string GenderName { get; set; }
        public string Message { get; set; }


        public bool IsFound { get; set; }
        public IEnumerable<SelectListItem> ProgramList { get; set; }
        public IEnumerable<SelectListItem> DivisionList { get; set; }
        public IEnumerable<SelectListItem> DistrictList { get; set; }
        public IEnumerable<SelectListItem> GenderList { get; set; }
        public IEnumerable<SelectListItem> EffectedPersonList { get; set; }
        public IEnumerable<SelectListItem> StatusList { get; set; }
        public IEnumerable<SelectListItem> CoMobList { get; set; }
        public IEnumerable<SelectListItem> TestResultList { get; set; }
        public IEnumerable<SelectListItem> AdmitList { get; set; }
        public IEnumerable<SelectListItem> IsolationOffices { get; set; }
        public IEnumerable<SelectListItem> Councilors { get; set; }


        public List<DoctorsPole> ExistingData { get; set; }

        

    }

    public class DoctorsPoleVisitModel
    {
        public int DoctorPoleId { get; set; }
        public bool IsFever { get; set; }
        public bool IsDryCough { get; set; }
        public bool IsSoreThroat { get; set; }
        public bool IsBreathing { get; set; }
        public string OtherSymptoms { get; set; }
        public bool AdvConTreat { get; set; }
        public bool Adv { get; set; }
        public bool AdvQurentine { get; set; }
        public bool AdvHomeIsolation { get; set; }
        public bool AdvReferHospital { get; set; }
        public bool IsAntibioticTaken { get; set; }
        public int StatusId { get; set; }
        public string Comments { get; set; }
        public Nullable<int> CoMorbidityId { get; set; }
        public Nullable<System.DateTime> SampleTakenDate { get; set; }
        public Nullable<int> TestResultId { get; set; }
        public Nullable<System.DateTime> TestResultDate { get; set; }
        public Nullable<int> AdmittedTypeId { get; set; }
        public string HospitalName { get; set; }
        public Nullable<System.DateTime> AdmissionDate { get; set; }
        public Nullable<System.DateTime> DischargeDate { get; set; }
        public string AlternatePhoneNo { get; set; }
        public string AlternateName { get; set; }
        public int EntryById { get; set; }
        public System.DateTime EntryTime { get; set; }
        public Nullable<int> LastUpdateById { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<System.DateTime> SampleTakenDate1 { get; set; }
        public Nullable<int> TestResultId1 { get; set; }
        public Nullable<System.DateTime> TestResultDate1 { get; set; }
        public Nullable<System.DateTime> SampleTakenDate2 { get; set; }
        public Nullable<int> TestResultId2 { get; set; }
        public Nullable<System.DateTime> TestResultDate2 { get; set; }
        public string AdviceTxt { get; set; }
        public string AntibioticTxt { get; set; }
        public Nullable<int> IsolationOfficeId { get; set; }
        public Nullable<System.DateTime> DeathTime { get; set; }
        public Nullable<System.DateTime> LastCouncilingDate { get; set; }

        [Required(ErrorMessage ="Please select next followup")]
        public Nullable<int> FollowupAfterDays { get; set; }
        public List<SelectListItem> FollowupAfterDaysList { get; set; }

        public IEnumerable<SelectListItem> AdminTypeList { get; set; }
        public IEnumerable<SelectListItem> TestResultList { get; set; }
        public List<SelectListItem> CoMobiList { get; set; }
        public IEnumerable<SelectListItem> StatusList { get; set; }
        public IEnumerable<SelectListItem> IsolationOffices { get; set; }

        public DoctorsPole DoctorsPole { get; set; }
        public List<DoctorsPoleVisit> DoctorsPoleVisits { get; set; }
        public List<DoctorPoleCouncilling> DoctorPoleCouncillings { get; set; }
    }

}