﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppProj.Service.Services;
using System.Configuration;
using AppProj.Web.Helpers;
using System.Data;
using AppProj.Domain;
using AppProj.Web.Models;

namespace AppProj.Web.Controllers
{
    [Authorize]
    public class MainController : Controller
    {
        readonly IStandingDataService standingDataService;
        readonly ISummerizedDataService sumDataService;
        readonly IDetailDataService detDataService;
        readonly IDistrictDataService disDataService;
        readonly IHnppDataService hnppDataService;
        readonly IBepDataService bepDataService;

        public MainController(IDetailDataService detDataService, ISummerizedDataService sumDataService
            , IStandingDataService standingDataService, IDistrictDataService disDataService
            , IHnppDataService hnppDataService, IBepDataService bepDataService
            )
        {

            this.standingDataService = standingDataService;
            this.disDataService = disDataService;
            this.sumDataService = sumDataService;
            this.detDataService = detDataService;
            this.hnppDataService = hnppDataService;
            this.bepDataService = bepDataService;
        }

        public ActionResult Index()
        {
            var data = standingDataService.GetByType(StandingDataTypes.TreeActivity)
                .OrderBy(c => c.IntValue)
                .ToList();
                
            return View(data);
        }

        public ActionResult Message()
        {
            var data = standingDataService.GetByType(StandingDataTypes.TreeMessage)
                .OrderByDescending(c => c.IntValue)
                .ToList();

            return View(data);
        }

        public ActionResult GisMap()
        {
            return View();
        }

        public ActionResult WorldMap()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            DashboardModel model = new DashboardModel();

            model.Suspect = detDataService.GetCount();
            model.SuspectFemale = detDataService.GetCountFemale();
            model.SuspectApp = detDataService.GetAppCount();
            model.Reach = sumDataService.GetReachCount();
            model.Districts = detDataService.Vulnarable(5);

            model.DashboardModelBDCs = disDataService.GetSummery()
                        .GroupBy(q => 1)
                        .Select(g => new DashboardModelBDC
                        {
                            Hospital = g.Sum(c => c.HospitalCount)
                            ,
                            Bed = g.Sum(c => c.BedCount)
                             ,
                            QrnCurrent = g.Sum(c => c.CurrentQuarantine)
                            ,
                            Qrn = g.Sum(c => c.TotalQuarantine)
                            ,
                            QrnReleased = g.Sum(c => c.TotalReleased)
                            ,
                            Death = g.Sum(c => c.TotalDeath)
                            ,
                            Test = g.Sum(c => c.TotalDoTestOn)
                            ,
                            DisRiceFamily = g.Sum(c => c.TotalReliefFamily)
                            ,
                            DisMoneyFamily = g.Sum(c => c.TotalReliefPerson)
                            ,
                            DisRice = g.Sum(c => c.TotalRice)
                            ,
                            DisMoney = g.Sum(c => c.TotalMoney)
                            ,
                            PlanRice = g.Sum(c => c.PlannedRice)
                            ,
                            PlanMoney = g.Sum(c => c.PlannedMoney)

                        }).Single();

            model.DashboardModelHnpps = hnppDataService.Get()
                      .GroupBy(q => 1)
                      .Select(g => new DashboardModelHnpp
                      {
                          MeetingGovt = g.Sum(c => c.GovtMeeting)
                          ,
                          MeetingBrac = g.Sum(c => c.BracMeeting)
                          ,
                          Leaflet = g.Sum(c => c.Leaflet)
                          ,
                          Sticker = g.Sum(c => c.Sticker)
                      }).Single();

            model.BepDataSummeryModelReaches = bepDataService.GetReachForDashboard();
            model.BepDataSummeryModelMaterials = bepDataService.GetMaterialForDashboard();
            model.BepDataSummeryModelMaterialDistributions = bepDataService.GetDistributionForDashboard();
            
            return View(model);
        }

        public ActionResult Error()
        {
            return PartialView();
        }

        public ActionResult Support()
        {
            return PartialView();
        }

        public ActionResult UnAuthorisedAction()
        {
            return PartialView();
        }

