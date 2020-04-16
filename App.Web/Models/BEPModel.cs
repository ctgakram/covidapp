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
        public System.DateTime Date { get; set; }
        public int ProgramId { get; set; }
        public int DivisionId { get; set; }
        public int DistrictId { get; set; }
        public Nullable<int> ImpactProgramId { get; set; }
        public Nullable<int> RestrictonId { get; set; }
        public int AffectedBracWorkerCount { get; set; }
        public int WorkFromHomeStaffCount { get; set; }
        public decimal FinancialLoss { get; set; }
        public int AffectedBracWorkerCountTotal { get; set; }
        public int WorkFromHomeStaffCountTotal { get; set; }
        public decimal FinancialLossTotal { get; set; }
        public int UpazilaCoverageCount { get; set; }
        public string UpazillaRemarks { get; set; }
        public Nullable<int> ComMaterialId { get; set; }
        public Nullable<int> ComChannelId { get; set; }
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

        public int Cat7NewReach { get; set; }
        public int Cat7OldReach { get; set; }

        public List<BEPDetailModel> BEPDetailModels { get; set; }

        public string ActivityName { get; set; }


    }

    public class BEPDetailModel
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public System.DateTime Date { get; set; }       
        public int ActivityId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public int ExpQuantity { get; set; }
        public int ReachCount { get; set; }
        public int ReachCountFemale { get; set; }


        public string ActivityName { get; set; }
        public string ItemName { get; set; }

    }
}
