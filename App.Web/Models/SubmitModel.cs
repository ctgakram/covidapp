using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppProj.Web.Models
{
    public class SubmitModel
    {
        [Key]
        public int Id { get; set; }
        public int SourceId { get; set; }
        public int DistrictId { get; set; }
        public int UpazillaId { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int ReachCount { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "(তারিখ অবশ্যই দিন)")]
        public System.DateTime Date { get; set; }

        [Required(ErrorMessage = "নাম অবশ্যই দিন")]
        public string Name { get; set; }

        [Required(ErrorMessage = "লিঙ্গ অবশ্যই দিন")]
        public Nullable<int> GenderId { get; set; }

        [Required(ErrorMessage = "বয়স অবশ্যই দিন")]
        public Nullable<int> Age { get; set; }

        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{5})$",
                   ErrorMessage = "সঠিক ফোন নম্বর দিন")]
        public string Phone { get; set; }

        public bool IsFever { get; set; }
        public bool IsDryCough { get; set; }
        public bool IsBreadth { get; set; }
        public bool IsContact { get; set; }
                
        public string CollectedBy { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int RiskBySS { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int RiskBySK { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int RiskByPK { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int RiskByPA { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int RiskByAM { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int CaseCnt { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int BracMeeting { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int BracParticipant { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int GovtMeeting { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int GovtParticipant { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int Leaflet { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int Sticker { get; set; }

        public IEnumerable<SelectListItem> Sources { get; set; }
        public IEnumerable<SelectListItem> Districts { get; set; }
        public IEnumerable<SelectListItem> Upazillas { get; set; }
        public IEnumerable<SelectListItem> Genders { get; set; }

        public List<MultiData> MultiDatas { get; set; }
    }

    public class MultiData
    {
        public int Id { get; set; }
        public string Src { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int Count { get; set; }
        public string CollectedBy { get; set; }
    }
}