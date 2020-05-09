using AppProj.Data.Infrastructure;
using AppProj.Domain;
using AppProj.Domain.ModelExt;
using AppProj.Service.Services;
using AppProj.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace AppProj.Web.Controllers
{
    public class MapController : Controller
    {
        readonly IStandingDataService standingDataService;
        readonly IMapSummaryService mapSummaryService;
        readonly IDistrictDataService districtDataService;
        readonly IUnitOfWork unitOfWork;

        public MapController(IUnitOfWork unitOfWork, IStandingDataService standingDataService, IMapSummaryService mapSummaryService, IDistrictDataService districtDataService)
        {
            this.standingDataService = standingDataService;
            this.mapSummaryService = mapSummaryService;
            this.districtDataService = districtDataService;
            this.unitOfWork = unitOfWork;

        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetData()
        {
            //int? prId, int? disId, bool ppl, bool health, bool relief, bool brac
            int? prId; int? disId; bool ppl; bool health; bool relief; bool brac;

            List<MapModel> model = new List<MapModel>();

            var data = districtDataService.GetSummery();

            model = data.Select(c => new MapModel
            {
                Name = c.DistrictCode
                 ,
                Lat = c.Latitude.Value
                 ,
                Lon = c.Longitude.Value
                 ,
                MapModelBrac = new MapModelBrac
                {
                    Food = (c.BracFoodDistribution ?? 0).ToString()
                      ,
                    Money = (c.BracMoneyDistribution ?? 0).ToString()
                }
                 ,
                MapModelIndex = new MapModelIndex
                {
                    IndexHealth = c.HealthIndex ?? 0
                      ,
                    IndexPopulation = c.PeopleIndex ?? 0
                      ,
                    IndexRelief = c.SupportIndex ?? 0
                }
                ,
                Index = ((c.HealthIndex ?? 0) + (c.PeopleIndex ?? 0) + (c.SupportIndex ?? 0))/3
            }).ToList();


            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FetchPatientCount()
        {
            string URL = "https://services9.arcgis.com/qclw57aOgdtDP5l4/arcgis/rest/services/district_wise_cases_lat_lon/FeatureServer/0/query?f=json&where=1%3D1&returnGeometry=false&spatialRel=esriSpatialRelIntersects&outFields=*&orderByFields=cases%20desc&resultOffset=0&resultRecordCount=500&resultType=standard&cacheHint=true";
            string param = "";
        
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(param).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {

                // Parse the response body.
                var json = response.Content.ReadAsStringAsync().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll

                var dataObjects = JsonConvert.DeserializeObject<CoronaGov>(json);

                DateTime dt = DateTime.Now.Date;

                //IEnumerable<DistrictPatient> dp = districtDataService.GetPatient(dt);

                var dis  = standingDataService.GetDistricts().ToList();
                DateTime inDt= DateTime.Now.Date.AddDays(-1);
                foreach (var row in dataObjects.features)
                {
                    DistrictPatient entity = new DistrictPatient();
                    int i = 0;

                    string dis_name = row.attributes.district_city_eng.Trim().ToLower();

                    if(dis_name== "brahmanbaria") { dis_name = "brahamanbaria"; }
                    else if (dis_name == "chapai nawabganj") { dis_name = "nawabganj"; }

                    var sd = dis.Where(c => c.Description.Trim().ToLower() == dis_name).FirstOrDefault();

                    if(sd !=null)
                    {
                        entity.DistrictId = sd.Id;
                    }

                    entity.NameCode = row.attributes.district_city_eng;
                    entity.Date = inDt;

                    //int.TryParse(row.attributes.cases,out i);
                    entity.TillPatientCount = row.attributes.cases;

                    districtDataService.AddPatient(entity);
                }
                
                unitOfWork.Commit();

            }
            
            //Make any other calls using HttpClient here.

            //Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();
        

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
