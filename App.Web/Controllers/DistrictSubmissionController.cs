﻿using AppProj.Data.Infrastructure;
using AppProj.Service.Services;
using AppProj.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppProj.Web.Helpers;
using AppProj.Domain;
using AppProj.Web.ViewModels;
using Microsoft.Web.Mvc;

namespace AppProj.Web.Controllers
{
    [Authorize]
    public class DistrictSubmissionController : Controller
    {
        readonly IStandingDataService standingDataService;
        readonly IDistrictDataService disDataService;
        readonly IDistrictQuestionService districtQuestionService;
        readonly IDistrictByUserProfileService districtByUserProfileService;
        readonly IUnitOfWork unitOfWork;

        public DistrictSubmissionController(IUnitOfWork unitOfWork,
            IDistrictDataService disDataService,
            IStandingDataService standingDataService,
            IDistrictQuestionService districtQuestionService,
            IDistrictByUserProfileService districtByUserProfileService)
        {
            this.standingDataService = standingDataService;
            this.disDataService = disDataService;
            this.unitOfWork = unitOfWork;
            this.districtQuestionService = districtQuestionService;
            this.districtByUserProfileService = districtByUserProfileService;
        }

        public ActionResult Index()
        {
            SearchModel up = new SearchModel();

            var source = standingDataService.GetDivisions().Where(r => r.IsActive);
            up.ContentTypes1 = source.ToSelectList(null, "Id", "Name");

            var dis = standingDataService.GetDistricts(0).Where(r => r.IsActive);
            up.ContentTypes2 = dis.ToSelectList(null, "Id", "Name");

            up.FromDate = DateTime.Now;
            up.ToDate = DateTime.Now;

            return View(up);
        }

