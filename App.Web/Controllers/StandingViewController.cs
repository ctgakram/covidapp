using AppProj.Domain;
using AppProj.Service.Services;
using AppProj.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppProj.Web.Controllers
{
    [Authorize]
    public class StandingViewController : Controller
    {
        readonly IStandingDataService standingDataService;
        readonly IDistrictQuestionService disServce;
        
        public StandingViewController( IStandingDataService standingDataService, IDistrictQuestionService disServce)
        {
            this.standingDataService = standingDataService;
            this.disServce = disServce;

        }

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
            else
            {
                return View("Error");
            }

            return View();
        }

        public JsonResult DataGrid()
        {
            int ec = int.Parse(Request.QueryString["sEcho"]);
            int skp = int.Parse(Request.QueryString["iDisplayLength"]);
            int tke = int.Parse(Request.QueryString["iDisplayStart"]);

            IEnumerable<StandingData> projList = null;

            string id = "" + SessionHelper.Temp;
            projList = standingDataService.GetByType(id)
                .OrderBy(c => c.Name);


            var obj = (from c in projList
                       select new object[] {
                           c.Name
                       //,c.StringValue
                       , c.IsActive?"Active":"Inactive"
                //,new GridButtonModel[]
                //    {
                //        new GridButtonModel{U=Url.Action("Edit",new {id=c.Id}), T="Edit", D = GridButtonDialog.dialig1.ToString(), H="Edit"},
                //        new GridButtonModel{U=Url.Action("Relation",new {id=c.Id,tp=StandingDataPCRelationType.Material_Response_Activity}), T="Response Activity", D = GridButtonDialog.dialig1.ToString(), H="Activities", V=(id==StandingDataTypes.MaterialsOrItems)},
                //        new GridButtonModel{U=Url.Action("Relation",new {id=c.Id,tp=StandingDataPCRelationType.Material_Prepareness_Activity}), T="Prepareness Activity", D = GridButtonDialog.dialig1.ToString(), H="Activities", V=(id==StandingDataTypes.MaterialsOrItems)}
                //    }
            }).Skip(tke).Take(skp).ToArray();

            JQueryDataTable js = new JQueryDataTable();
            js.sEcho = ec;
            js.iTotalDisplayRecords = projList.Count().ToString();
            js.iTotalRecords = js.iTotalDisplayRecords;
            js.aaData = obj;

            return Json(js, JsonRequestBehavior.AllowGet);
        }

    }
}
