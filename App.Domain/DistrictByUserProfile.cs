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
    
    public partial class DistrictByUserProfile
    {
        public int Id { get; set; }
        public int UserInfoId { get; set; }
        public int DistrictId { get; set; }
    
        public virtual StandingData StandingData { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
