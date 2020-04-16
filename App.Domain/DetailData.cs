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
    
    public partial class DetailData
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public int SourceId { get; set; }
        public int DistrictId { get; set; }
        public int UpazillaId { get; set; }
        public string Name { get; set; }
        public Nullable<int> GenderId { get; set; }
        public Nullable<int> Age { get; set; }
        public string Phone { get; set; }
        public Nullable<bool> IsFever { get; set; }
        public Nullable<bool> IsDryCough { get; set; }
        public Nullable<bool> IsBreadth { get; set; }
        public Nullable<bool> IsRespiratory { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsContact { get; set; }
        public string CollectedBy { get; set; }
        public int InsertedById { get; set; }
    
        public virtual StandingData StandingData { get; set; }
        public virtual StandingData StandingData1 { get; set; }
        public virtual StandingData StandingData2 { get; set; }
        public virtual StandingData StandingData3 { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
