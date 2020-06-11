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
    [CustomAuthorize(Roles: new string[] { "HR_Staff" })]
    public class WorkstationLeaveQuarantineController : Controller
    {
        readonly IStandingDataService standingDataService;
        readonly IWorkstationLeaveQuarantineService workstationLeaveQuarantineService;
        readonly IUnitOfWork unitOfWork;

        public WorkstationLeaveQuarantineController(IUnitOfWork unitOfWork, IWorkstationLeaveQuarantineService workstationLeaveQuarantineService, IStandingDataService standingDataService)
        {
            this.standingDataService = standingDataService;
            this.workstationLeaveQuarantineService = workstationLeaveQuarantineService;
            this.unitOfWork = unitOfWork;

        }

        public ActionResult Index()
        {
            SearchModel up = new SearchModel();

            var div = standingDataService.GetDivisions().Where(r => r.IsActive);
            up.ContentTypes1 = div.ToSelectList(null, "Id", "Name");

            var dis = standingDataService.GetDistricts(0).Where(r => r.IsActive);
            up.ContentTypes2 = dis.ToSelectList(null, "Id", "Name");

            up.FromDate = DateTime.Now;
            up.ToDate = DateTime.Now;

            return View(up);
        }

        public ActionResult Create()
        {
            WorkstationLeaveQuarantineShortModel model = new WorkstationLeaveQuarantineShortModel();

            return PartialView(model);
        }

        public ActionResult CreateDetail(WorkstationLeaveQuarantineShortModel mod)
        {
            try
            {

                WorkstationLeaveQuarantineModel model = new WorkstationLeaveQuarantineModel();
                string srcTxt = "";

                model.PIN = ("" + mod.PIN_).Trim();

                int count = 0;
                var entity = workstationLeaveQuarantineService.Get(model.PIN, 0, 1000000000, out count).ToList();

                if (entity.Count == 0)
                {
                    IEnumerable<StaffProfile> dataObjects = null;
                    StaffProfile staff = null;

                    if (!String.IsNullOrEmpty(model.PIN))
                    {
                        dataObjects = ApiCaller.GetEmployeeByPIN(model.PIN);
                        srcTxt = model.PIN;
                    }

                    if (dataObjects != null)
                    {
                        staff = dataObjects.FirstOrDefault();
                    }

                    GenerateSupervisorList(model);

                    if (staff == null)
                    {
                        model.Message = "Staff not found by PIN";

                        var prj = standingDataService.GetProject();
                        model.ProgramList = prj.ToSelectList(null, "Id", "Name");

                        var div = standingDataService.GetDivisions().Where(r => r.IsActive);
                        var fd = div.FirstOrDefault().Id;

                        model.DivisionList = div.ToSelectList(fd, "Id", "Name");

                        var dis = standingDataService.GetDistricts(fd).Where(r => r.IsActive);
                        model.DistrictList = dis.ToSelectList(null, "Id", "Name");


                        var upn = dis.FirstOrDefault().Id;
                        var upz = standingDataService.GetUpazilla(upn).Where(r => r.IsActive);
                        model.UpazillaList = upz.ToSelectList(null, "Id", "Name");


                        var gen = standingDataService.GetGender().Where(r => r.IsActive);
                        model.GenderList = gen.ToSelectList(null, "Id", "Name");

                        model.ExistingData = workstationLeaveQuarantineService.GetPersonal(srcTxt).ToList();

                        model.IsFound = false;

                        mod.Message = "Staff not found by PIN";

                        return PartialView("Create", mod);
                    }
                    else
                    {
                        model.Message = "";
                        model.PIN = staff.pin;
                        model.IsFound = true;
                        model.Designation = staff.Designationname;

                        model.ExistingData = workstationLeaveQuarantineService.GetPersonal(staff.pin).ToList();

                        var prjId = GetProject(staff.projectname);

                        var prj = standingDataService.GetProject().Where(r => r.IsActive);
                        model.ProgramList = prj.ToSelectList(prjId, "Id", "Name");
                        model.SourceId = prjId;

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

                        var upazilaOfStaff = standingDataService.GetUpazillas(staff.UpazilaName);

                        if (upazilaOfStaff != null)
                        {
                            var upz = standingDataService.GetUpazilla(upazilaOfStaff.ParentId.Value).Where(r => r.IsActive);

                            model.UpazillaList = upz.ToSelectList(upazilaOfStaff.Id, "Id", "Name");
                            model.UpazillaId = upazilaOfStaff.Id;
                        }
                        else
                        {
                            var upzila = standingDataService.GetUpazilla(model.DistrictId ?? 0).Where(r => r.IsActive);

                            model.UpazillaList = upzila.ToSelectList(model.DistrictId, "Id", "Name");
                        }

                        model.Name = staff.StaffName;
                        model.StaffName = staff.StaffName;
                        model.Branch = staff.branchname;
                        model.Grade = staff.Grade;
                        model.HomeDistrict = staff.PermanentAddressDistrictName;
                        model.DateOfJoiningBrac = staff.JoiningDate;
                        model.DateOfJoiningCurrentBranch = staff.TransferDate;

                        
                    }

                    return PartialView(model);
                }
                else
                {
                    model.Message = "Information already taken from this PIN";

                    return this.RedirectToAction("Edit", "WorkstationLeaveQuarantine", new { entity.FirstOrDefault().Id, model.Message });
                }
            }
            catch
            {
                return RedirectToAction("Error", "Main");
            }
        }

        private static void GenerateSupervisorList(WorkstationLeaveQuarantineModel model)
        {
            SelectListItem item1 = new SelectListItem();
            item1.Text = "Not Permitted";
            item1.Value = "Not Permitted";

            SelectListItem item2 = new SelectListItem();
            item2.Text = "Yes";
            item2.Value = "Yes";

            SelectListItem item3 = new SelectListItem();
            item3.Text = "No";
            item3.Value = "No";

            List<SelectListItem> selectListItems = new List<SelectListItem>();
            selectListItems.Add(item1);
            selectListItems.Add(item2);
            selectListItems.Add(item3);

            model.SupervisorPermittedList = selectListItems;
        }

        public ActionResult Save(WorkstationLeaveQuarantineModel model)
        {
            //return PartialView("CreateDetail", model);

            WorkstationLeaveQuarantine entity = new WorkstationLeaveQuarantine();
            ModelCopier.CopyModel(model, entity);

            entity.EntryById = SessionHelper.UserId;
            entity.EntryTime = DateTime.Now;

            entity.Name = Request.Form["StaffName"].ToString();

            if (Request.Form["IsWorkStationLeft"].ToString() == "1")
            {
                entity.IsWorkStationLeft = true;
            }
            else
            {
                entity.IsWorkStationLeft = false;

                entity.WorkStationLeaveDate = null;
                entity.WorkStationReturnDate = null;
            }


            if (Request.Form["IsStayInQuarantine"].ToString() == "1")
            {
                entity.IsStayInQuarantine = true;
            }
            else
            {
                entity.IsStayInQuarantine = false;

                entity.QuarantineStartDate = null;
                entity.QuarantineEndDate = null;
            }

            if (Request.Form["IsCoronaSymptomitic"].ToString() == "1")
            {
                entity.IsCoronaSymptomitic = true;
            }
            else
            {
                entity.IsCoronaSymptomitic = false;
            }

            if (Request.Form["IsQuarantineExtended"].ToString() == "1")
            {
                entity.IsQuarantineExtended = true;
            }
            else
            {
                entity.IsQuarantineExtended = false;
                entity.ExtendedQuarantineStartDate = null;
                entity.ExtendedQuarantineEndDate = null;
            }

            if (Request.Form["IsInformationFromAnotherPerson"].ToString() == "1")
            {
                entity.IsInformationFromAnotherPerson = true;
            }
            else
            {
                entity.IsInformationFromAnotherPerson = false;

                entity.InfoCollectedName = null;
                entity.InfoCollectedPIN = null;
                entity.InfoCollectedDesignation = null;
                entity.InfoCollectedDate = null;
            }

            if (entity.DistrictId != null)
            {
                var disOfStaff = standingDataService.GetDataById(entity.DistrictId ?? 0);
                entity.DivisionId = disOfStaff.ParentId.Value;
            }

            if (model.Id == 0)
            {
                workstationLeaveQuarantineService.Add(entity);

            }
            else
            {
                workstationLeaveQuarantineService.Update(entity);
            }



            unitOfWork.Commit();

            return PartialView();

        }

        public ActionResult Edit(int Id, string message)
        {
            try
            {
                var entity = workstationLeaveQuarantineService.Get(Id);

                WorkstationLeaveQuarantineModel model = new WorkstationLeaveQuarantineModel();

                ModelCopier.CopyModel(entity, model);

                model.Message = message;

                IEnumerable<StaffProfile> dataObjects = null;
                StaffProfile staff = null;

                if (!String.IsNullOrEmpty(model.PIN))
                {
                    dataObjects = ApiCaller.GetEmployeeByPIN(model.PIN);
                }

                if (dataObjects != null)
                {
                    staff = dataObjects.FirstOrDefault();
                }

                if (staff == null)
                {
                    model.Message = "Staff not found by PIN";

                    var prj = standingDataService.GetProject();
                    model.ProgramList = prj.ToSelectList(null, "Id", "Name");

                    var div = standingDataService.GetDivisions().Where(r => r.IsActive);
                    var fd = div.FirstOrDefault().Id;

                    model.DivisionList = div.ToSelectList(fd, "Id", "Name");

                    var dis = standingDataService.GetDistricts(fd).Where(r => r.IsActive);
                    model.DistrictList = dis.ToSelectList(null, "Id", "Name");

                    var upn = dis.FirstOrDefault().Id;
                    var upz = standingDataService.GetUpazilla(upn).Where(r => r.IsActive);
                    model.UpazillaList = upz.ToSelectList(null, "Id", "Name");

                    var gen = standingDataService.GetGender().Where(r => r.IsActive);
                    model.GenderList = gen.ToSelectList(null, "Id", "Name");

                    model.ExistingData = workstationLeaveQuarantineService.GetPersonal(model.PIN).ToList();

                    model.IsFound = false;
                }
                else
                {
                    //model.Message = "";
                    model.PIN = staff.pin;
                    model.IsFound = true;
                    model.Designation = staff.Designationname;

                    model.ExistingData = workstationLeaveQuarantineService.GetPersonal(staff.pin).ToList();

                    var prjId = GetProject(staff.projectname);

                    var prj = standingDataService.GetProject().Where(r => r.IsActive);
                    model.ProgramList = prj.ToSelectList(prjId, "Id", "Name");
                    model.SourceId = prjId;

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

                    var upazilaOfStaff = standingDataService.GetUpazillas(staff.UpazilaName);

                    if (upazilaOfStaff != null)
                    {
                        var upz = standingDataService.GetUpazilla(upazilaOfStaff.ParentId.Value).Where(r => r.IsActive);

                        model.UpazillaList = upz.ToSelectList(upazilaOfStaff.Id, "Id", "Name");
                        model.UpazillaId = upazilaOfStaff.Id;
                    }
                    else
                    {
                        var upzila = standingDataService.GetUpazilla(model.DistrictId ?? 0).Where(r => r.IsActive);

                        model.UpazillaList = upzila.ToSelectList(model.DistrictId, "Id", "Name");
                    }

                    GenerateSupervisorList(model);
                    model.SupervisorPermittedList.ToSelectList(model.SupervisorPermitted, "Id", "Name");

                    model.Name = staff.StaffName;
                    model.StaffName = staff.StaffName;
                    model.Branch = staff.branchname;
                    model.Grade = staff.Grade;
                    model.HomeDistrict = staff.PermanentAddressDistrictName;
                    model.DateOfJoiningBrac = staff.JoiningDate;
                    model.DateOfJoiningCurrentBranch = staff.TransferDate;
                }

                return PartialView("CreateDetail", model);
            }
            catch
            {
                return RedirectToAction("Error", "Main");
            }
        }

        public JsonResult DataGrid()
        {
            int count = 0;
            bool visible = true;//UserRole.Check("SUMMERY", SessionHelper.Role);
            int ec = int.Parse(Request.QueryString["sEcho"]);
            int take = int.Parse(Request.QueryString["iDisplayLength"]);
            int skip = int.Parse(Request.QueryString["iDisplayStart"]);


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

            string PIN = Request.QueryString["PIN"].ToString();

            DateTime FromDate = Convert.ToDateTime(Request.QueryString["FromDate"]);
            DateTime ToDate = Convert.ToDateTime(Request.QueryString["ToDate"]);

            List<WorkstationLeaveQuarantine> dataList = new List<WorkstationLeaveQuarantine>();

            if (!string.IsNullOrEmpty(PIN))
            {
                dataList = workstationLeaveQuarantineService.Get(PIN, skip, take, out count).ToList();
            }
            else
            {
                dataList = workstationLeaveQuarantineService.Get(null, divId, disId, FromDate, ToDate, skip, take, null, out count).ToList();
            }



            var obj = (from c in dataList
                       select new object[] {c.PIN
                       ,c.Name
                       ,c.Designation
                       ,c.Grade
                       ,standingDataService.GetGender().Where(r => r.Id ==c.GenderId).Select(m=>m.Name)
                       ,c.HomeDistrict
                       ,standingDataService.GetAllDataById(c.SourceId??0).Select(m=>m.Name)
                       ,String.Format("{0:dd MMM, yyyy}", c.DateOfJoiningBrac)
                       ,String.Format("{0:dd MMM, yyyy}", c.DateOfJoiningCurrentBranch)
                       ,c.Branch
                       ,c.Area
                       ,c.Region
                       ,standingDataService.GetAllDataById(c.UpazillaId??0).Select(m=>m.Name)
                       ,standingDataService.GetAllDataById(c.DistrictId??0).Select(m=>m.Name)
                       ,c.IsWorkStationLeft==true?"<span style=\"color:#FF0000\">Yes</span>":"No"
                       ,String.Format("{0:dd MMM, yyyy}", c.WorkStationLeaveDate)
                       ,String.Format("{0:dd MMM, yyyy}", c.WorkStationReturnDate)
                       ,c.SupervisorPermitted
                       ,c.IsStayInQuarantine==true?"<span style=\"color:#FF0000\">Yes</span>":"No"
                       ,String.Format("{0:dd MMM, yyyy}", c.QuarantineStartDate)
                       ,String.Format("{0:dd MMM, yyyy}", c.QuarantineEndDate)
                       ,c.IsCoronaSymptomitic==true?"<span style=\"color:#FF0000\">Yes</span>":"No"
                       ,c.IsQuarantineExtended==true?"<span style=\"color:#FF0000\">Yes</span>":"No"
                       ,String.Format("{0:dd MMM, yyyy}", c.ExtendedQuarantineStartDate)
                       ,String.Format("{0:dd MMM, yyyy}", c.ExtendedQuarantineEndDate)
                       ,c.IsInformationFromAnotherPerson==true?"<span style=\"color:#FF0000\">Yes</span>":"No"
                       ,c.InfoCollectedName
                       ,c.InfoCollectedPIN
                       ,c.InfoCollectedDesignation
                       ,String.Format("{0:dd MMM, yyyy}", c.InfoCollectedDate)
                       ,c.Comment
                       //,c.UserProfile.UserName
                ,new GridButtonModel[]
                    {
                         new GridButtonModel{U=Url.Action("Edit",new {Id=c.Id}), T="Edit", D = GridButtonDialog.dialig1.ToString(), H="Edit", M="class=\"brac-link\"", V = (visible && c.Id!=0) }// btn btn-mini btn-warning
                    }
            }).ToArray();

            JQueryDataTable js = new JQueryDataTable();
            js.sEcho = ec;
            js.iTotalDisplayRecords = count.ToString();
            js.iTotalRecords = js.iTotalDisplayRecords;
            js.aaData = obj;

            return Json(js, JsonRequestBehavior.AllowGet);
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
    }
}