        public JsonResult GetUpazilaByDistrict(int id)
        {
            var d = standingDataService.GetUpazilla(id).ToSelectList(null, "Id", "Name");

            return Json(d, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDistrictByDivision(int id)
        {
            var d = standingDataService.GetDistricts(id).ToSelectList(null, "Id", "Name");

            return Json(d, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SummeryData()
        {
            var dis = disDataService.GetSummery();



            var obj = new
            {
                suspect = detDataService.GetCount()
            ,
                app = detDataService.GetAppCount()
            ,
                femaleSuspect = detDataService.GetCountFemale()
            ,
                reach = sumDataService.GetReachCount()
            ,
                feverBreadth = detDataService.GetCountFeverBreadth()
            ,
                vulnarable = detDataService.Vulnarable(5)
            ,
                district = disDataService.GetSummery()
                        .GroupBy(q => 1)
                        .Select(g => new
                        {
                            hospital = g.Sum(c => c.HospitalCount)
                            ,
                            bed = g.Sum(c => c.BedCount)
                             ,
                            curquarantine = g.Sum(c => c.CurrentQuarantine)
                            ,
                            quarantine = g.Sum(c => c.TotalQuarantine)
                            ,
                            released = g.Sum(c => c.TotalReleased)
                            ,
                            death = g.Sum(c => c.TotalDeath)
                            ,
                            test = g.Sum(c => c.TotalDoTestOn)
                            //,
                            //oil = g.Sum(c => c.TotalOil)
                            //,
                            //soap = g.Sum(c => c.TotalSoap)
                            //,
                            //onion = g.Sum(c => c.TotalOnion)
                            //,
                            //salt = g.Sum(c => c.TotalSalt)
                            ,
                            family = g.Sum(c => c.TotalReliefFamily)
                            ,
                            person = g.Sum(c => c.TotalReliefPerson)
                            ,
                            rice = g.Sum(c => c.TotalRice)
                            //,
                            //dal = g.Sum(c => c.TotalDal)
                            //,
                            //potato = g.Sum(c => c.TotalPotato)
                            ,
                            money = g.Sum(c => c.TotalMoney)
                            ,
                            planRice = g.Sum(c => c.PlannedRice)
                            ,
                            planMoney = g.Sum(c => c.PlannedMoney)

                        }).Single()
              ,
                hnpp = hnppDataService.Get()
                      .GroupBy(q => 1)
                      .Select(g => new
                      {
                          govtMeet = g.Sum(c => c.GovtMeeting)
                          ,
                          bracMeet = g.Sum(c => c.BracMeeting)
                          ,
                          leaf = g.Sum(c => c.Leaflet)
                          ,
                          stic = g.Sum(c => c.Sticker)
                      }).Single()
            };

            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        public JsonResult MenuData()
        {
            var data = standingDataService.GetByType(StandingDataTypes.TreeActivity)
                .Where(c => c.IsActive)
                .OrderBy(c=>c.IntValue)
                .Select(c=> new { c.Name})
                .ToList();

            var data2 = standingDataService.GetByType(StandingDataTypes.TreeMessage)
                .Where(c => c.IsActive)
                .OrderByDescending(c => c.IntValue)
                .Select(c => new { c.Name })
                .ToList();

            var obj = new
            {
                act = data
                ,
                msg = data2
            };

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        /*
        public object FundProgramPie()
        {
            var data = ProjectService.Get();

            data = data.Where(c => c.IsActive && !c.IsCompleted);

            var obj = data.GroupBy(l => l.StandingData4.Name)
               .Select(c => new { Name = c.Key, Count = c.Sum(d=>d.ProposedFund)/10000000.00M });
            
            List<object> chartData = new List<object>();
            chartData.Add(new object[] { "Program", "Funds"});


            foreach (var row in obj)
            {
                chartData.Add(new object[] { row.Name, row.Count });
            }

            return Json(chartData, JsonRequestBehavior.AllowGet);
        }

        public object DueByProgramCombo()
        {
            var data = ProjectService.GetDueYear();

            var obj = data.GroupBy(l => l.Project.StandingData4.Name)
               .Select(c => new
               {
                   Name = c.Key
                   //,
                   //Cnt = c.Select(x => x.Project.Name).Distinct().Count()
                   ,
                   Pro = c.Count(x => x.Proposal == null)
                   ,
                   Aud = c.Count(x => x.Audit == null)
                    ,
                   Dc = c.Count(x => x.DcUno == null)
               });

            List<object> chartData = new List<object>();
            chartData.Add(new object[] { "Program", "Proposal", "Audit Report", "DC-UNO Certificate" });

            foreach (var row in obj)
            {
                chartData.Add(new object[] { row.Name, row.Pro, row.Aud, row.Dc });
            }

            return Json(chartData, JsonRequestBehavior.AllowGet);
        }

        

        public object Last3YearFundLine()
        {
            int yr = DateTime.Now.Year;

            if (DateTime.Now.Month <= 3)
            {
                yr--;
            }

            DateTime dt1 = DateTime.Parse("1/1/" + yr);
            DateTime edt1 = DateTime.Parse("12/31/" + yr);

            DateTime dt2 = DateTime.Parse("1/1/" + (yr - 1));
            DateTime edt2 = DateTime.Parse("12/31/" + (yr - 1));

            DateTime dt3 = DateTime.Parse("1/1/" + (yr - 2));
            DateTime edt3 = DateTime.Parse("12/31/" + (yr - 2));

            var data = ProjectService.GetYear(dt3);

            var obj = data.GroupBy(l => l.Project.StandingData4.Name)
               .Select(c => new
               {
                   Name = c.Key
                   //,
                   //Cnt = c.Select(x => x.Project.Name).Distinct().Count()
                   ,
                   Yr1 = c.Sum(d => ((d.Budget1ApprovalDate >= dt1 && d.Budget1ApprovalDate <= edt1) ? d.Budget1Amount ?? 0 : 0) + ((d.Budget2ApprovalDate >= dt1 && d.Budget2ApprovalDate <= edt1) ? d.Budget2Amount ?? 0 : 0))
                   ,
                   Yr2 = c.Sum(d => ((d.Budget1ApprovalDate >= dt2 && d.Budget1ApprovalDate <= edt2) ? d.Budget1Amount ?? 0 : 0) + ((d.Budget2ApprovalDate >= dt2 && d.Budget2ApprovalDate <= edt2) ? d.Budget2Amount ?? 0 : 0))
                    ,
                   Yr3 = c.Sum(d => ((d.Budget1ApprovalDate >= dt3 && d.Budget1ApprovalDate <= edt3) ? d.Budget1Amount ?? 0 : 0) + ((d.Budget2ApprovalDate >= dt3 && d.Budget2ApprovalDate <= edt3) ? d.Budget2Amount ?? 0 : 0))
               }).ToList();

            int count = obj.Count();

            List<object> chartData = new List<object>();

            var r = new object[count + 1];

            r[0] = "Year";
            for (int i = 1; i < count+1; i++)
            {
                r[i] = obj[i - 1].Name;
            }

            chartData.Add(r);

            //year -1 
            r = new object[count + 1];

            r[0] = (yr-2).ToString();
            for (int i = 1; i < count + 1; i++)
            {
                r[i] = obj[i - 1].Yr3 / 10000000;
            }

            chartData.Add(r);

            //year -2
            r = new object[count + 1];

            r[0] = (yr-1).ToString();
            for (int i = 1; i < count + 1; i++)
            {
                r[i] = obj[i - 1].Yr2 / 10000000;
            }

            chartData.Add(r);

            //year -3
            r = new object[count + 1];

            r[0] = yr.ToString();
            for (int i = 1; i < count + 1; i++)
            {
                r[i] = obj[i - 1].Yr1 / 10000000;
            }

            chartData.Add(r);

            return Json(chartData, JsonRequestBehavior.AllowGet);
        }
        
        public object AverageDueByProgramCombo()
        {
            var data = ProjectService.GetStartedYear();

            var obj = data.GroupBy(l => l.Project.StandingData4.Name)
               .Select(c => new
               {
                   Name = c.Key
                   //,
                   //Cnt = c.Select(x => x.Project.Name).Distinct().Count()
                   ,
                   Pro = c.Where(d => d.ProposalDueCount > 0).Average(x => x.ProposalDueCount)
                   ,
                   Aud = c.Where(d => d.AuditDueCount > 0).Average(x => x.AuditDueCount)
                    ,
                   Dc = c.Where(d => d.DcDueCount > 0).Average(x => x.DcDueCount)
               });

            List<object> chartData = new List<object>();
            chartData.Add(new object[] { "Program", "Proposal", "Audit Report", "DC-UNO Certificate" });

            foreach (var row in obj)
            {
                chartData.Add(new object[] { row.Name, row.Pro, row.Aud, row.Dc });
            }

            return Json(chartData, JsonRequestBehavior.AllowGet);
        }
        */
    }
}
