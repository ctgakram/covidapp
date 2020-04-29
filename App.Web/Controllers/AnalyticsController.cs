using AppProj.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppProj.Web.Controllers
{
    [Authorize]
    public class AnalyticsController : Controller
    {
        readonly IStandingDataService standingDataService;
        readonly ISummerizedDataService sumDataService;
        readonly IDetailDataService detDataService;
        readonly IDistrictDataService disDataService;
        readonly IHnppDataService hnppDataService;


        public AnalyticsController(IDetailDataService detDataService, ISummerizedDataService sumDataService
            , IStandingDataService standingDataService, IDistrictDataService disDataService
            , IHnppDataService hnppDataService
            )
        {

            this.standingDataService = standingDataService;
            this.disDataService = disDataService;
            this.sumDataService = sumDataService;
            this.detDataService = detDataService;
            this.hnppDataService = hnppDataService;

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Gob()
        {
            return View();
        }

        public ActionResult Survey()
        {
            return View();
        }

        public object BySourcePie()
        {
            var obj = sumDataService.GetBySource();

            List<object> chartData = new List<object>();
            chartData.Add(new object[] { "Source", "Reach" });


            foreach (var row in obj)
            {
                chartData.Add(new object[] { row.Name, row.Count });
            }

            return Json(chartData, JsonRequestBehavior.AllowGet);
        }

        public object PatientCountBar()
        {
            var obj = disDataService.GetLastPatientCount(15);

            List<object> chartData = new List<object>();
            chartData.Add(new object[] { "Date", "Patient", new { role = "style" } });


            foreach (var row in obj)
            {
                chartData.Add(new object[] { row.Name, row.Count, "#76A7FA" });
            }

            return Json(chartData, JsonRequestBehavior.AllowGet);
        }

        public object CountByDistrictBar()
        {
            var obj = sumDataService.GetByDistrict();

            List<object> chartData = new List<object>();
            chartData.Add(new object[] { "District", "Reach", new { role = "style" } });


            foreach (var row in obj)
            {
                chartData.Add(new object[] { row.Name, row.Count, "#76A7FA" });
            }

            return Json(chartData, JsonRequestBehavior.AllowGet);
        }

        public object Last3YearFundCombo()
        {
            var obj = detDataService.GetThreeIndex();

            List<object> chartData = new List<object>();
            chartData.Add(new object[] { "Symptom", "জ্বর-শ্বাসকষ্ট", "কন্টাক্ট " });

            foreach (var row in obj)
            {
                chartData.Add(new object[] { row.Name, row.Count, row.Count2 });
            }

            return Json(chartData, JsonRequestBehavior.AllowGet);
        }

        public object ByAgePie()
        {
            var obj = detDataService.GetByAge();

            List<object> chartData = new List<object>();
            chartData.Add(new object[] { "Source", "Reach" });


            foreach (var row in obj)
            {
                chartData.Add(new object[] { row.Name, row.Count });
            }

            return Json(chartData, JsonRequestBehavior.AllowGet);
        }


    }
}
