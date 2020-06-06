using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppProj.Web.Models
{
    public class DistrictSubmitModel
    {        
        public int SumId { get; set; }
        public int DetailId { get; set; }
        [Required(ErrorMessage = "(তারিখ অবশ্যই দিন)")]
        public DateTime Date { get; set; }

        public int DivisionId { get; set; }
        public string DivisionName { get; set; }

        public int DistrictId { get; set; }
        public string DistrictName { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int HospitalCount { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int BedCount { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int IcuCount { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int OtherPatientCount { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int BreathingPatientCount { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int MaternalCount { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public decimal PlannedRice { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public decimal PlannedPotato { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public decimal PlannedDal { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public decimal PlannedMoney { get; set; }
        public string RemarksSum{ get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int NewQuarantine { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int ReleasedQuarantine { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int DoTestOn { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int Death { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int BracPatient { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int BracReleased { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int BracQurantine { get; set; }


        public string LocalAdminMeasure { get; set; }
        public string Demand { get; set; }
        public string Panic { get; set; }
        public string SocialIssue { get; set; }
        public string BracThink { get; set; }
        public string NgoDoing { get; set; }
        public string NeedyPeople { get; set; }
        public string BracDo { get; set; }
        public string RemarksDetail { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int MemberCount { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int MemberUpazillaCount { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int DistrictTypeValue { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int ReliefFamily { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int ReliefPerson { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public decimal Rice { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public decimal Dal { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public decimal Potato { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int Money { get; set; }
        //public decimal Onion { get; set; }
        //public decimal Salt { get; set; }
        //public decimal Oil { get; set; }
        //public decimal Soap { get; set; }

        public bool IsPriceUpdate { get; set; }
        public Nullable<int> PriceRice { get; set; }
        public Nullable<int> PriceDal { get; set; }
        public Nullable<int> PricePotato { get; set; }
        public Nullable<int> PriceOnion { get; set; }
        public Nullable<int> PriceOil { get; set; }
        public Nullable<int> PriceOilPack { get; set; }
        public Nullable<int> PriceSalt { get; set; }
        public Nullable<int> PriceEggPlant { get; set; }
        public Nullable<int> PriceEgg { get; set; }
        public Nullable<int> PriceChille { get; set; }
        public Nullable<int> PricePumpkin { get; set; }
        public string PriceComment { get; set; }

        public string ReliefRemarks { get; set; }
        public int? Question1Id { get; set; }
        public string Question1 { get; set; }
        public int? Question2Id { get; set; }
        public string Question2 { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
    }

    public class DistrictSubmitSubModel
    {
        public int DivisionId { get; set; }
        public int DistrictId { get; set; }
        [Required(ErrorMessage = "(তারিখ অবশ্যই দিন)")]
        public DateTime Date { get; set; }

        public IEnumerable<SelectListItem> Divisions { get; set; }
        public IEnumerable<SelectListItem> Districts { get; set; }
    }

}