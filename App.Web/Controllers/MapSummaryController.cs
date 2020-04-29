using AppProj.Data.Infrastructure;
using AppProj.Domain.ModelExt;
using AppProj.Service.Services;
using AppProj.Web.Helpers;
using AppProj.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AppProj.Web.Controllers
{
    [Authorize]
    public class MapSummaryController : Controller
    {
        readonly IStandingDataService standingDataService;
        readonly IMapSummaryService mapSummaryService;
        readonly IDistrictDataService districtDataService;
        readonly IUnitOfWork unitOfWork;

        public MapSummaryController(IUnitOfWork unitOfWork, IStandingDataService standingDataService, IMapSummaryService mapSummaryService, IDistrictDataService districtDataService)
        {
            this.standingDataService = standingDataService;
            this.mapSummaryService = mapSummaryService;
            this.districtDataService = districtDataService;
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

            var disData = districtDataService.GetSummery();

            if (data.ToList().Count > 0)
            {
                return Json(data.Select(x => new
                {

                    DivisionId = x.DivisionId,
                    DistrictId = x.DistrictId,


                    PeopleIndex = disData.Where(l=>l.DistrictId == x.DistrictId).SingleOrDefault().PeopleIndex ?? 0,
                    HealthIndex = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().HealthIndex,
                    SupportIndex = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().SupportIndex,
                    BracIndex = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().BracIndex,

                    Latitude = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().Latitude,
                    Longitude = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().Longitude,

                    //People
                    DistrictName = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().DistrictName,
                    SummerizedData_ReachCount = x.SummerizedData_ReachCount ?? 0,
                    Population = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().Population ?? 0,

                    //Health
                    DetailData_TotalCount = x.DetailData_TotalCount ?? 0,
                    DetailData_MaleCount = x.DetailData_MaleCount ?? 0,
                    DetailData_FemaleCount = x.DetailData_FemaleCount ?? 0,
                    DetailData_MalePercent = x.DetailData_MalePercent ?? 0,
                    DetailData_FemalePercent = x.DetailData_FemalePercent ?? 0,

                    HospitalCount = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().HospitalCount,
                    BedCount = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().BedCount,

                    TotalQuarantine = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().TotalQuarantine,
                    TotalReleased = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().TotalQuarantine,
                    CurrentQuarantine = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().CurrentQuarantine,
                    TotalDoTestOn = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().TotalDoTestOn,
                    TotalDeath = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().TotalDeath,

                    //Govt. Support
                    PlannedRice = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().PlannedRice,
                    PlannedPotato = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().PlannedPotato,
                    PlannedDal = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().PlannedDal,
                    PlannedMoney = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().PlannedMoney,

                    TotalRice = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().TotalRice,
                    TotalDal = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().TotalDal,
                    TotalPotato = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().TotalPotato,
                    TotalMoney = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().TotalMoney,
                    TotalReliefFamily = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().TotalReliefFamily,

                    //Brac Internal
                    StaffWorkFromHome = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().StaffWorkFromHome ?? 0,
                    StaffAffected = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().StaffAffected ?? 0,
                    BracFinancialLoss = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().BracFinancialLoss ?? 0,
                    StaffMaleReach = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().StaffMaleReach ?? 0,
                    StaffFemaleReach = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().StaffFemaleReach ?? 0

                }), JsonRequestBehavior.AllowGet);
            }
            else
                return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDataAll()
        {
            var data = districtDataService.GetSummery();

            if (data.ToList().Count > 0)
            {
                return Json(data.Select(x => new
                {

                    DivisionId = x.DivisionId,
                    DistrictId = x.DistrictId,


                    PeopleIndex = x.PeopleIndex,
                    HealthIndex = x.HealthIndex,
                    SupportIndex = x.SupportIndex,
                    BracIndex = x.BracIndex,

                    Latitude = x.Latitude,
                    Longitude = x.Longitude,

                    //People
                    DistrictName = x.DistrictName,
                    SummerizedData_ReachCount = x.SummerizedData_ReachCount ?? 0,
                    Population = x.Population ?? 0,

                    //Health
                    DetailData_TotalCount = x.DetailData_TotalCount ?? 0,
                    DetailData_MaleCount = x.DetailData_MaleCount ?? 0,
                    DetailData_FemaleCount = x.DetailData_FemaleCount ?? 0,
                    DetailData_MalePercent = x.DetailData_MalePercent ?? 0,
                    DetailData_FemalePercent = x.DetailData_FemalePercent ?? 0,

                    HospitalCount = x.HospitalCount,
                    BedCount = x.BedCount,

                    TotalQuarantine = x.TotalQuarantine,
                    TotalReleased = x.TotalQuarantine,
                    CurrentQuarantine = x.CurrentQuarantine,
                    TotalDoTestOn = x.TotalDoTestOn,
                    TotalDeath = x.TotalDeath,

                    //Govt. Support
                    PlannedRice = x.PlannedRice,
                    PlannedPotato = x.PlannedPotato,
                    PlannedDal = x.PlannedDal,
                    PlannedMoney = x.PlannedMoney,

                    TotalRice = x.TotalRice,
                    TotalDal = x.TotalDal,
                    TotalPotato = x.TotalPotato,
                    TotalMoney = x.TotalMoney,
                    TotalReliefFamily = x.TotalReliefFamily,

                    //Brac Internal
                    StaffWorkFromHome = x.StaffWorkFromHome ?? 0,
                    StaffAffected = x.StaffAffected ?? 0,
                    BracFinancialLoss = x.BracFinancialLoss ?? 0,
                    StaffMaleReach = x.StaffMaleReach ?? 0,
                    StaffFemaleReach = x.StaffFemaleReach ?? 0

                }), JsonRequestBehavior.AllowGet);
            }
            else
                return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDataByDivisionId(int? id)
        {
            var data = districtDataService.GetSummery().Where(l => l.DivisionId == id);

            if (data.ToList().Count > 0)
            {
                return Json(data.Select(x => new
                {

                    DivisionId = x.DivisionId,
                    DistrictId = x.DistrictId,


                    PeopleIndex = x.PeopleIndex,
                    HealthIndex = x.HealthIndex,
                    SupportIndex = x.SupportIndex,
                    BracIndex = x.BracIndex,

                    Latitude = x.Latitude,
                    Longitude = x.Longitude,

                    //People
                    DistrictName = x.DistrictName,
                    SummerizedData_ReachCount = x.SummerizedData_ReachCount ?? 0,
                    Population = x.Population ?? 0,

                    //Health
                    DetailData_TotalCount = x.DetailData_TotalCount ?? 0,
                    DetailData_MaleCount = x.DetailData_MaleCount ?? 0,
                    DetailData_FemaleCount = x.DetailData_FemaleCount ?? 0,
                    DetailData_MalePercent = x.DetailData_MalePercent ?? 0,
                    DetailData_FemalePercent = x.DetailData_FemalePercent ?? 0,

                    HospitalCount = x.HospitalCount,
                    BedCount = x.BedCount,

                    TotalQuarantine = x.TotalQuarantine,
                    TotalReleased = x.TotalQuarantine,
                    CurrentQuarantine = x.CurrentQuarantine,
                    TotalDoTestOn = x.TotalDoTestOn,
                    TotalDeath = x.TotalDeath,

                    //Govt. Support
                    PlannedRice = x.PlannedRice,
                    PlannedPotato = x.PlannedPotato,
                    PlannedDal = x.PlannedDal,
                    PlannedMoney = x.PlannedMoney,

                    TotalRice = x.TotalRice,
                    TotalDal = x.TotalDal,
                    TotalPotato = x.TotalPotato,
                    TotalMoney = x.TotalMoney,
                    TotalReliefFamily = x.TotalReliefFamily,

                    //Brac Internal
                    StaffWorkFromHome = x.StaffWorkFromHome ?? 0,
                    StaffAffected = x.StaffAffected ?? 0,
                    BracFinancialLoss = x.BracFinancialLoss ?? 0,
                    StaffMaleReach = x.StaffMaleReach ?? 0,
                    StaffFemaleReach = x.StaffFemaleReach ?? 0

                }), JsonRequestBehavior.AllowGet);
            }
            else
                return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDataByProgramIdDivisionId(int id1, int id2)
        {
            var data = mapSummaryService.Get().Where(l => l.ProgramId == id1 && l.DivisionId == id2);

            var disData = districtDataService.GetSummery();

            if (data.ToList().Count > 0)
            {
                return Json(data.Select(x => new
                {

                    DivisionId = x.DivisionId,
                    DistrictId = x.DistrictId,


                    PeopleIndex = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().PeopleIndex ?? 0,
                    HealthIndex = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().HealthIndex,
                    SupportIndex = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().SupportIndex,
                    BracIndex = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().BracIndex,

                    Latitude = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().Latitude,
                    Longitude = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().Longitude,

                    //People
                    DistrictName = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().DistrictName,
                    SummerizedData_ReachCount = x.SummerizedData_ReachCount ?? 0,
                    Population = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().Population ?? 0,

                    //Health
                    DetailData_TotalCount = x.DetailData_TotalCount ?? 0,
                    DetailData_MaleCount = x.DetailData_MaleCount ?? 0,
                    DetailData_FemaleCount = x.DetailData_FemaleCount ?? 0,
                    DetailData_MalePercent = x.DetailData_MalePercent ?? 0,
                    DetailData_FemalePercent = x.DetailData_FemalePercent ?? 0,

                    HospitalCount = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().HospitalCount,
                    BedCount = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().BedCount,

                    TotalQuarantine = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().TotalQuarantine,
                    TotalReleased = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().TotalQuarantine,
                    CurrentQuarantine = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().CurrentQuarantine,
                    TotalDoTestOn = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().TotalDoTestOn,
                    TotalDeath = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().TotalDeath,

                    //Govt. Support
                    PlannedRice = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().PlannedRice,
                    PlannedPotato = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().PlannedPotato,
                    PlannedDal = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().PlannedDal,
                    PlannedMoney = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().PlannedMoney,

                    TotalRice = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().TotalRice,
                    TotalDal = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().TotalDal,
                    TotalPotato = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().TotalPotato,
                    TotalMoney = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().TotalMoney,
                    TotalReliefFamily = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().TotalReliefFamily,

                    //Brac Internal
                    StaffWorkFromHome = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().StaffWorkFromHome ?? 0,
                    StaffAffected = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().StaffAffected ?? 0,
                    BracFinancialLoss = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().BracFinancialLoss ?? 0,
                    StaffMaleReach = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().StaffMaleReach ?? 0,
                    StaffFemaleReach = disData.Where(l => l.DistrictId == x.DistrictId).SingleOrDefault().StaffFemaleReach ?? 0

                }), JsonRequestBehavior.AllowGet);
            }
            else
                return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDataByPriorityIdDivisionId(int id1, int id2)
        {
            var data = districtDataService.GetSummery()
            .Where(l => (l.PeopleIndex == id1 || l.HealthIndex == id1 || l.SupportIndex == id1 || l.BracIndex == id1) && l.DivisionId == id2);

            if (data.ToList().Count > 0)
            {
                return Json(data.Select(x => new
                {

                    DivisionId = x.DivisionId,
                    DistrictId = x.DistrictId,


                    PeopleIndex = x.PeopleIndex,
                    HealthIndex = x.HealthIndex,
                    SupportIndex = x.SupportIndex,
                    BracIndex = x.BracIndex,

                    Latitude = x.Latitude,
                    Longitude = x.Longitude,

                    //People
                    DistrictName = x.DistrictName,
                    SummerizedData_ReachCount = x.SummerizedData_ReachCount ?? 0,
                    Population = x.Population ?? 0,

                    //Health
                    DetailData_TotalCount = x.DetailData_TotalCount ?? 0,
                    DetailData_MaleCount = x.DetailData_MaleCount ?? 0,
                    DetailData_FemaleCount = x.DetailData_FemaleCount ?? 0,
                    DetailData_MalePercent = x.DetailData_MalePercent ?? 0,
                    DetailData_FemalePercent = x.DetailData_FemalePercent ?? 0,

                    HospitalCount = x.HospitalCount,
                    BedCount = x.BedCount,

                    TotalQuarantine = x.TotalQuarantine,
                    TotalReleased = x.TotalQuarantine,
                    CurrentQuarantine = x.CurrentQuarantine,
                    TotalDoTestOn = x.TotalDoTestOn,
                    TotalDeath = x.TotalDeath,

                    //Govt. Support
                    PlannedRice = x.PlannedRice,
                    PlannedPotato = x.PlannedPotato,
                    PlannedDal = x.PlannedDal,
                    PlannedMoney = x.PlannedMoney,

                    TotalRice = x.TotalRice,
                    TotalDal = x.TotalDal,
                    TotalPotato = x.TotalPotato,
                    TotalMoney = x.TotalMoney,
                    TotalReliefFamily = x.TotalReliefFamily,

                    //Brac Internal
                    StaffWorkFromHome = x.StaffWorkFromHome ?? 0,
                    StaffAffected = x.StaffAffected ?? 0,
                    BracFinancialLoss = x.BracFinancialLoss ?? 0,
                    StaffMaleReach = x.StaffMaleReach ?? 0,
                    StaffFemaleReach = x.StaffFemaleReach ?? 0

                }), JsonRequestBehavior.AllowGet);
            }
            else
                return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDataByPriorityIdAllDivision(int id)
        {
            var data = districtDataService.GetSummery()
                .Where(l => (l.PeopleIndex == id || l.HealthIndex == id || l.SupportIndex == id || l.BracIndex == id));

            if (data.ToList().Count > 0)
            {
                return Json(data.Select(x => new
                {

                    DivisionId = x.DivisionId,
                    DistrictId = x.DistrictId,


                    PeopleIndex = x.PeopleIndex,
                    HealthIndex = x.HealthIndex,
                    SupportIndex = x.SupportIndex,
                    BracIndex = x.BracIndex,

                    Latitude = x.Latitude,
                    Longitude = x.Longitude,

                    //People
                    DistrictName = x.DistrictName,
                    SummerizedData_ReachCount = x.SummerizedData_ReachCount ?? 0,
                    Population = x.Population ?? 0,

                    //Health
                    DetailData_TotalCount = x.DetailData_TotalCount ?? 0,
                    DetailData_MaleCount = x.DetailData_MaleCount ?? 0,
                    DetailData_FemaleCount = x.DetailData_FemaleCount ?? 0,
                    DetailData_MalePercent = x.DetailData_MalePercent ?? 0,
                    DetailData_FemalePercent = x.DetailData_FemalePercent ?? 0,

                    HospitalCount = x.HospitalCount,
                    BedCount = x.BedCount,

                    TotalQuarantine = x.TotalQuarantine,
                    TotalReleased = x.TotalQuarantine,
                    CurrentQuarantine = x.CurrentQuarantine, 
                    TotalDoTestOn = x.TotalDoTestOn,
                    TotalDeath = x.TotalDeath,

                    //Govt. Support
                    PlannedRice = x.PlannedRice,
                    PlannedPotato = x.PlannedPotato, 
                    PlannedDal = x.PlannedDal,
                    PlannedMoney = x.PlannedMoney,

                    TotalRice = x.TotalRice,
                    TotalDal = x.TotalDal,
                    TotalPotato = x.TotalPotato, 
                    TotalMoney = x.TotalMoney, 
                    TotalReliefFamily = x.TotalReliefFamily,

                    //Brac Internal
                    StaffWorkFromHome = x.StaffWorkFromHome ?? 0,
                    StaffAffected = x.StaffAffected ?? 0,
                    BracFinancialLoss = x.BracFinancialLoss ?? 0,
                    StaffMaleReach = x.StaffMaleReach ?? 0,
                    StaffFemaleReach = x.StaffFemaleReach ?? 0

                }), JsonRequestBehavior.AllowGet);
            }
            else
                return Json(null, JsonRequestBehavior.AllowGet);
        }

    }
}
