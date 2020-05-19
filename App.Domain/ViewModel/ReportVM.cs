using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Domain.ViewModel
{
    public class ReportVM
    {
        public ReportVM()
        {
            ReportTypes = new List<ReportTypes>();
            SelectedReports = new List<Content>();
        }

        public int ReportTypeId { get; set; }
        public List<ReportTypes> ReportTypes { get; set; }

        public List<Content> SelectedReports { get; set; }
    }
    public class ReportTypes
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public int Total { get; set; }
    }
}
