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
    
    public partial class BERDataPeopleWiseQuantity
    {
        public int Id { get; set; }
        public int BEPDataId { get; set; }
        public string Type { get; set; }
        public System.DateTime Date { get; set; }
        public int ActivityId { get; set; }
        public int ProgramId { get; set; }
        public int DivisionId { get; set; }
        public int DistrictId { get; set; }
        public int Cat1NewReach { get; set; }
        public int Cat1OldReach { get; set; }
        public int Cat2NewReach { get; set; }
        public int Cat2OldReach { get; set; }
        public int Cat3NewReach { get; set; }
        public int Cat3OldReach { get; set; }
        public int Cat4NewReach { get; set; }
        public int Cat4OldReach { get; set; }
        public int Cat5NewReach { get; set; }
        public int Cat5OldReach { get; set; }
        public int Cat6NewReach { get; set; }
        public int Cat6OldReach { get; set; }
        public int InsertedById { get; set; }
        public System.DateTime LastUpdated { get; set; }
        public int Cat7NewReach { get; set; }
        public int Cat7OldReach { get; set; }
        public int Cat8NewReach { get; set; }
        public int Cat8OldReach { get; set; }
    
        public virtual BEPData BEPData { get; set; }
        public virtual StandingData StandingData { get; set; }
        public virtual StandingData StandingData1 { get; set; }
        public virtual StandingData StandingData2 { get; set; }
        public virtual StandingData StandingData3 { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
