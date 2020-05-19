using AppProj.Data.Repositories;
using AppProj.Domain.Enums;
using AppProj.Domain.ViewModel;
using AppProj.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Service.ServicesImpl
{
    public class ReportService : IReportService
    {
        readonly IReportRepository _repository;
        public ReportService(IReportRepository reportRepository)
        {
            _repository = reportRepository;
        }
        public ReportVM GetReport(int selectedid)
        {
            var output = new ReportVM();
            var items = _repository.GetAll();
            output.SelectedReports = items.OrderByDescending(x => x.CreatedOn).ToList(); //items.Where(x => x.ContentType == selectedid).ToList();
            List<ReportTypes> lstrt = new List<ReportTypes>();
            foreach (int i in Enum.GetValues(typeof(ReportTypesEnum)))
            {
                lstrt.Add(new ReportTypes()
                {
                    TypeId = i,
                    TypeName = Utility.GetEnumDescription((ReportTypesEnum)i),
                    Total = items.Count(x => x.ContentType == i)
                });
            }
            output.ReportTypes = lstrt;
            return output;
        }
        public ReportVM GetReport()
        {
            var output = new ReportVM();
            output = GetReport(1);
            output.ReportTypeId = 1;
            return output;
        }
    }
}
