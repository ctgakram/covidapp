using AppProj.Data.Infrastructure;
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
    public class DoctorPoleController : Controller
    {
        readonly IDoctorsPoleService service;
        readonly IStandingDataService standingDataService;
        readonly IUnitOfWork unitOfWork;

        public DoctorPoleController(IDoctorsPoleService service, IStandingDataService standingDataService
            , UnitOfWork unitOfWork)
        {
            this.service = service;
            this.standingDataService = standingDataService;
            this.unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            var model = service.GetPersonal(SessionHelper.PIN);

            return View(model);
        }

        public ActionResult HrIndex()
        {
            SearchModel up = new SearchModel();

            var source = standingDataService.GetSource().Where(r => r.IsActive);
            up.ContentTypes1 = source.ToSelectList(null, "Id", "Name");

            var div = standingDataService.GetDivisions().Where(r => r.IsActive);
            up.ContentTypes2 = div.ToSelectList(null, "Id", "Name");

            var dis = standingDataService.GetDistricts(0).Where(r => r.IsActive);
            up.ContentTypes3 = dis.ToSelectList(null, "Id", "Name");

            up.FromDate = DateTime.Now.AddDays(-7);
            up.ToDate = DateTime.Now;

            return View(up);
        }

        public ActionResult Create()
        {
            DoctorsPoleShortModel model = new DoctorsPoleShortModel();

            return PartialView(model);
        }

        public ActionResult CreateDetail(DoctorsPoleShortModel mod)
        {
            DoctorsPoleModel model = new DoctorsPoleModel();

            model.PIN = ("" + mod.PIN_).Trim();
            model.MobileNo = ("" + mod.MobileNo_).Trim();

            IEnumerable<StaffProfile> dataObjects = null;
            StaffProfile staff = null;

            if (model.PIN != "")
            {
                dataObjects = ApiCaller.GetEmployeeByPIN(model.PIN);
            }
            else if (model.MobileNo != "")
            {
                dataObjects = ApiCaller.GetEmployeeByMobile(model.MobileNo);
            }


            if (dataObjects != null)
            {
                staff = dataObjects.FirstOrDefault();
            }

            if (staff == null)
            {
                model.Message = "Staff not found by PIN or mobile";

                var source = standingDataService.GetSource();
                model.ProgramList = source.ToSelectList(null, "Id", "Name");

                var div = standingDataService.GetDivisions().Where(r => r.IsActive);
                var fd = div.FirstOrDefault().Id;

                model.DivisionList = div.ToSelectList(fd, "Id", "Name");

                var dis = standingDataService.GetDistricts(fd).Where(r => r.IsActive);
                model.DistrictList = dis.ToSelectList(null, "Id", "Name");

                var gen = standingDataService.GetGender().Where(r => r.IsActive);
                model.GenderList = gen.ToSelectList(null, "Id", "Name");

                model.IsFound = false;
            }
            else
            {
                model.Message = "";
                model.PIN = staff.pin;
                model.IsFound = true;

                model.ExistingData = service.GetPersonal(staff.pin).ToList();

                var prjId = GetProgram(staff.projectname);

                var source = standingDataService.GetSource().Where(r => r.IsActive);
                model.ProgramList = source.ToSelectList(prjId, "Id", "Name");
                model.ProgramId = prjId;

                var disOfStaff = standingDataService.GetDistricts(staff.districtname);
                var genOfStaff = standingDataService.GetGender(staff.sex);

                var div = standingDataService.GetDivisions().Where(r => r.IsActive);
                var gen = standingDataService.GetGender().Where(r => r.IsActive);



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

                var eff = standingDataService.GetByType(StandingDataTypes.Doctor_EffectedPerson).Where(r => r.IsActive);
                model.EffectedPersonList = eff.ToSelectList(null, "Id", "Name");

                model.Name = staff.StaffName;
                model.MobileNo = staff.MobileNo;

                if (staff.dateofbirth != null)
                {
                    model.Dob = staff.dateofbirth;
                    model.Age = (int)(((DateTime.Now - staff.dateofbirth.Value).TotalDays) / 365);
                }

                model.AreaOffice = staff.branchname;

            }

            model.EffectedPersonList = standingDataService.GetByType(StandingDataTypes.Doctor_EffectedPerson)
                    .Where(r => r.IsActive)
                    .ToSelectList(null, "Id", "Name");


            return PartialView(model);
        }

        public ActionResult Edit(int Id)
        {
            DoctorsPoleModel model = new DoctorsPoleModel();

            //model.PIN = ("" + mod.PIN_).Trim();
            //model.MobileNo = ("" + mod.MobileNo_).Trim();

            IEnumerable<StaffProfile> dataObjects = null;
            StaffProfile staff = null;

            if (model.PIN != "")
            {
                dataObjects = ApiCaller.GetEmployeeByPIN(model.PIN);
            }
            else if (model.MobileNo != "")
            {
                dataObjects = ApiCaller.GetEmployeeByMobile(model.MobileNo);
            }


            if (dataObjects != null)
            {
                staff = dataObjects.FirstOrDefault();
            }

            if (staff == null)
            {
                model.Message = "Staff not found by PIN or mobile";

                var source = standingDataService.GetSource();
                model.ProgramList = source.ToSelectList(null, "Id", "Name");

                var div = standingDataService.GetDivisions().Where(r => r.IsActive);
                var fd = div.FirstOrDefault().Id;

                model.DivisionList = div.ToSelectList(fd, "Id", "Name");

                var dis = standingDataService.GetDistricts(fd).Where(r => r.IsActive);
                model.DistrictList = dis.ToSelectList(null, "Id", "Name");

                var gen = standingDataService.GetGender().Where(r => r.IsActive);
                model.GenderList = gen.ToSelectList(null, "Id", "Name");

                model.IsFound = false;
            }
            else
            {
                model.Message = "";
                model.PIN = staff.pin;
                model.IsFound = true;

                model.ExistingData = service.GetPersonal(staff.pin).ToList();

                var prjId = GetProgram(staff.projectname);

                var source = standingDataService.GetSource().Where(r => r.IsActive);
                model.ProgramList = source.ToSelectList(prjId, "Id", "Name");
                model.ProgramId = prjId;

                var disOfStaff = standingDataService.GetDistricts(staff.districtname);
                var genOfStaff = standingDataService.GetGender(staff.sex);

                var div = standingDataService.GetDivisions().Where(r => r.IsActive);
                var gen = standingDataService.GetGender().Where(r => r.IsActive);



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

                var eff = standingDataService.GetByType(StandingDataTypes.Doctor_EffectedPerson).Where(r => r.IsActive);
                model.EffectedPersonList = eff.ToSelectList(null, "Id", "Name");

                model.Name = staff.StaffName;
                model.MobileNo = staff.MobileNo;

                if (staff.dateofbirth != null)
                {
                    model.Dob = staff.dateofbirth;
                    model.Age = (int)(((DateTime.Now - staff.dateofbirth.Value).TotalDays) / 365);
                }

                model.AreaOffice = staff.branchname;

            }

            model.EffectedPersonList = standingDataService.GetByType(StandingDataTypes.Doctor_EffectedPerson)
                    .Where(r => r.IsActive)
                    .ToSelectList(null, "Id", "Name");


            return PartialView("HrCreateDetail", model);
        }

        public ActionResult Save(DoctorsPoleModel model)
        {

            DoctorsPole entity = new DoctorsPole();

            ModelCopier.CopyModel(model, entity);

            entity.EntryById = SessionHelper.UserId;
            entity.EntryTime = DateTime.Now;

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
                return PartialView("Followup", new { id = entity.Id });
            }


            return PartialView();
        }

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
                       select new object[] {c.StandingData1.Name
                       ,c.StandingData2.Name
                       ,c.StandingData3.Name
                       ,c.AreaOffice
                       , String.Format("{0:dd MMM, yyyy}", c.EntryTime)
                       ,String.Format("{0:dd MMM, yyyy}", c.FirstDoctorCallTime)
                       ,c.Name
                       ,c.Age
                       , c.StandingData.Name
                       , c.StandingData4.Name
                       , c.StandingData5==null?"":c.StandingData5.Name
                       ,c.UserProfile.UserName
                ,new GridButtonModel[]
                    {
                         new GridButtonModel{U=Url.Action("Edit",new {Id=c.Id}), T="Edit", D = GridButtonDialog.dialig1.ToString(), H="Edit", M="class=\"btn btn-mini btn-warning\""
                         , V = (visible && c.FirstDoctorCallTime==null)}

                    }
            }).ToArray();


            JQueryDataTable js = new JQueryDataTable();
            js.sEcho = ec;
            js.iTotalDisplayRecords = count.ToString();
            js.iTotalRecords = js.iTotalDisplayRecords;
            js.aaData = obj;

            return Json(js, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Followup(int id)
        {
            DoctorsPoleVisitModel model = new DoctorsPoleVisitModel();

            var stst = standingDataService.GetByType(StandingDataTypes.Doctor_Status).Where(r => r.IsActive);
            model.StatusList = stst.ToSelectList(null, "Id", "Name");

            var co = standingDataService.GetByType(StandingDataTypes.Doctor_CoMorbidity).Where(r => r.IsActive);
            model.CoMobiList = co.ToSelectList(null, "Id", "Name");

            var ts = standingDataService.GetByType(StandingDataTypes.Doctor_TestResult).Where(r => r.IsActive);
            model.TestResultList = ts.ToSelectList(null, "Id", "Name");

            var ad = standingDataService.GetByType(StandingDataTypes.Doctor_AdmissionType).Where(r => r.IsActive);
            model.AdminTypeList = ad.ToSelectList(null, "Id", "Name");

            model.DoctorsPole = service.Get(id);
            model.DoctorsPoleVisits = service.GetVisitsByParent(id).ToList();

            var last = model.DoctorsPoleVisits
                .OrderByDescending(c => c.EntryTime)
                .Take(1).Skip(0)
                .FirstOrDefault();

            if(last !=null)
            {
                ModelCopier.CopyModel(last, model);
            }

            model.DoctorPoleId = id;

            return PartialView(model);
        }

        public ActionResult FollowupSave(DoctorsPoleVisitModel model)
        {
            var entity = service.Get(model.DoctorPoleId);
            int oldEb = entity.EntryById;
            DateTime oldEt = entity.EntryTime;
            DateTime oldFst = entity.FirstDoctorCallTime ?? DateTime.Now;

            entity.NextFollowupDate = DateTime.Now.AddDays(model.FollowupAfterDays);

            ModelCopier.CopyModel(model, entity);

            entity.LastUpdateById = SessionHelper.UserId;
            entity.LastUpdateTime = DateTime.Now;
            entity.EntryById = oldEb;
            entity.EntryTime = oldEt;
            entity.FirstDoctorCallTime = oldFst;

            service.Update(entity);

            DoctorsPoleVisit entityVisit = new DoctorsPoleVisit();

            ModelCopier.CopyModel(model, entityVisit);

            entityVisit.EntryById = SessionHelper.UserId;
            entityVisit.EntryTime = DateTime.Now;

            service.AddVisit(entityVisit);

            unitOfWork.Commit();

            return PartialView();
        }

        public ActionResult FollowupIndex()
        {
            SearchModel up = new SearchModel();

            var source = standingDataService.GetSource().Where(r => r.IsActive);
            up.ContentTypes1 = source.ToSelectList(null, "Id", "Name");

            var div = standingDataService.GetDivisions().Where(r => r.IsActive);
            up.ContentTypes2 = div.ToSelectList(null, "Id", "Name");

            var dis = standingDataService.GetDistricts(0).Where(r => r.IsActive);
            up.ContentTypes3 = dis.ToSelectList(null, "Id", "Name");

            up.FromDate = DateTime.Now.AddDays(-7);
            up.ToDate = DateTime.Now;

            return View(up);
        }

        public JsonResult FollowupDataGrid()
        {
            bool visible = UserRole.Check("DoctorPool_Doc", SessionHelper.Role);

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

            
            int count = 0;
            IEnumerable<DoctorsPole> dataList = service.Get(srcId, divId, disId, skip, take, out count);


            var obj = (from c in dataList
                       select new object[] {c.StandingData1.Name
                       ,c.StandingData2.Name
                       ,c.StandingData3.Name
                       ,c.AreaOffice
                       , String.Format("{0:dd MMM, yyyy}", c.EntryTime)
                       ,String.Format("{0:dd MMM, yyyy}", c.FirstDoctorCallTime)
                       ,c.Name
                       ,c.Age
                       , c.StandingData.Name
                       , c.StandingData4.Name
                       , c.StandingData5==null?"":c.StandingData5.Name
                       ,c.UserProfile.UserName
                ,new GridButtonModel[]
                    {
                         new GridButtonModel{U=Url.Action("Followup",new {Id=c.Id}), T="Followup", D = GridButtonDialog.dialig1.ToString(), H="Edit", M="class=\"btn btn-mini btn-warning\""
                         , V = (visible)}

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

            var source = standingDataService.GetSource().Where(r => r.IsActive);
            up.ContentTypes1 = source.ToSelectList(null, "Id", "Name");

            var div = standingDataService.GetDivisions().Where(r => r.IsActive);
            up.ContentTypes2 = div.ToSelectList(null, "Id", "Name");

            var dis = standingDataService.GetDistricts(0).Where(r => r.IsActive);
            up.ContentTypes3 = dis.ToSelectList(null, "Id", "Name");

            up.FromDate = DateTime.Now.AddDays(-7);
            up.ToDate = DateTime.Now;

            return View(up);
        }

        public JsonResult ReportDataGrid()
        {
            bool visible = UserRole.Check("DoctorPool_Doc", SessionHelper.Role);

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
            IEnumerable<DoctorsPole> dataList = service.Get(srcId, divId, disId, FromDate, ToDate, skip, take,  out count);


            var obj = (from c in dataList
                       select new object[] {c.StandingData1.Name
                       ,c.StandingData2.Name
                       ,c.StandingData3.Name
                       ,c.AreaOffice
                       , String.Format("{0:dd MMM, yyyy}", c.EntryTime)
                       ,String.Format("{0:dd MMM, yyyy}", c.FirstDoctorCallTime)
                       ,c.Name
                       ,c.Age
                       , c.StandingData.Name
                       , c.StandingData4.Name
                       , c.StandingData5==null?"":c.StandingData5.Name
                       ,c.UserProfile.UserName
                ,new GridButtonModel[]
                    {
                         new GridButtonModel{U=Url.Action("Followup",new {Id=c.Id}), T="Followup", D = GridButtonDialog.dialig1.ToString(), H="Edit", M="class=\"btn btn-mini btn-warning\""
                         , V = (visible)}

                    }
            }).ToArray();


            JQueryDataTable js = new JQueryDataTable();
            js.sEcho = ec;
            js.iTotalDisplayRecords = count.ToString();
            js.iTotalRecords = js.iTotalDisplayRecords;
            js.aaData = obj;

            return Json(js, JsonRequestBehavior.AllowGet);
        }

        int GetProgram(string program)
        {
            var source = standingDataService.GetSource(program);

            if (source == null)
            {
                standingDataService.Add(new StandingData { Name = program, Type = "SRC", IsActive = true });
                unitOfWork.Commit();
            }

            return standingDataService.GetSource(program).Id;
        }

    }
}
