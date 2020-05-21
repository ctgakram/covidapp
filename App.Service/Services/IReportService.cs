using AppProj.Domain;
using AppProj.Domain.ModelExt;
using AppProj.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Service.Services
{
    public interface IReportService
    {
        ReportVM GetReport();
    }
}
