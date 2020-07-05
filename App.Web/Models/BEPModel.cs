using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using AppProj.Domain;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppProj.Web.Models
{

    public partial class BEPModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "(তারিখ অবশ্যই দিন)")]
        public System.DateTime Date { get; set; }
        public int ProgramId { get; set; }
        public int DivisionId { get; set; }
        public int DistrictId { get; set; }
        public Nullable<int> ImpactProgramId { get; set; }
        public Nullable<int> RestrictonId { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int AffectedBracWorkerCount { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int WorkFromHomeStaffCount { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public decimal FinancialLoss { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int AffectedBracWorkerCountTotal { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int WorkFromHomeStaffCountTotal { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public decimal FinancialLossTotal { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int UpazilaCoverageCount { get; set; }
        public string UpazillaRemarks { get; set; }
        public Nullable<int> ComMaterialId { get; set; }
        public Nullable<int> ComChannelId { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int Target { get; set; }
        public string Remarks { get; set; }
        
        public List<BEPDetailModel> BEPPreparenessActivityModels { get; set; }
        public List<BEPResponseActivityModel> BEPResponseActivityModels { get; set; }
        
        public IEnumerable<SelectListItem> Sources { get; set; }
        public IEnumerable<SelectListItem> Divisions { get; set; }
        public IEnumerable<SelectListItem> Districts { get; set; }
        public IEnumerable<SelectListItem> ImpactedReasons { get; set; }
        public IEnumerable<SelectListItem> Restrictions { get; set; }

        public IEnumerable<SelectListItem> CommMaterials { get; set; }
        public IEnumerable<SelectListItem> CommChannels { get; set; }

        public string ProgramName { get; set; }
        public string DivisionName { get; set; }
        public string DistrictName { get; set; }

    }

    public class BEPResponseActivityModel
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public System.DateTime Date { get; set; }
        public int ActivityId { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int Cat1NewReach { get; set; } //male
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int Cat1OldReach { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int Cat2NewReach { get; set; } //female
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int Cat2OldReach { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int Cat3NewReach { get; set; } //boy
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int Cat3OldReach { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int Cat4NewReach { get; set; } //girl
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int Cat4OldReach { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int Cat5NewReach { get; set; } //PWD
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int Cat5OldReach { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int Cat6NewReach { get; set; } //HHS
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int Cat6OldReach { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int Cat7NewReach { get; set; } //not classified
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int Cat7OldReach { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int Cat8NewReach { get; set; } //pregnant
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int Cat8OldReach { get; set; }

        public List<BEPDetailModel> BEPDetailModels { get; set; }

        public string ActivityName { get; set; }


    }

    public class BEPDetailModel
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        [Required(ErrorMessage = "(তারিখ অবশ্যই দিন)")]
        public System.DateTime Date { get; set; }       
        public int ActivityId { get; set; }
        public int ItemId { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public decimal Quantity { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public decimal ExpQuantity { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int ReachCount { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "(সংখ্যাটি ০ বা তার চেয়ে বড় হবে)")]
        public int ReachCountFemale { get; set; }


        public string ActivityName { get; set; }
        public string ItemName { get; set; }

    }
}
