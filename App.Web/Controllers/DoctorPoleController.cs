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
using static AppProj.Web.StandingDataTypes;
using AppProj.Domain.ModelExt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace AppProj.Web.Controllers
{
    /*
    standing data
  0 > Gender
  1 > program
  2 > test result-2
  3
  4 > blood group
  5 > division
  6 > district
  7 > who effected
  8 > status
  9
  10 > test result-1
  11 > test result
  12 > AdmittedTypeId
         */
    [Authorize]
    [CustomAuthorize(Roles: new string[] { "DoctorPool_HR"
        , "DoctorPool_Doc"
        , "DoctorPool_Suspect"
        , "DoctorPool_Admin"
        , "DoctorPool_Council" })]
    public class DoctorPoleController : Controller
    {
        readonly IDoctorsPoleService service;
        readonly IStandingDataService standingDataService;
        readonly IUserProfileService userProfile;
        readonly IUnitOfWork unitOfWork;

        public DoctorPoleController(IDoctorsPoleService service
            , IStandingDataService standingDataService
            , IUserProfileService userProfile
            , UnitOfWork unitOfWork)
        {
            this.service = service;
            this.standingDataService = standingDataService;
            this.userProfile = userProfile;
            this.unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            var model = service.GetPersonal(SessionHelper.PIN);

            return View(model);
        }

        [CustomAuthorize(Roles: new string[] { "DoctorPool_HR" })]
        public ActionResult HrIndex()
        {
            SearchModel up = new SearchModel();

            var proj = standingDataService.GetProject().Where(r => r.IsActive);
            up.ContentTypes1 = proj.ToSelectList(null, "Id", "Name");

            var div = standingDataService.GetDivisions().Where(r => r.IsActive);
            up.ContentTypes2 = div.ToSelectList(null, "Id", "Name");

            var dis = standingDataService.GetDistricts(0).Where(r => r.IsActive);
            up.ContentTypes3 = dis.ToSelectList(null, "Id", "Name");

            up.FromDate = DateTime.Now.AddDays(-7);
            up.ToDate = DateTime.Now;

            return View(up);
        }

        [CustomAuthorize(Roles: new string[] { "DoctorPool_HR", "DoctorPool_Doc" })]
        public ActionResult Create()
        {
            DoctorsPoleShortModel model = new DoctorsPoleShortModel();

            return PartialView(model);
        }

        [CustomAuthorize(Roles: new string[] { "DoctorPool_HR", "DoctorPool_Doc" })]
        public ActionResult CreateDetail(DoctorsPoleShortModel mod)
        {
            DoctorsPoleModel model = new DoctorsPoleModel();
            string srcTxt = "";

            model.PIN = ("" + mod.PIN_).Trim();
            model.MobileNo = ("" + mod.MobileNo_).Trim();

            IEnumerable<StaffProfile> dataObjects = null;
            StaffProfile staff = null;

            if (!String.IsNullOrEmpty(model.PIN))
            {
                dataObjects = ApiCaller.GetEmployeeByPIN(model.PIN);
                srcTxt = model.PIN;
            }
            else if (!String.IsNullOrEmpty(model.MobileNo))
            {
                dataObjects = ApiCaller.GetEmployeeByMobile(model.MobileNo);
                srcTxt = model.MobileNo;
            }


            if (dataObjects != null)
            {
                staff = dataObjects.FirstOrDefault();
            }

            if (staff == null)
            {
                model.Message = "Staff not found by PIN or mobile";

                var prj = standingDataService.GetProject();
                model.ProgramList = prj.ToSelectList(null, "Id", "Name");

                var div = standingDataService.GetDivisions().Where(r => r.IsActive);
                var fd = div.FirstOrDefault().Id;

                model.DivisionList = div.ToSelectList(fd, "Id", "Name");

                var dis = standingDataService.GetDistricts(fd).Where(r => r.IsActive);
                model.DistrictList = dis.ToSelectList(null, "Id", "Name");

                var gen = standingDataService.GetGender().Where(r => r.IsActive);
                model.GenderList = gen.ToSelectList(null, "Id", "Name");

                var bg = standingDataService.GetBloodGroup().Where(r => r.IsActive);
                model.BloodGroupList = bg.ToSelectList(null, "Id", "Name");

                model.ExistingData = service.GetPersonal(srcTxt).ToList();

                model.IsFound = false;
            }
            else
            {
                model.Message = "";
                model.PIN = staff.pin;
                model.IsFound = true;
                model.Designation = staff.Designationname;
                
                model.ExistingData = service.GetPersonal(staff.pin).ToList();

                var prjId = GetProject(staff.projectname);

                var prj = standingDataService.GetProject().Where(r => r.IsActive);
                model.ProgramList = prj.ToSelectList(prjId, "Id", "Name");
                model.ProgramId = prjId;

                var disOfStaff = standingDataService.GetDistricts(staff.districtname);
                var genOfStaff = standingDataService.GetGender(staff.sex);
                var bgOfStaff = standingDataService.GetBloodGroup(staff.BloodGroup);

                var div = standingDataService.GetDivisions().Where(r => r.IsActive);
                var gen = standingDataService.GetGender().Where(r => r.IsActive);

                var bg = standingDataService.GetBloodGroup().Where(r => r.IsActive);
                model.BloodGroupList = bg.ToSelectList(null, "Id", "Name");
                if (bgOfStaff != null)
                {
                    model.BloodGroupId = bgOfStaff.Id;
                }

                if (genOfStaff != null)
                {
                    model.GenderList = gen.ToSelectList(genOfStaff.Id, "Id", "Name");
                    model.GenderId = genOfStaff.Id;
                }
                else
                {
                    model.GenderList = gen.ToSelectList(null, "Id", "Name");
                }

                if (disOfStaff != null)
                {
                    var dis = standingDataService.GetDistricts(disOfStaff.ParentId.Value).Where(r => r.IsActive);

                    model.DistrictList = dis.ToSelectList(disOfStaff.Id, "Id", "Name");
                    model.DivisionList = div.ToSelectList(disOfStaff.ParentId, "Id", "Name");

                    model.DivisionId = disOfStaff.ParentId.Value;
                    model.DistrictId = disOfStaff.Id;

                }
                else
                {
                    var fd = div.FirstOrDefault().Id;

                    var dis = standingDataService.GetDistricts(fd).Where(r => r.IsActive);
                    model.DistrictList = dis.ToSelectList(null, "Id", "Name");

                    model.DivisionList = div.ToSelectList(fd, "Id", "Name");

                }


                model.Name = staff.StaffName;
                model.StaffName = staff.StaffName;
                model.MobileNo = staff.MobileNo;

                if (staff.dateofbirth != null)
                {
                    model.Dob = staff.dateofbirth;
                    model.Age = (int)(((DateTime.Now - staff.dateofbirth.Value).TotalDays) / 365);
                }

                model.AreaOffice = staff.branchname;

            }

            var eff = standingDataService.GetByType(StandingDataTypes.Doctor_EffectedPerson)
                    .Where(r => r.IsActive)
                    .OrderBy(c => c.IntValue);
            model.EffectedPersonList = eff.ToSelectList(null, "Id", "Name");

            //var ofc = standingDataService.GetByType(StandingDataTypes.Doctor_IsolationOffices)
            //    .Where(r => r.IsActive)
            //    .OrderBy(c => c.Name);
            //model.IsolationOffices = ofc.ToSelectList(null, "Id", "Name");

            return PartialView(model);
        }

        [CustomAuthorize(Roles: new string[] { "DoctorPool_Admin" })]
        public ActionResult Edit(int Id)
        {
            DoctorsPoleModel model = new DoctorsPoleModel();

            var entity = service.Get(Id);

            ModelCopier.CopyModel(entity, model);


            var prj = standingDataService.GetProject().Where(r => r.IsActive);
            model.ProgramList = prj.ToSelectList(null, "Id", "Name");

            var div = standingDataService.GetDivisions().Where(r => r.IsActive);
            model.DivisionList = div.ToSelectList(null, "Id", "Name");

            var dis = standingDataService.GetDistricts(model.DivisionId).Where(r => r.IsActive);
            model.DistrictList = dis.ToSelectList(null, "Id", "Name");

            var gen = standingDataService.GetGender().Where(r => r.IsActive).OrderBy(c => c.IntValue);
            model.GenderList = gen.ToSelectList(null, "Id", "Name");

            var bg = standingDataService.GetBloodGroup().Where(r => r.IsActive).OrderBy(c => c.Name);
            model.BloodGroupList = bg.ToSelectList(null, "Id", "Name");

            var eff = standingDataService.GetByType(StandingDataTypes.Doctor_EffectedPerson)
                    .Where(r => r.IsActive)
                    .OrderBy(c => c.IntValue);
            model.EffectedPersonList = eff.ToSelectList(null, "Id", "Name");

            return PartialView("Edit", model);
        }

        [CustomAuthorize(Roles: new string[] { "DoctorPool_HR", "DoctorPool_Doc" })]
        public ActionResult Save(DoctorsPoleModel model)
        {

            DoctorsPole entity = new DoctorsPole();

            ModelCopier.CopyModel(model, entity);

            var ststId = standingDataService.GetByType(StandingDataTypes.Doctor_Status)
                    .Where(r => r.IntValue == 0).FirstOrDefault().Id;

            entity.EntryById = SessionHelper.UserId;
            entity.EntryTime = DateTime.Now;
            entity.StatusId = ststId;

            if (UserRole.Check("DoctorPool_Doc", SessionHelper.Role))
            {
                entity.EntryByDept = DoctorsPoleDataInputBy.Doctor;
            }
            else if (UserRole.Check("DoctorPool_HR", SessionHelper.Role))
            {
                entity.EntryByDept = DoctorsPoleDataInputBy.Hrd;
            }

            if (model.Id == 0)
            {
                service.Add(entity);
            }
            else
            {
                service.Update(entity);
            }

            unitOfWork.Commit();

            if (UserRole.Check("DoctorPool_Doc", SessionHelper.Role))
            {
                return RedirectToAction("Followup", new { id = entity.Id });
            }

            return PartialView();
        }

        [CustomAuthorize(Roles: new string[] { "DoctorPool_HR" })]
        public JsonResult HrDataGrid()
        {
            bool visible = UserRole.Check("DoctorPool_HR", SessionHelper.Role);

            int ec = int.Parse(Request.QueryString["sEcho"]);
            int take = int.Parse(Request.QueryString["iDisplayLength"]);
            int skip = int.Parse(Request.QueryString["iDisplayStart"]);

            if (take == -1) { take = 100000000; skip = 0; }

            int? srcId = null;

            try
            {
                srcId = Convert.ToInt32(Request.QueryString["ContentTypeId1"]);
            }
            catch { }

            int? divId = null;

            try
            {
                divId = Convert.ToInt32(Request.QueryString["ContentTypeId2"]);
            }
            catch { }

            int? disId = null;
            try
            {
                disId = Convert.ToInt32(Request.QueryString["ContentTypeId3"]);
            }
            catch { }



            DateTime FromDate = Convert.ToDateTime(Request.QueryString["FromDate"]);
            DateTime ToDate = Convert.ToDateTime(Request.QueryString["ToDate"]);

            int count = 0;
            IEnumerable<DoctorsPole> dataList = service.Get(srcId, divId, disId, FromDate, ToDate, skip, take, DoctorsPoleDataInputBy.Hrd, out count);


            var obj = (from c in dataList
                       select new object[] {c.StandingData1==null?"":c.StandingData1.Name
                       ,c.StandingData3==null?"":c.StandingData3.Name
                       ,c.StandingData4==null?"":c.StandingData4.Name
                       ,c.AreaOffice
                       , String.Format("{0:dd MMM, yyyy}", c.EntryTime)
                       ,String.Format("{0:dd MMM, yyyy}", c.FirstDoctorCallTime)
                       ,c.Name
                       ,c.Age
                       , c.StandingData==null?"":c.StandingData.Name
                       , c.StandingData5==null?"":c.StandingData5.Name
                       , c.StandingData6==null?"":c.StandingData6.Name
                       ,c.UserProfile.UserName
                //,new GridButtonModel[]
                //    {
                //         //new GridButtonModel{U=Url.Action("Edit",new {Id=c.Id}), T="Edit", D = GridButtonDialog.dialig1.ToString(), H="Edit", M="class=\"btn btn-mini btn-warning\""
                //         //, V = (visible && c.FirstDoctorCallTime==null)}

                //    }
            }).ToArray();


            JQueryDataTable js = new JQueryDataTable();
            js.sEcho = ec;
            js.iTotalDisplayRecords = count.ToString();
            js.iTotalRecords = js.iTotalDisplayRecords;
            js.aaData = obj;

            return Json(js, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(Roles: new string[] { "DoctorPool_Doc", "DoctorPool_Suspect" })]
        public ActionResult Followup(int id)
        {
            DoctorsPoleVisitModel model = new DoctorsPoleVisitModel();
            model.DoctorsPole = service.Get(id);

            var stst = standingDataService.GetByType(StandingDataTypes.Doctor_Status)
                .Where(r => r.IsActive)
                .OrderBy(c => c.IntValue);
            model.StatusList = stst.ToSelectList(model.DoctorsPole.StatusId, "Id", "Name");

            var ts = standingDataService.GetByType(StandingDataTypes.Doctor_TestResult).Where(r => r.IsActive);
            model.TestResultList = ts.ToSelectList(null, "Id", "Name");

            var ad = standingDataService.GetByType(StandingDataTypes.Doctor_AdmissionType).Where(r => r.IsActive);
            model.AdminTypeList = ad.ToSelectList(null, "Id", "Name");

            model.DoctorsPoleVisits = service.GetVisitsByParent(id).ToList();

            var co = standingDataService.GetByType(StandingDataTypes.Doctor_CoMorbidity).Where(r => r.IsActive);
            model.CoMobiList = co.ToSelectList(null, "Id", "Name").ToList();

            var last = model.DoctorsPoleVisits
                .OrderByDescending(c => c.EntryTime)
                .Take(1).Skip(0)
                .FirstOrDefault();

            if (last != null)
            {
                ModelCopier.CopyModel(last, model);

                var lastCo = service.GetVisitDetailsByParent(last.Id);
                foreach (var r in lastCo)
                {
                    try
                    {
                        model.CoMobiList.Where(c => c.Value == r.RelationStandingDataId.ToString()).FirstOrDefault().Selected = true;
                    }
                    catch
                    { }
                }
            }

            model.DoctorPoleId = id;

            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem { Text = "After 1 day", Value = "1" });
            lst.Add(new SelectListItem { Text = "After 3 days", Value = "3" });
            lst.Add(new SelectListItem { Text = "After 5 days", Value = "5" });
            lst.Add(new SelectListItem { Text = "After 7 days", Value = "7" });
            lst.Add(new SelectListItem { Text = "After 9 days", Value = "9" });
            lst.Add(new SelectListItem { Text = "After 12 days", Value = "12" });
            lst.Add(new SelectListItem { Text = "After 14 days", Value = "14" });
            lst.Add(new SelectListItem { Text = "Today", Value = "0" });
            lst.Add(new SelectListItem { Text = "No need any followup", Value = "-1" });
            model.FollowupAfterDaysList = lst;

            var ofc = standingDataService.GetByType(StandingDataTypes.Doctor_IsolationOffices)
                .Where(r => r.IsActive)
                .OrderBy(c => c.Name);
            model.IsolationOffices = ofc.ToSelectList(null, "Id", "Name");


            return PartialView(model);
        }

        [CustomAuthorize(Roles: new string[] { "DoctorPool_Doc", "DoctorPool_Suspect" })]
        public ActionResult FollowupSave(DoctorsPoleVisitModel model)
        {
            var entity = service.Get(model.DoctorPoleId);
            int oldEb = entity.EntryById;
            DateTime oldEt = entity.EntryTime;
            DateTime oldFst = entity.FirstDoctorCallTime ?? DateTime.Now;

            string comobStr = string.Join(", ", model.CoMobiList.Where(d => d.Selected).Select(c => c.Text).ToArray());

            if (model.FollowupAfterDays == -1)
            {
                model.FollowupAfterDays = 36500;
            }
            entity.NextFollowupDate = DateTime.Now.AddDays(model.FollowupAfterDays ?? 0);

            ModelCopier.CopyModel(model, entity);

            entity.LastUpdateById = SessionHelper.UserId;
            entity.LastUpdateTime = DateTime.Now;
            entity.EntryById = oldEb;
            entity.EntryTime = oldEt;
            entity.FirstDoctorCallTime = oldFst;
            entity.CoMorbidities = comobStr;

            service.Update(entity);

            DoctorsPoleVisit entityVisit = new DoctorsPoleVisit();

            ModelCopier.CopyModel(model, entityVisit);

            entityVisit.EntryById = SessionHelper.UserId;
            entityVisit.EntryTime = DateTime.Now;
            entityVisit.CoMorbidities = comobStr;

            service.AddVisit(entityVisit);
            unitOfWork.Commit();

            service.DeleteVisitDetailAllByParent(entityVisit.Id);

            foreach (var r in model.CoMobiList)
            {
                if (r.Selected)
                {
                    service.AddVisitDetail(new DoctorsPoleVisitDetail
                    {
                        DoctorsPoleVisitId = entityVisit.Id
                         ,
                        RelationStandingDataId = int.Parse(r.Value)
                        ,
                        RelationType = DoctorsPoleVisitVetailRelationType.CoMorbidity
                    });
                }
            }

            unitOfWork.Commit();

            return PartialView();
        }

        [CustomAuthorize(Roles: new string[] { "DoctorPool_Doc", "DoctorPool_Suspect" })]
        public ActionResult FollowupSuspected(int id)
        {
            DoctorsPoleVisitModel model = new DoctorsPoleVisitModel();
            model.DoctorsPole = service.Get(id);

            var stst = standingDataService.GetByType(StandingDataTypes.Doctor_Status)
                .Where(r => r.IsActive)
                .OrderBy(c => c.IntValue);
            model.StatusList = stst.ToSelectList(model.DoctorsPole.StatusId, "Id", "Name");

            //var ts = standingDataService.GetByType(StandingDataTypes.Doctor_TestResult).Where(r => r.IsActive);
            //model.TestResultList = ts.ToSelectList(null, "Id", "Name");

            //var ad = standingDataService.GetByType(StandingDataTypes.Doctor_AdmissionType).Where(r => r.IsActive);
            //model.AdminTypeList = ad.ToSelectList(null, "Id", "Name");

            model.DoctorsPoleVisits = service.GetVisitsByParent(id).ToList();

            //var co = standingDataService.GetByType(StandingDataTypes.Doctor_CoMorbidity).Where(r => r.IsActive);
            //model.CoMobiList = co.ToSelectList(null, "Id", "Name").ToList();

            //var last = model.DoctorsPoleVisits
            //    .OrderByDescending(c => c.EntryTime)
            //    .Take(1).Skip(0)
            //    .FirstOrDefault();

            //if (last != null)
            //{
            //    ModelCopier.CopyModel(last, model);

            //    var lastCo = service.GetVisitDetailsByParent(last.Id);
            //    foreach (var r in lastCo)
            //    {
            //        try
            //        {
            //            model.CoMobiList.Where(c => c.Value == r.RelationStandingDataId.ToString()).FirstOrDefault().Selected = true;
            //        }
            //        catch
            //        { }
            //    }
            //}

            model.DoctorPoleId = id;

            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem { Text = "Today", Value = "0" });
            lst.Add(new SelectListItem { Text = "After 1 day", Value = "1" });
            lst.Add(new SelectListItem { Text = "After 3 days", Value = "3" });
            lst.Add(new SelectListItem { Text = "After 5 days", Value = "5" });
            lst.Add(new SelectListItem { Text = "After 7 days", Value = "7" });
            lst.Add(new SelectListItem { Text = "After 9 days", Value = "9" });
            lst.Add(new SelectListItem { Text = "After 12 days", Value = "12" });
            lst.Add(new SelectListItem { Text = "After 14 days", Value = "14" });
            lst.Add(new SelectListItem { Text = "No Need to followup", Value = "-1" });
            model.FollowupAfterDaysList = lst;

            return PartialView(model);
        }

        [CustomAuthorize(Roles: new string[] { "DoctorPool_Doc", "DoctorPool_Suspect" })]
        public ActionResult FollowupSuspectedSave(DoctorsPoleVisitModel model)
        {
            var entity = service.Get(model.DoctorPoleId);
            int oldEb = entity.EntryById;
            DateTime oldEt = entity.EntryTime;
            DateTime oldFst = entity.FirstDoctorCallTime ?? DateTime.Now;

            if (model.FollowupAfterDays == -1)
            {
                model.FollowupAfterDays = 36500;
            }

            entity.NextFollowupDate = DateTime.Now.AddDays(model.FollowupAfterDays ?? 0);

            ModelCopier.CopyModel(model, entity);

            entity.LastUpdateById = SessionHelper.UserId;
            entity.LastUpdateTime = DateTime.Now;
            entity.EntryById = oldEb;
            entity.EntryTime = oldEt;
            entity.FirstDoctorCallTime = oldFst;

            service.Update(entity);

            unitOfWork.Commit();

            return PartialView("Followup" +
                "Save");
        }

        [CustomAuthorize(Roles: new string[] { "DoctorPool_Doc", "DoctorPool_Suspect", "DoctorPool_Council", "DoctorPool_Admin" })]
        public ActionResult FollowupIndex()
        {
            SearchModel up = new SearchModel();
            bool visibleDoc = UserRole.Check("DoctorPool_Doc", SessionHelper.Role);
            bool visibleSus = UserRole.Check("DoctorPool_Suspect", SessionHelper.Role);
            bool visibleCouncil = UserRole.Check("DoctorPool_Council", SessionHelper.Role);

            var prj = standingDataService.GetProject().Where(r => r.IsActive);
            up.ContentTypes1 = prj.ToSelectList(null, "Id", "Name");

            var div = standingDataService.GetDivisions().Where(r => r.IsActive);
            up.ContentTypes2 = div.ToSelectList(null, "Id", "Name");

            var dis = standingDataService.GetDistricts(0).Where(r => r.IsActive);
            up.ContentTypes3 = dis.ToSelectList(null, "Id", "Name");

            var co = new List<StandingData>();

            if (visibleDoc || visibleCouncil)
            {
                co = standingDataService.GetByType(StandingDataTypes.Doctor_Status)
                    .Where(c => (c.IntValue == 0 || c.IntValue == 1 || c.IntValue == 2 || c.IntValue == 3))
                    .OrderBy(c => c.IntValue)
                    .ToList();
            }
            else if (visibleSus)
            {
                co = standingDataService.GetByType(StandingDataTypes.Doctor_Status)
                    .Where(c => (c.IntValue == 0 || c.IntValue == 1 || c.IntValue == 2))
                    .OrderBy(c => c.IntValue)
                    .ToList();
            }

            up.StatusList = co.ToSelectList(null, "Id", "Name").ToList();
            //up.StatusList.ForEach(c => c.Selected = true);

            up.FromDate = DateTime.Now.AddDays(-7);
            up.ToDate = DateTime.Now;

            return View(up);
        }

        [CustomAuthorize(Roles: new string[] { "DoctorPool_Doc", "DoctorPool_Suspect", "DoctorPool_Council", "DoctorPool_Admin" })]
        public JsonResult FollowupDataGrid()
        {
            bool visibleDoc = UserRole.Check("DoctorPool_Doc", SessionHelper.Role);
            bool visibleSus = UserRole.Check("DoctorPool_Suspect", SessionHelper.Role);
            bool visibleAdmin = UserRole.Check("DoctorPool_Admin", SessionHelper.Role);
            bool visibleCouncil = UserRole.Check("DoctorPool_Council", SessionHelper.Role);

            int ec = int.Parse(Request.QueryString["sEcho"]);
            int take = int.Parse(Request.QueryString["iDisplayLength"]);
            int skip = int.Parse(Request.QueryString["iDisplayStart"]);

            if (take == -1) { take = 100000000; skip = 0; }

            string tmpSts = Request.QueryString["StatusIds"];
            bool isStaffOnly = false;
            List<int> statusIds = new List<int>();
            if (!string.IsNullOrEmpty(tmpSts))
            {
                statusIds = tmpSts.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                if (statusIds.IndexOf(-1) >= 0)
                {
                    statusIds.Remove(-1);
                    isStaffOnly = true;
                }
            }
            int? srcId = null;

            try
            {
                srcId = Convert.ToInt32(Request.QueryString["ContentTypeId1"]);
            }
            catch { }

            int? divId = null;

            try
            {
                divId = Convert.ToInt32(Request.QueryString["ContentTypeId2"]);
            }
            catch { }

            int? disId = null;
            try
            {
                disId = Convert.ToInt32(Request.QueryString["ContentTypeId3"]);
            }
            catch { }

            int? effectedTypeId = null;

            if (isStaffOnly)
            {
                effectedTypeId = standingDataService.GetByType(StandingDataTypes.Doctor_EffectedPerson)
                    .Where(c => c.IntValue == 1)
                    .FirstOrDefault().Id;
            }

            int count = 0;
            IEnumerable<DoctorsPole> dataList = service.GetFollowup(effectedTypeId, srcId, divId, disId, statusIds, skip, take, out count);


            var obj = (from c in dataList
                       select new object[] {
                        c.StandingData7==null?"":c.StandingData7.Name
                        ,c.PIN
                        ,c.Name
                        ,c.MobileNo
                        ,c.StandingData4==null?"":c.StandingData4.Name
                        ,c.Age
                        ,c.StandingData==null?"":c.StandingData.Name
                        ,c.Designation
                       ,c.StandingData1==null?"":c.StandingData1.Name
                       ,c.StandingData6==null?"":c.StandingData6.Name
                       ,c.AreaOffice
                       ,c.StandingData12==null?"" : (c.StandingData12.Name + " "+c.HospitalName)
                       ,c.AlternateName + "<br />" +c.AlternatePhoneNo

                       ,String.Format("{0:dd MMM, yyyy}", c.NextFollowupDate)
                       //,c.UserProfile2==null?"":c.UserProfile2.UserName
                       //,String.Format("{0:dd MMM, yyyy}", c.LastCouncilingDate)
                ,new GridButtonModel[]
                    {
                         new GridButtonModel{U=Url.Action("Followup",new {Id=c.Id}), T="Followup", D = GridButtonDialog.dialig1.ToString(), H="Followup", M="class=\"\"", V = (visibleDoc)}
                         ,new GridButtonModel{U=Url.Action("FollowupSuspected",new {Id=c.Id}), T="Suspected", D = GridButtonDialog.dialig1.ToString(), H="Suspected Patient", M="class=\"\"", V = (visibleSus)}
                         ,new GridButtonModel {U=Url.Action("Edit",new {Id=c.Id}), T="Edit", D = GridButtonDialog.dialig1.ToString(), H="Edit", M="class=\"\"", V = (visibleAdmin)}
                         ,new GridButtonModel {U=Url.Action("Counciling",new {Id=c.Id}), T="Counseling", D = GridButtonDialog.dialig1.ToString(), H="Counseling", M="class=\"\"", V = (visibleCouncil)}
                    }
            }).ToArray();


            JQueryDataTable js = new JQueryDataTable();
            js.sEcho = ec;
            js.iTotalDisplayRecords = count.ToString();
            js.iTotalRecords = js.iTotalDisplayRecords;
            js.aaData = obj;

            return Json(js, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReportIndex()
        {
            SearchModel up = new SearchModel();

            var prj = standingDataService.GetProject().Where(r => r.IsActive);
            up.ContentTypes1 = prj.ToSelectList(null, "Id", "Name");

            var div = standingDataService.GetDivisions().Where(r => r.IsActive);
            up.ContentTypes2 = div.ToSelectList(null, "Id", "Name");

            var dis = standingDataService.GetDistricts(0).Where(r => r.IsActive);
            up.ContentTypes3 = dis.ToSelectList(null, "Id", "Name");

            var co = standingDataService.GetByType(StandingDataTypes.Doctor_Status)
                .Where(r => r.IsActive)
                .OrderBy(c => c.IntValue);
            up.StatusList = co.ToSelectList(null, "Id", "Name").ToList();
            //up.StatusList.ForEach(c => c.Selected = true);


            up.FromDate = DateTime.Now.AddDays(-7);
            up.ToDate = DateTime.Now;

            return View(up);
        }

        public JsonResult ReportDataGrid()
        {
            bool visibleDoc = UserRole.Check("DoctorPool_Doc", SessionHelper.Role);
            bool visibleSus = UserRole.Check("DoctorPool_Suspect", SessionHelper.Role);
            bool visibleAdmin = UserRole.Check("DoctorPool_Admin", SessionHelper.Role);
            bool visibleCouncil = UserRole.Check("DoctorPool_Council", SessionHelper.Role);

            int ec = int.Parse(Request.QueryString["sEcho"]);
            int take = int.Parse(Request.QueryString["iDisplayLength"]);
            int skip = int.Parse(Request.QueryString["iDisplayStart"]);

            string txt = Request.QueryString["SearchText"];
            string tmpSts = Request.QueryString["StatusIds"];
            string dateType = Request.QueryString["dateType"];
            bool isStaffOnly = false;

            List<int> statusIds = new List<int>();
            if (!string.IsNullOrEmpty(tmpSts))
            {
                statusIds = tmpSts.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                if (statusIds.IndexOf(-1) >= 0)
                {
                    statusIds.Remove(-1);
                    isStaffOnly = true;
                }
            }

            if (take == -1) { take = 100000000; skip = 0; }

            int? srcId = null;

            try
            {
                srcId = Convert.ToInt32(Request.QueryString["ContentTypeId1"]);
            }
            catch { }

            int? divId = null;

            try
            {
                divId = Convert.ToInt32(Request.QueryString["ContentTypeId2"]);
            }
            catch { }

            int? disId = null;
            try
            {
                disId = Convert.ToInt32(Request.QueryString["ContentTypeId3"]);
            }
            catch { }



            DateTime FromDate = Convert.ToDateTime(Request.QueryString["FromDate"]);
            DateTime ToDate = Convert.ToDateTime(Request.QueryString["ToDate"]);

            int? effectedTypeId = null;

            if (isStaffOnly)
            {
                effectedTypeId = standingDataService.GetByType(StandingDataTypes.Doctor_EffectedPerson)
                    .Where(c => c.IntValue == 1)
                    .FirstOrDefault().Id;
            }
            int count = 0;
            IEnumerable<DoctorsPole> dataList = service.Get(effectedTypeId, srcId, divId, disId, FromDate, ToDate, dateType, statusIds, txt, skip, take, out count);


            var obj = (from c in dataList
                       select new object[] {
                        c.StandingData7==null?"":c.StandingData7.Name
                        ,c.PIN
                        ,c.Name
                        ,c.MobileNo
                        ,c.StandingData4==null?"":c.StandingData4.Name
                        ,c.Age
                        ,c.StandingData==null?"":c.StandingData.Name
                        ,c.Designation
                       ,c.StandingData1==null?"":c.StandingData1.Name
                       ,c.StandingData6==null?"":c.StandingData6.Name
                       ,c.AreaOffice
                       ,c.StandingData12==null?"" : (c.StandingData12.Name + " "+c.HospitalName)
                       ,c.AlternateName + "<br />" +c.AlternatePhoneNo

                       ,String.Format("{0:dd MMM, yyyy}", c.NextFollowupDate)
                       //,c.UserProfile2==null?"":c.UserProfile2.UserName
                       //,String.Format("{0:dd MMM, yyyy}", c.LastCouncilingDate)
                ,new GridButtonModel[]
                    {
                         new GridButtonModel {U=Url.Action("Followup",new {Id=c.Id}), T="Followup", D = GridButtonDialog.dialig1.ToString(), H="Followup", M="class=\"\"", V = (visibleDoc)}
                        ,new GridButtonModel {U=Url.Action("Edit",new {Id=c.Id}), T="Edit", D = GridButtonDialog.dialig1.ToString(), H="Edit", M="class=\"\"", V = (visibleAdmin)}
                        ,new GridButtonModel {U=Url.Action("Counciling",new {Id=c.Id}), T="Counseling", D = GridButtonDialog.dialig1.ToString(), H="Counseling", M="class=\"\"", V = (visibleCouncil)}
                    }
            }).ToArray();


            JQueryDataTable js = new JQueryDataTable();
            js.sEcho = ec;
            js.iTotalDisplayRecords = count.ToString();
            js.iTotalRecords = js.iTotalDisplayRecords;
            js.aaData = obj;

            return Json(js, JsonRequestBehavior.AllowGet);
        }


        [CustomAuthorize(Roles: new string[] { "DoctorPool_Council" })]
        public ActionResult Counciling(int id)
        {
            DoctorsPoleVisitModel model = new DoctorsPoleVisitModel();
            model.DoctorsPole = service.Get(id);

            model.DoctorsPoleVisits = service.GetVisitsByParent(id).ToList();

            model.DoctorPoleCouncillings = service.GetCouncilByParent(id).ToList();

            model.DoctorPoleId = id;


            return PartialView(model);
        }

        [CustomAuthorize(Roles: new string[] { "DoctorPool_Council" })]
        public ActionResult CouncilingSave(DoctorsPoleVisitModel model)
        {
            var entity = service.Get(model.DoctorPoleId);

            entity.CouncilorUserId = SessionHelper.UserId;
            entity.LastCouncilingDate = DateTime.Now;

            service.Update(entity);


            DoctorPoleCouncilling entityC = new DoctorPoleCouncilling();

            entityC.DoctorPoleId = model.DoctorPoleId;
            entityC.Advice = model.AdviceTxt;
            entityC.Comment = model.AntibioticTxt;

            entityC.InsertedById = SessionHelper.UserId;
            entityC.InsertionTime = DateTime.Now;

            service.AddCounciling(entityC);

            unitOfWork.Commit();


            return PartialView("Save");
        }

        public ActionResult Dashboard()
        {
            DoctorPoleDashboardModel model = new DoctorPoleDashboardModel();

            model = service.Dashboard(null, 2);

            return View(model);
        }

        int GetProject(string program)
        {
            var prj = standingDataService.GetProject(program);

            if (prj == null)
            {
                standingDataService.Add(new StandingData
                {
                    Name = program
                    ,
                    Type = StandingDataTypes.Projects
                    ,
                    IsActive = true
                });
                unitOfWork.Commit();
            }

            return standingDataService.GetProject(program).Id;
        }

        public JsonResult CallForToken(int kioskId)
        {
            ApplicantApiReturnModel model = new ApplicantApiReturnModel();
            string key = ConfigurationManager.AppSettings.Get("kiosk_doctor");

            var user = userProfile.GetDataById(SessionHelper.UserId);

            if(String.IsNullOrEmpty(user.MobileNo) && String.IsNullOrEmpty(user.EmailAddress))
            {
                model.Success = false;
                model.Message = "Please update mobile number / email at your profile. For generating token you will have an OTP at email/mobile";
                return Json(model, JsonRequestBehavior.AllowGet);
            }

            var entity = service.GetKiosk(key, kioskId);

            if (entity == null)
            {
                model.Success = false;
                model.Message = "Daily quota has exceed for this user";
                return Json(model, JsonRequestBehavior.AllowGet);
            }


            if (entity.Available > 0)
            {
                entity.Available--;
                service.UpdateKioskQuota(entity);
                unitOfWork.Commit();

                var entity1 = service.GetKiosk(key, kioskId);

                if (entity1.Available < 0)
                {
                    entity1.Available++;
                    service.UpdateKioskQuota(entity1);
                    unitOfWork.Commit();

                    model.Success = false;
                    model.Message = "Daily quota has exceed for this user";
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                model.Success = false;
                model.Message = "Daily quota has exceed for this user";
                return Json(model, JsonRequestBehavior.AllowGet);
            }

            
            string URL = "https://coronatest.brac.net/api/appointment";
            string urlParameters = "?key=" + key + "&kioskId=" + kioskId + "&mobileNo=" + user.MobileNo + "&email=" + user.EmailAddress;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.

                try
                {
                    model = response.Content.ReadAsAsync<ApplicantApiReturnModel>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll

                }
                catch
                {

                }
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }
                
        public JsonResult GetToken(int kioskId, string otpKey, string otp, int profileId)
        {
            ApplicantApiReturnModel model = new ApplicantApiReturnModel();

            string key = ConfigurationManager.AppSettings.Get("kiosk_doctor");

            var dp = service.Get(profileId);

            var user = userProfile.GetDataById(SessionHelper.UserId);
            var profile = service.Get(profileId);

            ApplicantPost post = new ApplicantPost();

            post.Age = profile.Age ?? 0;
            post.BloodGroupId = profile.StandingData4 == null ? 861792 : (profile.StandingData4.IntValue ?? 861792);
            post.Email = "";
            post.GenderId = profile.StandingData == null ? 861741 : (profile.StandingData.IntValue ?? 861741); 
            post.Key= ConfigurationManager.AppSettings.Get("kiosk_doctor");
            post.KioskId = kioskId;
            post.MobileNo = profile.MobileNo;
            post.Name = profile.Name;
            post.OTP = otp;
            post.OtpKey = otpKey;
            post.RelationId = 861789;

            post.HrEmail= ConfigurationManager.AppSettings.Get("kiosk_hr_email");
            post.By_Name = user.UserName;
            post.By_Mobile = user.MobileNo;
            post.By_Email = user.EmailAddress;

            post.Staff_Program = profile.StandingData1 == null ? "" : profile.StandingData1.Name;
            post.Staff_Age = "" + profile.Age;
            post.Staff_AreaOffice = profile.AreaOffice;
            post.Staff_Designation = profile.Designation;
            post.Staff_District = profile.StandingData6 == null ? "" : profile.StandingData6.Name;
            post.Staff_Division = profile.StandingData5 == null ? "" : profile.StandingData5.Name;
            post.Staff_Email = "";
            post.Staff_Mobile = profile.MobileNo;
            post.Staff_Name = profile.Name;
            post.Staff_Pin = profile.PIN;
            post.Staff_Sex = profile.StandingData.Name;
            post.Staff_Relation = profile.StandingData7 == null ? "" : profile.StandingData7.Name;

            var json = JsonConvert.SerializeObject(post);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            string URL = "https://coronatest.brac.net/api/appointment";
            
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.PostAsync(URL, data).Result;//client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.

                try
                {
                    model = response.Content.ReadAsAsync<ApplicantApiReturnModel>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll

                }
                catch
                {

                }
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

    }

    
}
