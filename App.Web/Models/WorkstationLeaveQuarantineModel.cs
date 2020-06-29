using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppProj.Domain;

namespace AppProj.Web.Models
{
    public class WorkstationLeaveQuarantineShortModel
    {
        public string PIN_ { get; set; }

        public string Message { get; set; }
    }
    public class WorkstationLeaveQuarantineModel
    {
        [Key]
        public int Id { get; set; }
        public string PIN { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Grade { get; set; }
        public Nullable<int> GenderId { get; set; }
        public string HomeDistrict { get; set; }
        public Nullable<System.DateTime> DateOfJoiningBrac { get; set; }
        public Nullable<System.DateTime> DateOfJoiningCurrentBranch { get; set; }
        public string Branch { get; set; }
        public string Area { get; set; }
        public string Region { get; set; }
        public Nullable<int> UpazillaId { get; set; }
        public Nullable<int> DistrictId { get; set; }
        public Nullable<int> DivisionId { get; set; }
        public Nullable<int> SourceId { get; set; }
        public bool IsWorkStationLeft { get; set; }

      
        public Nullable<System.DateTime> WorkStationLeaveDate { get; set; }
        public Nullable<System.DateTime> WorkStationReturnDate { get; set; }
        public string SupervisorPermitted { get; set; }
        public bool IsStayInQuarantine { get; set; }
        public Nullable<System.DateTime> QuarantineStartDate { get; set; }
        public Nullable<System.DateTime> QuarantineEndDate { get; set; }
        public bool IsCoronaSymptomitic { get; set; }
        public bool IsQuarantineExtended { get; set; }
        public Nullable<System.DateTime> ExtendedQuarantineStartDate { get; set; }
        public Nullable<System.DateTime> ExtendedQuarantineEndDate { get; set; }
        public string InfoCollectedName { get; set; }
        public string InfoCollectedPIN { get; set; }
        public string InfoCollectedDesignation { get; set; }
        public Nullable<System.DateTime> InfoCollectedDate { get; set; }
        public string Comment { get; set; }
        public int EntryById { get; set; }
        public System.DateTime EntryTime { get; set; }
        public Nullable<int> LastUpdateById { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }

        public bool IsInformationFromAnotherPerson { get; set; }

        public string StaffName { get; set; }

        public bool IsFound { get; set; }
        public IEnumerable<SelectListItem> ProgramList { get; set; }
        public IEnumerable<SelectListItem> DivisionList { get; set; }
        public IEnumerable<SelectListItem> DistrictList { get; set; }

        public IEnumerable<SelectListItem> UpazillaList { get; set; }
        public IEnumerable<SelectListItem> GenderList { get; set; }

        public List<WorkstationLeaveQuarantine> ExistingData { get; set; }

        public IEnumerable<SelectListItem> SupervisorPermittedList { get; set; }

        public string Message { get; set; }
        public bool? IsWorkStationLeftByDoctor { get; set; }
        public Nullable<System.DateTime> WorkStationLeftByDoctorDate { get; set; }

    }
}