        public JsonResult DataGrid()
        {

            bool visible = UserRole.Check("DISTRICT", SessionHelper.Role);

            int ec = int.Parse(Request.QueryString["sEcho"]);
            int take = int.Parse(Request.QueryString["iDisplayLength"]);
            int skip = int.Parse(Request.QueryString["iDisplayStart"]);
            //bool isSum = bool.Parse(Request.QueryString["isSum"]);

            if (take == -1) { take = 1000000000; skip = 0; }

            int? divId = null;

            try
            {
                divId = Convert.ToInt32(Request.QueryString["ContentTypeId1"]);
            }
            catch { }

            int? disId = null;

            try
            {
                disId = Convert.ToInt32(Request.QueryString["ContentTypeId2"]);
            }
            catch { }


            DateTime FromDate = Convert.ToDateTime(Request.QueryString["FromDate"]);
            DateTime ToDate = Convert.ToDateTime(Request.QueryString["ToDate"]);

            int count = 0;
            List<DistrictData> dataList = disDataService.Get(divId, disId, FromDate, ToDate, skip, take, out count).ToList();

            /*
            if (skip <= 0 && dataList.Count()>0)
            {
                IEnumerable<DistrictData> dataListSum = disDataService.Get(divId, disId, FromDate, ToDate, 0, 10000000, out count);

                DistrictData tot = dataListSum.GroupBy(q => 1)
                    .Select(g => new DistrictData
                    {
                        Id = 0
                        ,
                        Date = ToDate
                        ,
                        StandingData = new StandingData { Name = "  Total" }
                        ,
                        DivisionId = -1
                        ,
                        StandingData1 = new StandingData { Name = " " }
                        ,
                        DistrictId = -1
                        ,
                        NewQuarantine = g.Sum(c => c.NewQuarantine)
                        ,
                        Death = g.Sum(c => c.Death)
                        ,
                        DoTestOn = g.Sum(c => c.DoTestOn)
                        ,
                        ReleasedQuarantine = g.Sum(c => c.ReleasedQuarantine)
                        ,
                        InsertedById = -1
                        ,
                        UserProfile = new UserProfile { UserName = "" }
                    }
                    ).Single();

                dataList.Insert(0, tot);
            }
            */

            if (FromDate == ToDate && divId == null && disId == null)
            {
                IEnumerable<StandingData> districts = standingDataService.GetDistricts().Where(c => c.IsActive);

                List<int> all = districts.Select(c => c.Id).ToList();
                List<int> used = dataList.Select(c => c.DistrictId).ToList();

                List<int> notused = all.Except(used).ToList();

                List<StandingData> add = districts.Where(c => notused.Contains(c.Id)).ToList();

                foreach (var v in add)
                {
                    dataList.Add(new DistrictData
                    {
                        Id = 0
                        ,
                        Date = ToDate
                        ,
                        StandingData = new StandingData { Name = "<span style=\"color:#0000ff;\">তথ্য দেয়া হয়নি</span>" }
                        ,
                        DivisionId = v.ParentId.Value
                        ,
                        StandingData1 = new StandingData { Name = v.Name }
                        ,
                        DistrictId = v.Id
                        ,
                        InsertedById = -1
                        ,
                        UserProfile = new UserProfile { UserName = "" }
                    });
                }
            }

            var obj = (from c in dataList
                       select new object[] {
                       "⇒"+c.StandingData.Name + "<br/>⇒"+c.StandingData1.Name+"<br/>⇒"+String.Format("{0:dd MMM, yyyy}", c.Date)+"<br/>⇒ D ("+c.MemberCount+") U("+c.MemberUpazillaCount+")"
                       //,c.NewQuarantine
                       //,c.ReleasedQuarantine
                       //,c.DoTestOn
                       //,c.Death                      
                       , c.LocalAdminMeasure
                       ,c.Demand
                       ,c.Panic
                       ,c.SocialIssue
                       ,c.BracThink
                       ,c.NgoDoing
                       ,c.NeedyPeople
                       ,c.BracDo
                       ,c.Remarks
                       ,c.Question1Id!=null?"প্রশ্ন : "+standingDataService.GetDataById((int)c.Question1Id)?.Name +" উত্তর : "+c.Answer1:""
                       ,c.Question2Id != null ? "প্রশ্ন : " + standingDataService.GetDataById((int)c.Question2Id)?.Name +" উত্তর : " + c.Answer2:""
                       //,c.UserProfile.UserName
                //,new GridButtonModel[]
                //    {
                //         new GridButtonModel{U=Url.Action("Edit",new {Id=c.Id}), T="Edit", D = GridButtonDialog.dialig1.ToString(), H="Edit", M="class=\"btn btn-mini btn-warning\"", V = visible}

                //    }
            }).ToArray();


            JQueryDataTable js = new JQueryDataTable();
            js.sEcho = ec;
            js.iTotalDisplayRecords = count.ToString();
            js.iTotalRecords = js.iTotalDisplayRecords;
            js.aaData = obj;

            return Json(js, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IndexSum()
        {
            SearchModel up = new SearchModel();

            var source = standingDataService.GetDivisions().Where(r => r.IsActive);
            up.ContentTypes1 = source.ToSelectList(null, "Id", "Name");

            var dis = standingDataService.GetDistricts(0).Where(r => r.IsActive);
            up.ContentTypes2 = dis.ToSelectList(null, "Id", "Name");

            return View(up);
        }

        public JsonResult DataGridSum()
        {

            //bool visible = UserRole.Check("DISTRICT", SessionHelper.Role);

            int ec = int.Parse(Request.QueryString["sEcho"]);
            int take = int.Parse(Request.QueryString["iDisplayLength"]);
            int skip = int.Parse(Request.QueryString["iDisplayStart"]);

            if (take == -1) { take = 100000000; skip = 0; }

            int? divId = null;

            try
            {
                divId = Convert.ToInt32(Request.QueryString["ContentTypeId1"]);
            }
            catch { }

            int? disId = null;

            try
            {
                disId = Convert.ToInt32(Request.QueryString["ContentTypeId2"]);
            }
            catch { }


            //DateTime FromDate = Convert.ToDateTime(Request.QueryString["FromDate"]);
            //DateTime ToDate = Convert.ToDateTime(Request.QueryString["ToDate"]);

            int count = 0;
            List<DistrictSummery> dataList = disDataService.GetSummery(divId, disId, skip, take, out count).ToList();

            if (dataList.Count() > 0)
            {
                DistrictSummery tot = dataList.GroupBy(q => 1)
                    .Select(g => new DistrictSummery
                    {
                        Id = 0
                        ,
                        StandingData = new StandingData { Name = "<span style=\"color:#ff6a00; font-size:14px;\">Total</span>" }
                        ,
                        DivisionId = -1
                        ,
                        StandingData1 = new StandingData { Name = " " }
                        ,
                        DistrictId = -1
                        ,
                        HospitalCount = g.Sum(c => c.HospitalCount)
                        ,
                        BedCount = g.Sum(c => c.BedCount)
                        ,
                        CurrentQuarantine = g.Sum(c => c.CurrentQuarantine)
                        ,
                        TotalDeath = g.Sum(c => c.TotalDeath)
                        ,
                        TotalDoTestOn = g.Sum(c => c.TotalDoTestOn)
                        ,
                        TotalQuarantine = g.Sum(c => c.TotalQuarantine)
                        ,
                        TotalReleased = g.Sum(c => c.TotalReleased)
                        ,
                        TotalReliefFamily = g.Sum(c => c.TotalReliefFamily)
                        ,
                        TotalReliefPerson = g.Sum(c => c.TotalReliefPerson)
                        ,
                        TotalRice = g.Sum(c => c.TotalRice)
                        //,
                        //TotalDal = g.Sum(c => c.TotalDal)
                        ,
                        TotalMoney = g.Sum(c => c.TotalMoney)
                        ,
                        OtherPatientCount = g.Sum(c => c.OtherPatientCount)
                       ,
                        BreathingPatientCount = g.Sum(c => c.BreathingPatientCount)
                       ,
                        MaternalCount = g.Sum(c => c.MaternalCount)
                        //,
                        //TotalPotato = g.Sum(c => c.TotalPotato)
                        //,
                        //TotalOil = g.Sum(c => c.TotalOil)
                        //,
                        //TotalOnion = g.Sum(c => c.TotalOnion)
                        //,
                        //TotalSalt = g.Sum(c => c.TotalSalt)
                        //,
                        //TotalSoap = g.Sum(c => c.TotalSoap)
                        //,
                        //PlannedDal = g.Sum(c => c.PlannedDal)
                        ,
                        PlannedMoney = g.Sum(c => c.PlannedMoney)
                        //,
                        //PlannedPotato = g.Sum(c => c.PlannedPotato)
                        ,
                        PlannedRice = g.Sum(c => c.PlannedRice)
                        ,
                        TotalBracPatient = g.Sum(c => c.TotalBracPatient)
                        ,
                        TotalBracReleased = g.Sum(c => c.TotalBracReleased)
                        ,
                        TotalBracQurantine = g.Sum(c => c.TotalBracQurantine)
                        ,
                        InsertedById = -1
                        ,
                        UserProfile = new UserProfile { UserName = "" }
                    }
                    ).Single();

                dataList.Insert(0, tot);
            }

            var obj = (from c in dataList
                       select new object[] {c.StandingData.Name
                       ,c.StandingData1.Name
                       ,c.HospitalCount
                       ,c.BedCount
                       ,c.CurrentQuarantine
                       ,c.TotalQuarantine
                       ,c.TotalReleased
                       ,c.TotalDeath
                       ,c.TotalDoTestOn
                       ,c.OtherPatientCount
                       ,c.BreathingPatientCount
                       ,c.MaternalCount
                       ,c.TotalBracPatient
                       ,c.TotalBracReleased
                       ,c.TotalBracQurantine
                       ,c.PlannedRice
                       //,c.PlannedDal
                       //,c.PlannedPotato
                       ,c.PlannedMoney
                       //,c.Remarks
                       ,c.TotalRice
                       ,c.TotalReliefFamily
                      // ,c.TotalDal
                       //,c.TotalPotato
                       ,c.TotalMoney
                       ,c.TotalReliefPerson
                       //,c.TotalOnion
                       //,c.TotalSalt
                       //,c.TotalOil
                       //,c.TotalSoap
                       
                //,new GridButtonModel[]
                //    {
                //         new GridButtonModel{U=Url.Action("Edit",new {Id=c.Id}), T="Edit", D = GridButtonDialog.dialig1.ToString(), H="Edit", M="class=\"btn btn-mini btn-warning\"", V = visible}

                //    }
            }).ToArray();


            JQueryDataTable js = new JQueryDataTable();
            js.sEcho = ec;
            js.iTotalDisplayRecords = count.ToString();
            js.iTotalRecords = js.iTotalDisplayRecords;
            js.aaData = obj;

            return Json(js, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IndexReleif()
        {
            SearchModel up = new SearchModel();

            var source = standingDataService.GetDivisions().Where(r => r.IsActive);
            up.ContentTypes1 = source.ToSelectList(null, "Id", "Name");

            var dis = standingDataService.GetDistricts(0).Where(r => r.IsActive);
            up.ContentTypes2 = dis.ToSelectList(null, "Id", "Name");

            up.FromDate = DateTime.Now;
            up.ToDate = DateTime.Now;

            return View(up);
        }

        public JsonResult DataGridReleif()
        {

            bool visible = UserRole.Check("DISTRICT", SessionHelper.Role);

            int ec = int.Parse(Request.QueryString["sEcho"]);
            int take = int.Parse(Request.QueryString["iDisplayLength"]);
            int skip = int.Parse(Request.QueryString["iDisplayStart"]);
            //bool isSum = bool.Parse(Request.QueryString["isSum"]);

            if (take == -1) { take = 1000000000; skip = 0; }

            int? divId = null;

            try
            {
                divId = Convert.ToInt32(Request.QueryString["ContentTypeId1"]);
            }
            catch { }

            int? disId = null;

            try
            {
                disId = Convert.ToInt32(Request.QueryString["ContentTypeId2"]);
            }
            catch { }


            DateTime FromDate = Convert.ToDateTime(Request.QueryString["FromDate"]);
            DateTime ToDate = Convert.ToDateTime(Request.QueryString["ToDate"]);

            int count = 0;
            List<DistrictData> dataList = disDataService.Get(divId, disId, FromDate, ToDate, skip, take, out count).ToList();

            //List<DistrictData> dl = new List<DistrictData>();
            /*
            if (skip<=0 && dataList.Count()>0)
            {
                IEnumerable<DistrictData> dataListSum = disDataService.Get(divId, disId, FromDate, ToDate, skip, 1000000000, out count);

                DistrictData tot = dataListSum.GroupBy(q => 1)
                    .Select(g => new DistrictData
                    {
                        Id = 0
                        ,
                        Date = ToDate
                        ,
                        StandingData = new StandingData { Name = "<span style=\"color:#ff6a00; font-size:14px;\">Total</span>" }
                        ,
                        DivisionId = -1
                        ,
                        StandingData1 = new StandingData { Name = " " }
                        ,
                        DistrictId = -1
                        ,
                        NewQuarantine = g.Sum(c => c.NewQuarantine)
                        ,
                        Death = g.Sum(c => c.Death)
                        ,
                        DoTestOn = g.Sum(c => c.DoTestOn)
                        ,
                        ReleasedQuarantine = g.Sum(c => c.ReleasedQuarantine)
                        //,
                        //TillCurrentQuarantine = g.Sum(c => c.TillCurrentQuarantine)
                        //,
                        //TillDeath = g.Sum(c => c.TillDeath)
                        //,
                        //TillDoTestOn = g.Sum(c => c.TillDoTestOn)
                        //,
                        //TillQuarantine = g.Sum(c => c.TillQuarantine)
                        //,
                        //TillReleasedQuarantine = g.Sum(c => c.TillReleasedQuarantine)
                        ,                        
                        ReliefFamily = g.Sum(c => c.ReliefFamily)
                       //,
                       // ReliefPerson = g.Sum(c => c.ReliefPerson)
                       ,
                        Rice = g.Sum(c => c.Rice)
                       ,
                        Dal = g.Sum(c => c.Dal)
                       ,
                        Potato = g.Sum(c => c.Potato)
                       ,
                        Money = g.Sum(c => c.Money)
                       //,
                       // Onion = g.Sum(c => c.Onion)
                       //,
                       // Salt = g.Sum(c => c.Salt)
                       //,
                       // Oil = g.Sum(c => c.Oil)
                       //,
                       // Soap = g.Sum(c => c.Soap)
                        ,
                        InsertedById = -1
                        ,
                        UserProfile = new UserProfile { UserName = "" }
                    }
                    ).Single();

                dataList.Insert(0, tot);
            }
            */
            if (FromDate == ToDate && divId == null && disId == null)
            {
                IEnumerable<StandingData> districts = standingDataService.GetDistricts().Where(c => c.IsActive);

                List<int> all = districts.Select(c => c.Id).ToList();
                List<int> used = dataList.Select(c => c.DistrictId).ToList();

                List<int> notused = all.Except(used).ToList();

                List<StandingData> add = districts.Where(c => notused.Contains(c.Id)).ToList();

                foreach (var v in add)
                {
                    dataList.Add(new DistrictData
                    {
                        Id = 0
                        ,
                        Date = ToDate
                        ,
                        StandingData = new StandingData { Name = "<span style=\"color:#0000ff;\">তথ্য দেয়া হয়নি</span>" }
                        ,
                        DivisionId = v.ParentId.Value
                        ,
                        StandingData1 = new StandingData { Name = v.Name }
                        ,
                        DistrictId = v.Id
                        ,
                        InsertedById = -1
                        ,
                        UserProfile = new UserProfile { UserName = "" }
                    });
                }
            }

            var obj = (from c in dataList
                       select new object[] {
                       c.StandingData.Name
                       ,c.StandingData1.Name
                       ,String.Format("{0:dd MMM, yyyy}", c.Date)

                       ,c.NewQuarantine
                       ,c.ReleasedQuarantine
                       ,c.DoTestOn
                       ,c.Death

                       ,c.TillQuarantine
                       ,c.TillReleasedQuarantine
                       ,c.TillDoTestOn
                       ,c.TillDeath

                       ,c.TillCurrentQuarantine




                       ,c.Rice
                       ,c.ReliefFamily
                       //,c.Dal
                       //,c.Potato
                       ,c.Money
                       ,c.ReliefPerson
                       ,c.ReliefRemarks

                       //,c.Onion
                       //,c.Salt
                       //,c.Oil
                       //,c.Soap
                       //,c.UserProfile.UserName
                //,new GridButtonModel[]
                //    {
                //         new GridButtonModel{U=Url.Action("Edit",new {Id=c.Id}), T="Edit", D = GridButtonDialog.dialig1.ToString(), H="Edit", M="class=\"btn btn-mini btn-warning\"", V = visible}

                //    }
            }).ToArray();


            JQueryDataTable js = new JQueryDataTable();
            js.sEcho = ec;
            js.iTotalDisplayRecords = count.ToString();
            js.iTotalRecords = js.iTotalDisplayRecords;
            js.aaData = obj;

            return Json(js, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IndexBrac()
        {
            SearchModel up = new SearchModel();

            var source = standingDataService.GetDivisions().Where(r => r.IsActive);
            up.ContentTypes1 = source.ToSelectList(null, "Id", "Name");

            var dis = standingDataService.GetDistricts(0).Where(r => r.IsActive);
            up.ContentTypes2 = dis.ToSelectList(null, "Id", "Name");

            //up.FromDate = DateTime.Now;
            //up.ToDate = DateTime.Now;

            return View(up);
        }

        public JsonResult DataGridBrac()
        {

            //bool visible = UserRole.Check("DISTRICT", SessionHelper.Role);

            int ec = int.Parse(Request.QueryString["sEcho"]);
            int take = int.Parse(Request.QueryString["iDisplayLength"]);
            int skip = int.Parse(Request.QueryString["iDisplayStart"]);

            if (take == -1) { take = 100000000; skip = 0; }

            int? divId = null;

            try
            {
                divId = Convert.ToInt32(Request.QueryString["ContentTypeId1"]);
            }
            catch { }

            int? disId = null;

            try
            {
                disId = Convert.ToInt32(Request.QueryString["ContentTypeId2"]);
            }
            catch { }


            //DateTime FromDate = Convert.ToDateTime(Request.QueryString["FromDate"]);
            //DateTime ToDate = Convert.ToDateTime(Request.QueryString["ToDate"]);

            int count = 0;
            List<DistrictSummery> dataList = disDataService.GetSummery(divId, disId, skip, take, out count).ToList();
                       

            var obj = (from c in dataList
                       select new object[] {c.StandingData.Name
                       ,c.StandingData1.Name
                       ,String.Format("{0:dd MMM, yyyy}", c.PriceDate)
                       ,c.PriceRice
                       ,c.PriceDal
                       ,c.PricePotato
                       ,c.PriceOnion
                       ,c.PriceOilPack
                       ,c.PriceOil                       
                       ,c.PriceSalt
                       ,c.PriceEggPlant
                       ,c.PriceEgg
                       ,c.PriceChille
                       ,c.PricePumpkin
                       ,c.PriceComment
                      
            }).ToArray();


            JQueryDataTable js = new JQueryDataTable();
            js.sEcho = ec;
            js.iTotalDisplayRecords = count.ToString();
            js.iTotalRecords = js.iTotalDisplayRecords;
            js.aaData = obj;

            return Json(js, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(Roles: new string[] { "DISTRICT" })]
        public ActionResult Create()
        {
            DistrictSubmitSubModel up = new DistrictSubmitSubModel();

            //up.Divisions= standingDataService.GetDivisions().Where(r => r.IsActive).ToSelectList(null, "Id", "Name");

            //int d = int.Parse(up.Divisions.FirstOrDefault().Value) ;

            //up.Districts=standingDataService.GetDistricts(d).Where(r => r.IsActive).ToSelectList(d, "Id", "Name");

            up.Districts = districtByUserProfileService.GetByUser(SessionHelper.UserId).Select(c => new { c.DistrictId, c.StandingData.Name }).ToSelectList(null, "DistrictId", "Name");

            //up.DivisionId = d;
            up.Date = DateTime.Now;



            return PartialView(up);
        }

        [CustomAuthorize(Roles: new string[] { "DISTRICT" })]
        public ActionResult CreateDetail(DistrictSubmitSubModel model)
        {
            DistrictSubmitModel up = new DistrictSubmitModel();

            var disData = disDataService.Get(model.DistrictId, model.Date);
            var sumData = disDataService.GetSummery(model.DistrictId);

            ModelCopier.CopyModel(disData, up);
            ModelCopier.CopyModel(sumData, up);

            if (disData != null)
            {
                up.DetailId = disData.Id;
                up.RemarksDetail = disData.Remarks;
            }

            if (sumData != null)
            {
                up.SumId = sumData.Id;
                up.RemarksSum = sumData.Remarks;
            }

            up.Date = model.Date;

            var dis = standingDataService.GetDataById(model.DistrictId);
            up.DistrictName = dis.Name;

            up.DivisionId = dis.ParentId.Value;
            up.DivisionName = standingDataService.GetDataById(up.DivisionId).Name;


            up.DistrictTypeValue = dis.IntValue ?? 0;
            var districtQuesitonsIds = districtQuestionService.GetDistrictQuestions(model.DistrictId).Take(2).ToList();
            if (districtQuesitonsIds != null)
            {
                if (districtQuesitonsIds.Count == 1)
                {
                    var ques1 = standingDataService.GetDataById(districtQuesitonsIds.First());
                    up.Question1 = ques1.Name;
                }
                else if (districtQuesitonsIds.Count == 2)
                {
                    var ques1 = standingDataService.GetDataById(districtQuesitonsIds.First());
                    if (ques1 != null)
                    {
                        up.Question1 = ques1.Name;
                        up.Question1Id = ques1.Id;
                    }
                    var ques2 = standingDataService.GetDataById(districtQuesitonsIds.Last());
                    if (ques2 != null)
                    {
                        up.Question2 = ques2.Name;
                        up.Question2Id = ques2.Id;
                    }
                }

            }

            return PartialView(up);
        }

        [CustomAuthorize(Roles: new string[] { "DISTRICT" })]
        public ActionResult Save(DistrictSubmitModel model)
        {

            DistrictData entityDis = disDataService.Get(model.DistrictId, model.Date);
            DistrictSummery entityDisSum = disDataService.GetSummery(model.DistrictId);

            if (entityDisSum == null)
            {
                entityDisSum = new DistrictSummery();
            }

            if (entityDis == null)
            {
                entityDis = new DistrictData();
                entityDisSum.TotalQuarantine = entityDisSum.TotalQuarantine + model.NewQuarantine;
                entityDisSum.TotalReleased = entityDisSum.TotalReleased + model.ReleasedQuarantine;
                entityDisSum.TotalDoTestOn = entityDisSum.TotalDoTestOn + model.DoTestOn;
                entityDisSum.TotalDeath = entityDisSum.TotalDeath + model.Death;

                entityDisSum.TotalReliefFamily = entityDisSum.TotalReliefFamily + model.ReliefFamily;
                entityDisSum.TotalReliefPerson = entityDisSum.TotalReliefPerson + model.ReliefPerson;
                entityDisSum.TotalRice = entityDisSum.TotalRice + model.Rice;
                entityDisSum.TotalDal = entityDisSum.TotalDal + model.Dal;
                entityDisSum.TotalPotato = entityDisSum.TotalPotato + model.Potato;
                entityDisSum.TotalMoney = entityDisSum.TotalMoney + model.Money;

                entityDisSum.AgroLaborSalaryMale = entityDisSum.AgroLaborSalaryMale + model.AgroLaborSalaryMale;
                entityDisSum.AgroLaborExpMale = entityDisSum.AgroLaborExpMale + model.AgroLaborExpMale;
                entityDisSum.CarpenterSalaryMale = entityDisSum.CarpenterSalaryMale + model.CarpenterSalaryMale;

                entityDisSum.CarpenterAssSalaryMale = entityDisSum.CarpenterAssSalaryMale + model.CarpenterAssSalaryMale;
                entityDisSum.RickPullIncome = entityDisSum.RickPullIncome + model.RickPullIncome;
                entityDisSum.RickPullExp = entityDisSum.RickPullExp + model.RickPullExp;
                entityDisSum.AgroLaborSalaryFemale = entityDisSum.AgroLaborSalaryFemale + model.AgroLaborSalaryFemale;
                entityDisSum.AgroLaborExpFemale = entityDisSum.AgroLaborExpFemale + model.AgroLaborExpFemale;
                entityDisSum.CarpenterAssSalaryFemale = entityDisSum.CarpenterAssSalaryFemale + model.CarpenterAssSalaryFemale;


                //entityDisSum.TotalBracPatient = entityDisSum.TotalBracPatient + model.BracPatient;
                //entityDisSum.TotalBracReleased = entityDisSum.TotalBracReleased + model.BracReleased;
                //entityDisSum.TotalBracQurantine = entityDisSum.TotalBracQurantine + model.BracQurantine;

                //entityDisSum.TotalOil = entityDisSum.TotalOil + model.Oil;
                //entityDisSum.TotalSoap = entityDisSum.TotalSoap + model.Soap;
                //entityDisSum.TotalOnion = entityDisSum.TotalOnion + model.Onion;
                //entityDisSum.TotalSalt = entityDisSum.TotalSalt + model.Salt;
            }
            else
            {
                entityDisSum.TotalQuarantine = entityDisSum.TotalQuarantine + (model.NewQuarantine - entityDis.NewQuarantine);
                entityDisSum.TotalReleased = entityDisSum.TotalReleased + (model.ReleasedQuarantine - entityDis.ReleasedQuarantine);
                entityDisSum.TotalDoTestOn = entityDisSum.TotalDoTestOn + (model.DoTestOn - entityDis.DoTestOn);
                entityDisSum.TotalDeath = entityDisSum.TotalDeath + (model.Death - entityDis.Death);

                entityDisSum.TotalReliefFamily = entityDisSum.TotalReliefFamily + (model.ReliefFamily - entityDis.ReliefFamily);
                entityDisSum.TotalReliefPerson = entityDisSum.TotalReliefPerson + (model.ReliefPerson - entityDis.ReliefPerson);
                entityDisSum.TotalRice = entityDisSum.TotalRice + (model.Rice - entityDis.Rice);
                entityDisSum.TotalDal = entityDisSum.TotalDal + (model.Dal - entityDis.Dal);
                entityDisSum.TotalPotato = entityDisSum.TotalPotato + (model.Potato - entityDis.Potato);
                entityDisSum.TotalMoney = entityDisSum.TotalMoney + (model.Money - entityDis.Money);


                //============
                entityDisSum.AgroLaborSalaryMale = entityDisSum.AgroLaborSalaryMale + (model.AgroLaborSalaryMale- entityDis.AgroLaborSalaryMale);
                entityDisSum.AgroLaborExpMale = entityDisSum.AgroLaborExpMale + (model.AgroLaborExpMale - entityDis.AgroLaborExpMale);
                entityDisSum.CarpenterSalaryMale = entityDisSum.CarpenterSalaryMale + (model.CarpenterSalaryMale - entityDis.CarpenterSalaryMale);

                entityDisSum.CarpenterAssSalaryMale = entityDisSum.CarpenterAssSalaryMale + (model.CarpenterAssSalaryMale - entityDis.CarpenterAssSalaryMale);
                entityDisSum.RickPullIncome = entityDisSum.RickPullIncome + (model.RickPullIncome - entityDis.RickPullIncome);
                entityDisSum.RickPullExp = entityDisSum.RickPullExp + (model.RickPullExp - entityDis.RickPullExp);
                entityDisSum.AgroLaborSalaryFemale = entityDisSum.AgroLaborSalaryFemale + (model.AgroLaborSalaryFemale - entityDis.AgroLaborSalaryFemale);
                entityDisSum.AgroLaborExpFemale = entityDisSum.AgroLaborExpFemale + (model.AgroLaborExpFemale - entityDis.AgroLaborExpFemale);
                entityDisSum.CarpenterAssSalaryFemale = entityDisSum.CarpenterAssSalaryFemale + (model.CarpenterAssSalaryFemale - entityDis.CarpenterAssSalaryFemale);




                //entityDisSum.TotalBracPatient = entityDisSum.TotalBracPatient + (model.BracPatient - entityDis.BracPatient);
                //entityDisSum.TotalBracReleased = entityDisSum.TotalBracReleased + (model.BracReleased - entityDis.BracReleased);
                //entityDisSum.TotalBracQurantine = entityDisSum.TotalBracQurantine + (model.BracQurantine - entityDis.BracQurantine);

                //entityDisSum.TotalOil = entityDisSum.TotalOil + (model.Oil- entityDis.TotalOil);
                //entityDisSum.TotalSoap = entityDisSum.TotalSoap +( model.Soap- entityDis.TotalSoap);
                //entityDisSum.TotalOnion = entityDisSum.TotalOnion + (model.Onion- entityDis.TotalOnion);
                //entityDisSum.TotalSalt = entityDisSum.TotalSalt + (model.Salt- entityDis.TotalSalt);

            }

            entityDisSum.CurrentQuarantine = entityDisSum.TotalQuarantine
                - entityDisSum.TotalReleased;

            //running qrn, death, release, new qrn

            int? PriceRice = entityDisSum.PriceRice;
            int? PriceDal = entityDisSum.PriceDal;
            int? PricePotato = entityDisSum.PricePotato;
            int? PriceOnion = entityDisSum.PriceOnion;
            int? PriceOil = entityDisSum.PriceOil;
            int? PriceOilPack = entityDisSum.PriceOilPack;
            int? PriceSalt = entityDisSum.PriceSalt;
            int? PriceEggPlant = entityDisSum.PriceEggPlant;
            int? PriceEgg = entityDisSum.PriceEgg;
            int? PriceChille = entityDisSum.PriceChille;
            int? PricePumpkin = entityDisSum.PricePumpkin;
            string PriceComment = entityDisSum.PriceComment;


            int? AgroLaborSalaryMale = entityDisSum.AgroLaborSalaryMale;
            int? AgroLaborExpMale = entityDisSum.AgroLaborExpMale;
            int? CarpenterSalaryMale = entityDisSum.CarpenterSalaryMale;
            int? CarpenterAssSalaryMale = entityDisSum.CarpenterAssSalaryMale;
            int? RickPullIncome = entityDisSum.RickPullIncome;
            int? RickPullExp = entityDisSum.RickPullExp;
            int? AgroLaborSalaryFemale = entityDisSum.AgroLaborSalaryFemale;
            int? AgroLaborExpFemale = entityDisSum.AgroLaborExpFemale;
            int? CarpenterAssSalaryFemale = entityDisSum.CarpenterAssSalaryFemale;


            ModelCopier.CopyModel(model, entityDis);
            ModelCopier.CopyModel(model, entityDisSum);

            if (model.IsPriceUpdate)
            {
                entityDisSum.PriceDate = model.Date;
            }
            else
            {
                entityDisSum.PriceRice = PriceRice;
                entityDisSum.PriceDal = PriceDal;
                entityDisSum.PricePotato = PricePotato;
                entityDisSum.PriceOnion = PriceOnion;
                entityDisSum.PriceOil = PriceOil;
                entityDisSum.PriceOilPack = PriceOilPack;
                entityDisSum.PriceSalt = PriceSalt;
                entityDisSum.PriceEggPlant = PriceEggPlant;
                entityDisSum.PriceEgg = PriceEgg;
                entityDisSum.PriceChille = PriceChille;
                entityDisSum.PricePumpkin = PricePumpkin;
                entityDisSum.PriceComment = PriceComment;


                //=========================
                entityDisSum.AgroLaborSalaryMale = AgroLaborSalaryMale;
                entityDisSum.AgroLaborExpMale = AgroLaborExpMale;
                entityDisSum.CarpenterSalaryMale=CarpenterSalaryMale;
                entityDisSum.CarpenterAssSalaryMale= CarpenterAssSalaryMale;
                entityDisSum.RickPullIncome= RickPullIncome;
                entityDisSum.RickPullExp = RickPullExp;
                entityDisSum.AgroLaborSalaryFemale = AgroLaborSalaryFemale;
                entityDisSum.AgroLaborExpFemale = AgroLaborExpFemale;
                entityDisSum.CarpenterAssSalaryFemale = CarpenterAssSalaryFemale;

            }


            entityDis.Id = model.DetailId;
            entityDis.Remarks = model.RemarksDetail;

            entityDisSum.Id = model.SumId;
            entityDisSum.Remarks = model.RemarksSum;

            entityDis.InsertedById = SessionHelper.UserId;
            entityDisSum.InsertedById = SessionHelper.UserId;

            disDataService.Update(entityDis, entityDisSum);

            unitOfWork.Commit();

            return PartialView();

        }

    }
}
