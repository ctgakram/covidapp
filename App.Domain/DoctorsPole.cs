//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppProj.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class DoctorsPole
    {
        public DoctorsPole()
        {
            this.DoctorPoleCouncillings = new HashSet<DoctorPoleCouncilling>();
            this.DoctorPoleStatuses = new HashSet<DoctorPoleStatus>();
            this.DoctorsPoleVisits = new HashSet<DoctorsPoleVisit>();
        }
    
        public int Id { get; set; }
        public string PIN { get; set; }
        public string EntryByDept { get; set; }
        public Nullable<System.DateTime> FirstDoctorCallTime { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public Nullable<System.DateTime> Dob { get; set; }
        public Nullable<int> GenderId { get; set; }
        public Nullable<int> ProgramId { get; set; }
        public string AreaOffice { get; set; }
        public Nullable<int> DivisionId { get; set; }
        public Nullable<int> DistrictId { get; set; }
        public Nullable<int> EffectedPersonId { get; set; }
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
        public Nullable<int> LastUpdateById { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<int> Age { get; set; }
        public string CoMorbidities { get; set; }
        public string StaffName { get; set; }
        public Nullable<System.DateTime> LastFollowupDate { get; set; }
        public string Designation { get; set; }
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
        public Nullable<int> CouncilorUserId { get; set; }
    
        public virtual ICollection<DoctorPoleCouncilling> DoctorPoleCouncillings { get; set; }
        public virtual ICollection<DoctorPoleStatus> DoctorPoleStatuses { get; set; }
        public virtual StandingData StandingData { get; set; }
        public virtual StandingData StandingData1 { get; set; }
        public virtual StandingData StandingData2 { get; set; }
        public virtual StandingData StandingData3 { get; set; }
        public virtual StandingData StandingData4 { get; set; }
        public virtual StandingData StandingData5 { get; set; }
        public virtual StandingData StandingData6 { get; set; }
        public virtual StandingData StandingData7 { get; set; }
        public virtual StandingData StandingData8 { get; set; }
        public virtual StandingData StandingData9 { get; set; }
        public virtual StandingData StandingData10 { get; set; }
        public virtual StandingData StandingData11 { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual UserProfile UserProfile1 { get; set; }
        public virtual UserProfile UserProfile2 { get; set; }
        public virtual ICollection<DoctorsPoleVisit> DoctorsPoleVisits { get; set; }
    }
}
