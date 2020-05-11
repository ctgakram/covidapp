using AppProj.Data.Infrastructure;
using AppProj.Domain;
using AppProj.Service.Services;
using AppProj.Web.Models;
using AppProj.Web.ViewModels;
using Microsoft.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppProj.Web.Controllers
{
    [Authorize]    
    public class StandingController : Controller
    {
        readonly IStandingDataService standingDataService;
        readonly IDistrictQuestionService disServce;
        readonly IUnitOfWork unitOfWork;

        public StandingController(IUnitOfWork unitOfWork, IStandingDataService standingDataService, IDistrictQuestionService disServce)
        {
            this.standingDataService = standingDataService;
            this.disServce = disServce;
            this.unitOfWork = unitOfWork;

        }

        [CustomAuthorize(Roles: new string[] { "ALL_PROGRAM" })]
        public ActionResult Index(string id)
        {
            ViewBag.Tp = id;

            SessionHelper.Temp = id;

            if (id == StandingDataTypes.Programs)
            {
                ViewBag.Title = "Programs";
            }
            else if (id == StandingDataTypes.HowProgramsAffected)
            {
                ViewBag.Title = "How the program has been impacted/affected? ";
            }
            else if (id == StandingDataTypes.CommunicationMaterial)
            {
                ViewBag.Title = "What are the communication materials?";
            }
            else if (id == StandingDataTypes.CommunicationChannel)
            {
                ViewBag.Title = "What are the communication channels?";
            }
            else if (id == StandingDataTypes.MaterialsOrItems)
            {
                ViewBag.Title = "Materials";
            }
            else if (id == StandingDataTypes.Activities)
            {
                ViewBag.Title = "What are the prepardness initiatives taken by the program?";
            }
            else if (id == StandingDataTypes.RestrictionsOnProgramImplementation)
            {
                ViewBag.Title = "Is there any national/ local restriction on program implementation?";
            }
            else if (id == StandingDataTypes.TreeMessage)
            {
                ViewBag.Title = "COVID Circular";
            }
            else
            {
                return View("Error");
            }

            return View();
        }

        [CustomAuthorize(Roles: new string[] { "ALL_PROGRAM" })]
        public ActionResult Create()
        {
            StandingDataModel up = new StandingDataModel();
            up.IsActive = true;

            up.Type = ""+SessionHelper.Temp;
            
            return PartialView(up);
        }

        [CustomAuthorize(Roles: new string[] { "ALL_PROGRAM" })]
        public ActionResult Edit(int Id)
        {
            var entity = standingDataService.GetDataById(Id);

            StandingDataModel model = new StandingDataModel();
            ModelCopier.CopyModel(entity, model);

            return PartialView("Create", model);
        }

        [CustomAuthorize(Roles: new string[] { "ALL_PROGRAM" })]
        public ActionResult Save(StandingDataModel model)
        {
            StandingData entity = new StandingData();

            ModelCopier.CopyModel(model, entity);


            if (model.Id == 0)
            {
                standingDataService.Add(entity);
            }
            else
            {
                standingDataService.Update(entity);
            }

            unitOfWork.Commit();

            return PartialView();
        }

        [CustomAuthorize(Roles: new string[] { "ALL_PROGRAM" })]
        public JsonResult DataGrid()
        {
            int ec = int.Parse(Request.QueryString["sEcho"]);
            int skp = int.Parse(Request.QueryString["iDisplayLength"]);
            int tke = int.Parse(Request.QueryString["iDisplayStart"]);

            IEnumerable<StandingData> projList = null;

            string id = ""+SessionHelper.Temp;
            projList = standingDataService.GetByType(id)
                .OrderBy(c=>c.Name);
            
            
            var obj = (from c in projList
                       select new object[] {
                           c.Name
                       //,c.StringValue
                       , c.IsActive?"Active":"Inactive"
                ,new GridButtonModel[]
                    {
                        new GridButtonModel{U=Url.Action("Edit",new {id=c.Id}), T="Edit", D = GridButtonDialog.dialig1.ToString(), H="Edit"},
                        new GridButtonModel{U=Url.Action("Relation",new {id=c.Id,tp=StandingDataPCRelationType.Material_Response_Activity}), T="Response Activity", D = GridButtonDialog.dialig1.ToString(), H="Activities", V=(id==StandingDataTypes.MaterialsOrItems)},
                        new GridButtonModel{U=Url.Action("Relation",new {id=c.Id,tp=StandingDataPCRelationType.Material_Prepareness_Activity}), T="Prepareness Activity", D = GridButtonDialog.dialig1.ToString(), H="Activities", V=(id==StandingDataTypes.MaterialsOrItems)}
                    }
            }).Skip(tke).Take(skp).ToArray();

            JQueryDataTable js = new JQueryDataTable();
            js.sEcho = ec;
            js.iTotalDisplayRecords = projList.Count().ToString();
            js.iTotalRecords = js.iTotalDisplayRecords;
            js.aaData = obj;

            return Json(js, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(Roles: new string[] { "ALL_PROGRAM" })]
        public ActionResult Relation(int Id,string tp)
        {
            try
            {
                return PartialView("Relation", DataForRelation(Id,tp));
            }
            catch
            {
                return RedirectToAction("Error", "Main");
            }
        }

        [CustomAuthorize(Roles: new string[] { "ALL_PROGRAM" })]
        public ActionResult RelationAdd(int ChildId, int ParentId, string tp)
        {
            try
            {
                StandingDataPcRelation entity = new StandingDataPcRelation();
                entity.ParentId = ParentId;
                entity.ChildId = ChildId;
                entity.Type = tp;
                standingDataService.AddParentChild(entity);
                unitOfWork.Commit();
                
                return PartialView("Relation", DataForRelation(ParentId, tp));
            }
            catch
            {
                return RedirectToAction("Error", "Main");
            }
        }

        [CustomAuthorize(Roles: new string[] { "ALL_PROGRAM" })]
        public ActionResult RelationDelete(int Id, string tp)
        {

            try
            {
                var entity = standingDataService.GetParentChildById(Id);
                standingDataService.DeleteParentChild(entity);
                unitOfWork.Commit();

                return PartialView("Relation", DataForRelation(entity.ParentId, tp));
            }
            catch
            {
                return RedirectToAction("Error", "Main");
            }
        }


        StandingDataPcRelationModel DataForRelation(int Id, string type)
        {
            List<StandingData> restClilds = new List<StandingData>();

            if (type == StandingDataPCRelationType.Material_Response_Activity)
            {
                ViewBag.Title = "Response Activity with Material";
                restClilds = standingDataService
                    .GetByType(StandingDataTypes.Activities)
                    .ToList();
            }
            else if (type == StandingDataPCRelationType.Material_Prepareness_Activity)
            {
                ViewBag.Title = "Prepareness Activity with Material";
                restClilds = standingDataService
                    .GetByType(StandingDataTypes.Activities)
                    .ToList();
            }

            var pcData = standingDataService.GetParentChild(Id, type);
            IEnumerable<int> ids = pcData.Select(c => c.ChildId);

            restClilds.RemoveAll(c => ids.Contains(c.Id));

            StandingDataPcRelationModel up = new StandingDataPcRelationModel();

            up.Type = type;
            up.ParentId = Id;

            up.Allowed = pcData.Select(c => new StandingDataPcRelationModelDetail
            {
                Id = c.Id,
                ChildId = c.ChildId,
                ChildName = c.StandingData.Name
            });

            up.NotAllowed = restClilds.Select(c => new StandingDataPcRelationModelDetail
            {
                Id = 0,
                ChildId = c.Id,
                ChildName = c.Name
            });

            return up;
        }

        [CustomAuthorize(Roles: new string[] { "BDC_ADMIN" })]
        public ActionResult DistQuestion()
        {
            ViewBag.Title = "District Wise Question";
            return View();
        }

        [CustomAuthorize(Roles: new string[] { "BDC_ADMIN" })]
        public ActionResult CreateQuestion()
        {
            StandingDataModel up = new StandingDataModel();
            up.IsActive = true;
            up.Type = "QES";

            return PartialView(up);
        }

        [CustomAuthorize(Roles: new string[] { "BDC_ADMIN" })]
        public ActionResult EditQuestion(int Id)
        {
            var entity = standingDataService.GetDataById(Id);

            StandingDataModel model = new StandingDataModel();
            ModelCopier.CopyModel(entity, model);

            return PartialView("CreateQuestion", model);
        }

        [CustomAuthorize(Roles: new string[] { "BDC_ADMIN" })]
        public ActionResult SaveQuestion(StandingDataModel model)
        {
            StandingData entity = new StandingData();

            ModelCopier.CopyModel(model, entity);
            
            if (model.Id == 0)
            {
                standingDataService.Add(entity);
            }
            else
            {
                standingDataService.Update(entity);
            }

            unitOfWork.Commit();

            return PartialView();
        }

        [CustomAuthorize(Roles: new string[] { "BDC_ADMIN" })]
        public JsonResult DataGridQuestion()
        {
            int ec = int.Parse(Request.QueryString["sEcho"]);
            int skp = int.Parse(Request.QueryString["iDisplayLength"]);
            int tke = int.Parse(Request.QueryString["iDisplayStart"]);

            IEnumerable<StandingData> projList = null;

            projList = standingDataService.GetQuestions();

            var obj = (from c in projList
                       select new object[] { c.Name, c.IsActive?"Active":"Inactive"
                ,new GridButtonModel[]
                    {
                        new GridButtonModel{U=Url.Action("EditQuestion",new {id=c.Id}), T="Edit", D = GridButtonDialog.dialig1.ToString(), H="Edit"}
                       ,new GridButtonModel{U=Url.Action("Districts",new {id=c.Id}), T="Districts", D = GridButtonDialog.dialig1.ToString(),H="Allowed Districts"}
                    }
            }).Skip(tke).Take(skp).ToArray();

            JQueryDataTable js = new JQueryDataTable();
            js.sEcho = ec;
            js.iTotalDisplayRecords = projList.Count().ToString();
            js.iTotalRecords = js.iTotalDisplayRecords;
            js.aaData = obj;

            return Json(js, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(Roles: new string[] { "BDC_ADMIN" })]
        public ActionResult Districts(int Id)
        {
            try
            {
                QuestionDistrictModel up = new QuestionDistrictModel();
                up.QuestionId = Id;
                up.DistrictByQuestion = disServce.GetByQuestion(Id).ToList();

                IEnumerable<int> ids = up.DistrictByQuestion.Select(c => c.DistrictId);

                var allDis = standingDataService.GetDistricts().OrderBy(c => c.Name).ToList(); ;
                allDis.RemoveAll(c => ids.Contains(c.Id));

                up.RestDistricts = allDis;

                return PartialView("Districts", up);
            }
            catch
            {
                return RedirectToAction("Error", "Main");
            }
        }

        [CustomAuthorize(Roles: new string[] { "BDC_ADMIN" })]
        public ActionResult DistrictsAdd(int Id, int questionId)
        {
            try
            {
                DistrictQuestion entity = new DistrictQuestion();
                entity.QuestionId = questionId;
                entity.DistrictId = Id;

                disServce.Add(entity);
                unitOfWork.Commit();

                QuestionDistrictModel up = new QuestionDistrictModel();
                up.QuestionId = questionId;
                up.DistrictByQuestion = disServce.GetByQuestion(questionId).ToList();

                IEnumerable<int> ids = up.DistrictByQuestion.Select(c => c.DistrictId);

                var allDis = standingDataService.GetDistricts().OrderBy(c => c.Name).ToList(); ;
                allDis.RemoveAll(c => ids.Contains(c.Id));

                up.RestDistricts = allDis;

                return PartialView("Districts", up);
            }
            catch
            {
                return RedirectToAction("Error", "Main");
            }
        }

        [CustomAuthorize(Roles: new string[] { "BDC_ADMIN" })]
        public ActionResult DistrictsDelete(int Id)
        {

            try
            {
                var entity = disServce.Get(Id);
                disServce.Delete(entity);
                unitOfWork.Commit();


                QuestionDistrictModel up = new QuestionDistrictModel();
                up.QuestionId = entity.QuestionId;
                up.DistrictByQuestion = disServce.GetByQuestion(entity.QuestionId).ToList();

                IEnumerable<int> ids = up.DistrictByQuestion.Select(c => c.DistrictId);

                var allDis = standingDataService.GetDistricts().OrderBy(c => c.Name).ToList(); ;
                allDis.RemoveAll(c => ids.Contains(c.Id));

                up.RestDistricts = allDis;

                return PartialView("Districts", up);
            }
            catch
            {
                return RedirectToAction("Error", "Main");
            }
        }

    }
}
