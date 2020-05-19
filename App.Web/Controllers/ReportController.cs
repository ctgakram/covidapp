using AppProj.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppProj.Web.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }
        public ActionResult Index()
        {
            var items = _reportService.GetReport();
            return View(items);
        }

    }
}
