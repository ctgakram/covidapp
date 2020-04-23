using AppProj.Data.Infrastructure;
using AppProj.Service.Services;
using AppProj.Web.Helpers;
using AppProj.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace AppProj.Web.Controllers
{
    [Authorize]
    public class MapSummaryController : Controller
    {
        readonly IStandingDataService standingDataService;
        readonly IMapSummaryService mapSummaryService;
        readonly IUnitOfWork unitOfWork;

        public MapSummaryController(IUnitOfWork unitOfWork, IStandingDataService standingDataService, IMapSummaryService mapSummaryService)
        {
            this.standingDataService = standingDataService;
            this.mapSummaryService = mapSummaryService;
            this.unitOfWork = unitOfWork;

        }

        public ActionResult Index()
        {
            SearchModel up = new SearchModel();
          
            var prog = standingDataService.GetSource().Where(r => r.IsActive);
            up.ContentTypes3 = prog.ToSelectList(null, "Id", "Name");

            var div = standingDataService.GetDivisions().Where(r => r.IsActive);
            up.ContentTypes1 = div.ToSelectList(null, "Id", "Name");

            return View(up);
        }

        public JsonResult GetDataByProgramId(int? id)
        {
            var data = mapSummaryService.Get().Where(l => l.ProgramId == null ? true : l.ProgramId == id);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDataAll()
        {
            var data = mapSummaryService.Get().Distinct().DistinctBy(l=>l.DistrictId).Where(x=>x.Latitude != null);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDataByDivisionId(int? id)
        {
            var data = mapSummaryService.Get().Where(l => l.DivisionId == null ? true : l.DivisionId == id);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDataByProgramIdDivisionId(int id1,int id2)
        {
            var data = mapSummaryService.Get().Where(l => l.ProgramId == id1 && l.DivisionId == id2);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}
