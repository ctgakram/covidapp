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
using AppProj.Domain.ModelExt;

namespace AppProj.Web.Controllers
{
    [Authorize]
    public class BepSubmissionController : Controller
    {
        readonly IStandingDataService standingDataService;
        readonly IBepDataService bepDataService;
        readonly IProgramByUserProfileService progService;
        readonly IUnitOfWork unitOfWork;

        public BepSubmissionController(IUnitOfWork unitOfWork, IProgramByUserProfileService progService, IBepDataService bepDataService, IStandingDataService standingDataService)
        {
            this.standingDataService = standingDataService;
            this.bepDataService = bepDataService;
            this.progService = progService;
            this.unitOfWork = unitOfWork;

        }

        public ActionResult Index()
        {
            SearchModel up = new SearchModel();

            var div = standingDataService.GetDivisions().Where(r => r.IsActive);
            up.ContentTypes1 = div.ToSelectList(null, "Id", "Name");

            var dis = standingDataService.GetDistricts(0).Where(r => r.IsActive);
            up.ContentTypes2 = dis.ToSelectList(null, "Id", "Name");

            var prog = standingDataService.GetSource().Where(r => r.IsActive);
            up.ContentTypes3 = prog.ToSelectList(null, "Id", "Name");

            up.FromDate = DateTime.Now;
            up.ToDate = DateTime.Now;

            return View(up);
        }

        [CustomAuthorize(Roles: new string[] { "ALL_PROGRAM_DATA" })]
        public ActionResult Create()
        {
            int div = 0;
            int.TryParse("" + SessionHelper.Temp, out div);

            int dis = 0;
            int.TryParse("" + SessionHelper.Temp2, out dis);

            int pro = 0;
            int.TryParse("" + SessionHelper.Temp3, out pro);

            BEPModel up = new BEPModel();

            var divs = standingDataService.GetDivisions().Where(r => r.IsActive);
            up.Divisions = divs.ToSelectList(div, "Id", "Name");

            div = (div == 0 ? divs.FirstOrDefault().Id : div);
            var districts = standingDataService.GetDistricts(div).Where(r => r.IsActive);

            int i = (dis == 0 ? districts.FirstOrDefault().Id : dis);
            up.Districts = districts.ToSelectList(i, "Id", "Name");

            var src = standingDataService.GetSource().Where(r => r.IsActive);

            var prg = progService.GetByUser(SessionHelper.UserId).Select(c => c.ProgramId).ToList();

            if (prg.Count() > 0)
            {
                up.Sources = src.Where(c => prg.Contains(c.Id)).ToSelectList(pro, "Id", "Name");
            }
            else
            {
                up.Sources = src.ToSelectList(pro, "Id", "Name");
            }

            up.Date = DateTime.Now;

            return PartialView(up);
        }

        [CustomAuthorize(Roles: new string[] { "ALL_PROGRAM_DATA" })]
        public ActionResult CreateDetail(BEPModel model)
        {
            BEPModel up = new BEPModel();

            ModelCopier.CopyModel(model, up);

            up.ProgramName = standingDataService.GetDataById(up.ProgramId).Name;
            up.DivisionName = standingDataService.GetDataById(up.DivisionId).Name;
            up.DistrictName = standingDataService.GetDataById(up.DistrictId).Name;

            up.ImpactedReasons = standingDataService.GetImpactedReasons().Where(r => r.IsActive).ToSelectList(null, "Id", "Name");

            up.Restrictions = standingDataService.GetRestrictions().Where(r => r.IsActive).ToSelectList(null, "Id", "Name");

            up.CommMaterials = standingDataService.GetCommMaterials().Where(r => r.IsActive).ToSelectList(null, "Id", "Name");

            up.CommChannels = standingDataService.GetCommChannels().Where(r => r.IsActive).ToSelectList(null, "Id", "Name");

            var pcPreItems = standingDataService.GetParentChild(StandingDataPCRelationType.Material_Prepareness_Activity)
                .OrderBy(c => c.ChildId)
                .ToList();

            up.BEPPreparenessActivityModels = new List<BEPDetailModel>();

            foreach (var r in pcPreItems)
            {
                up.BEPPreparenessActivityModels.Add(new BEPDetailModel
                {
                    ItemId = r.ParentId
                     ,
                    ActivityId = r.ChildId
                     ,
                    ItemName = r.StandingData1.Name
                    ,
                    ActivityName = r.StandingData.Name
                     ,
                    Quantity = 0
                    ,
                    ExpQuantity = 0
                    ,
                    ReachCount = 0
                    ,
                    ReachCountFemale = 0

                });
            }

            var pcResItems = standingDataService.GetParentChild(StandingDataPCRelationType.Material_Response_Activity)
                .OrderBy(c => c.ChildId)
                .ToList();

            up.BEPResponseActivityModels = pcResItems.GroupBy(c => new { c.ChildId, c.StandingData.Name })
                .Select(c => new BEPResponseActivityModel { ActivityId = c.Key.ChildId, ActivityName = c.Key.Name })
                .ToList();


            foreach (var v in up.BEPResponseActivityModels)
            {
                v.BEPDetailModels = pcResItems.Where(c => c.ChildId == v.ActivityId)
                    .Select(d => new BEPDetailModel { ActivityId = v.ActivityId, ItemId = d.ParentId, ItemName = d.StandingData1.Name })
                    .ToList();

            }

            return PartialView(up);
        }

        [CustomAuthorize(Roles: new string[] { "ALL_PROGRAM_DATA" })]
        public ActionResult Edit(int Id)
        {
            var entity = bepDataService.Get(Id);

            BEPModel up = new BEPModel();
            ModelCopier.CopyModel(entity, up);

            up.Divisions = standingDataService.GetDivisions().Where(r => r.IsActive).ToSelectList(entity.DivisionId, "Id", "Name");

            var districts = standingDataService.GetDistricts(entity.DivisionId).Where(r => r.IsActive);

            up.Districts = districts.ToSelectList(entity.DistrictId, "Id", "Name");

            var src = standingDataService.GetSource().Where(r => r.IsActive);

            var prg = progService.GetByUser(SessionHelper.UserId).Select(c => c.ProgramId).ToList();

            if (prg.Count() > 0)
            {
                up.Sources = src.Where(c => prg.Contains(c.Id)).ToSelectList(entity.ProgramId, "Id", "Name");
            }
            else
            {
                up.Sources = src.ToSelectList(entity.ProgramId, "Id", "Name");
            }

            up.ImpactedReasons = standingDataService.GetImpactedReasons().Where(r => r.IsActive).ToSelectList(entity.ImpactProgramId, "Id", "Name");

            up.Restrictions = standingDataService.GetRestrictions().Where(r => r.IsActive).ToSelectList(entity.RestrictonId, "Id", "Name");

            up.CommMaterials = standingDataService.GetCommMaterials().Where(r => r.IsActive).ToSelectList(entity.ComMaterialId, "Id", "Name");

            up.CommChannels = standingDataService.GetCommChannels().Where(r => r.IsActive).ToSelectList(entity.ComChannelId, "Id", "Name");

            up.ProgramName = standingDataService.GetDataById(up.ProgramId).Name;
            up.DivisionName = standingDataService.GetDataById(up.DivisionId).Name;
            up.DistrictName = standingDataService.GetDataById(up.DistrictId).Name;

            up.BEPPreparenessActivityModels = new List<BEPDetailModel>();


            var preExisting = entity.BERDataItemWiseQuantities
                .Where(c => c.Type == ActivitiesDataType.Preparation)
                .ToList();


            foreach (var r in preExisting)
            {
                up.BEPPreparenessActivityModels.Add(new BEPDetailModel
                {
                    ItemId = r.ItemId
                     ,
                    ActivityId = r.ActivityId
                     ,
                    ItemName = r.StandingData.Name
                    ,
                    ActivityName = r.StandingData4.Name
                     ,
                    Quantity = r.Quantity
                    ,
                    ExpQuantity = r.ExpQuantity
                    ,
                    ReachCount = r.ReachCount
                    ,
                    ReachCountFemale = r.ReachCountFemale
                    ,
                    Id = r.Id

                });
            }

            var pcPreItems = standingDataService.GetParentChild(StandingDataPCRelationType.Material_Prepareness_Activity)
                .OrderBy(c => c.ChildId)
                .ToList();


            foreach (var v in preExisting)
            {
                pcPreItems.RemoveAll(c => c.ParentId == v.ItemId && c.ChildId == v.ActivityId);

            }


            foreach (var r in pcPreItems)
            {
                up.BEPPreparenessActivityModels.Add(new BEPDetailModel
                {
                    ItemId = r.ParentId
                     ,
                    ActivityId = r.ChildId
                     ,
                    ItemName = r.StandingData.Name
                    ,
                    ActivityName = r.StandingData1.Name
                     ,
                    Quantity = 0
                    ,
                    ExpQuantity = 0
                    ,
                    ReachCount = 0
                    ,
                    ReachCountFemale = 0

                });
            }

            var rchExisting = entity.BERDataItemWiseQuantities
                .Where(c => c.Type == ActivitiesDataType.Reach)
                .ToList();

            var rchPplExisting = entity.BERDataPeopleWiseQuantities
               .Where(c => c.Type == ActivitiesDataType.Reach)
               .ToList();


            var pcResItems = standingDataService.GetParentChild(StandingDataPCRelationType.Material_Response_Activity)
                .OrderBy(c => c.ChildId)
                .ToList();

            up.BEPResponseActivityModels = pcResItems.GroupBy(c => new { c.ChildId, c.StandingData.Name })
                .Select(c => new BEPResponseActivityModel { ActivityId = c.Key.ChildId, ActivityName = c.Key.Name })
                .ToList();

            foreach (var v in up.BEPResponseActivityModels)
            {
                List<int> ids = pcResItems.Where(c => c.ChildId == v.ActivityId).Select(c => c.ParentId).ToList();

                v.BEPDetailModels = pcResItems.Where(c => c.ChildId == v.ActivityId)
                    .Select(d => new BEPDetailModel
                    {
                        ActivityId = v.ActivityId
                    ,
                        ItemId = d.ParentId
                    ,
                        ItemName = d.StandingData1.Name
                    ,
                        Quantity = rchExisting.Where(e => e.ActivityId == v.ActivityId && e.ItemId == d.ParentId).FirstOrDefault() == null ? 0 : rchExisting.Where(e => e.ActivityId == v.ActivityId && e.ItemId == d.ParentId).FirstOrDefault().Quantity
                    ,
                        ExpQuantity = rchExisting.Where(e => e.ActivityId == v.ActivityId && e.ItemId == d.ParentId).FirstOrDefault() == null ? 0 : rchExisting.Where(e => e.ActivityId == v.ActivityId && e.ItemId == d.ParentId).FirstOrDefault().ExpQuantity
                    ,
                        ReachCount = rchExisting.Where(e => e.ActivityId == v.ActivityId && e.ItemId == d.ParentId).FirstOrDefault() == null ? 0 : rchExisting.Where(e => e.ActivityId == v.ActivityId && e.ItemId == d.ParentId).FirstOrDefault().ReachCount
                        ,
                        ReachCountFemale = rchExisting.Where(e => e.ActivityId == v.ActivityId && e.ItemId == d.ParentId).FirstOrDefault() == null ? 0 : rchExisting.Where(e => e.ActivityId == v.ActivityId && e.ItemId == d.ParentId).FirstOrDefault().ReachCountFemale
                    })
                    .ToList();


                rchExisting.RemoveAll(c => c.ActivityId == v.ActivityId && ids.Contains(c.ItemId));

                foreach (var e in rchExisting.Where(c => c.ActivityId == v.ActivityId))
                {
                    v.BEPDetailModels.Add(new BEPDetailModel
                    {
                        ActivityId = v.ActivityId
                    ,
                        ItemId = e.ItemId
                    ,
                        ItemName = e.StandingData1.Name
                        ,
                        Quantity = e.Quantity
                        ,
                        ExpQuantity = e.ExpQuantity
                        ,
                        ReachCount = e.ReachCount
                        ,
                        ReachCountFemale = e.ReachCountFemale
                    ,
                    });
                }

                var vv = rchPplExisting.Where(c => c.ActivityId == v.ActivityId).FirstOrDefault();
                ModelCopier.CopyModel(vv, v);

                rchPplExisting.RemoveAll(c => c.ActivityId == v.ActivityId);
            }

            foreach (var v in rchPplExisting)
            {
                BEPResponseActivityModel m = new BEPResponseActivityModel();

                ModelCopier.CopyModel(v, m);

                m.ActivityName = v.StandingData.Name;

                m.BEPDetailModels = rchExisting.Where(e => e.ActivityId == v.ActivityId)
                    .Select(c => new BEPDetailModel
                    {
                        ActivityId = v.ActivityId,
                        Id = c.Id,
                        ExpQuantity = c.ExpQuantity,
                        ItemId = c.ItemId
                    ,
                        ItemName = c.StandingData1.Name,
                        Quantity = c.Quantity,
                        ReachCount = c.ReachCount,
                        ReachCountFemale = c.ReachCountFemale
                    })
                    .ToList();

                up.BEPResponseActivityModels.Add(m);


            }

            return PartialView("CreateDetail", up);

        }

        [CustomAuthorize(Roles: new string[] { "ALL_PROGRAM_DATA" })]
        public ActionResult Save(BEPModel model)
        {
            SessionHelper.Temp = model.DistrictId;
            SessionHelper.Temp2 = model.DivisionId;
            SessionHelper.Temp2 = model.ProgramId;

            BEPData entity = new BEPData();

            if (model.Id > 0)
            {
                entity = bepDataService.Get(model.Id);
            }


            //if (model.Id > 0)
            //{
            //    entity = bepDataService.Get(model.Id);
            //    model.AffectedBracWorkerCount = entity.AffectedBracWorkerCount;
            //    model.WorkFromHomeStaffCount = entity.WorkFromHomeStaffCount;
            //    model.FinancialLoss = entity.FinancialLoss;
            //    model.ImpactProgramId = entity.ImpactProgramId;
            //    model.RestrictonId = entity.RestrictonId;
            //}

            ModelCopier.CopyModel(model, entity);

            entity.InsertedById = SessionHelper.UserId;
            entity.LastUpdated = DateTime.Now;

            if (entity.Id == 0)
            {
                bepDataService.Add(entity);
            }
            else
            {
                bepDataService.Update(entity);
            }

            /*
            //summery data
            if (model.Id > 0)
            {
                BERDataSummery entitySum = bepDataService.GetSummery(model.DivisionId, model.DistrictId, model.ProgramId);

                if (entitySum == null)
                {
                    entitySum = new BERDataSummery();
                }

                if (entitySum.AffectedBracWorkerCount == model.AffectedBracWorkerCount &&
                entitySum.WorkFromHomeStaffCount == model.WorkFromHomeStaffCount &&
                entitySum.FinancialLoss == model.FinancialLoss &&
                entitySum.ImpactProgramId == model.ImpactProgramId &&
                entitySum.RestrictonId == model.RestrictonId
                )
                {

                }
                else
                {
                    entitySum.AffectedBracWorkerCount = model.AffectedBracWorkerCount;
                    entitySum.WorkFromHomeStaffCount = model.WorkFromHomeStaffCount;
                    entitySum.FinancialLoss = model.FinancialLoss;
                    entitySum.DistrictId = entity.DistrictId;
                    entitySum.DivisionId = entity.DivisionId;
                    entitySum.ProgramId = entity.ProgramId;
                    entitySum.ImpactProgramId = model.ImpactProgramId;
                    entitySum.RestrictonId = model.RestrictonId;

                    entitySum.Date = model.Date;

                    entitySum.InsertedById = SessionHelper.UserId;
                    entitySum.LastUpdated = DateTime.Now;

                    if (entitySum.Id == 0)
                    {
                        bepDataService.AddSummery(entitySum);
                    }
                    else
                    {
                        bepDataService.UpdateSummery(entitySum);
                    }
                }
            }
            */

            //prepareness
            foreach (var m in model.BEPPreparenessActivityModels)
            {

                if (m.Quantity > 0 || m.ExpQuantity > 0 || m.ReachCount > 0 || m.ReachCountFemale > 0)
                {
                    BERDataItemWiseQuantity entityItem = new BERDataItemWiseQuantity();

                    ModelCopier.CopyModel(m, entityItem);

                    entityItem.BEPDataId = entity.Id;
                    entityItem.Date = entity.Date;
                    entityItem.DistrictId = entity.DistrictId;
                    entityItem.DivisionId = entity.DivisionId;
                    entityItem.ProgramId = entity.ProgramId;
                    entityItem.Type = ActivitiesDataType.Preparation;
                    entityItem.InsertedById = SessionHelper.UserId;
                    entityItem.LastUpdated = DateTime.Now;

                    if (m.Id <= 0)
                    {
                        bepDataService.AddItem(entityItem);
                    }
                    else
                    {
                        bepDataService.UpdateItem(entityItem);
                    }
                }
                else if (m.Id > 0)
                {
                    bepDataService.DeleteItem(bepDataService.GetSingleItem(m.Id));
                }
            }



            //reach
            foreach (var r in model.BEPResponseActivityModels)
            {
                if (r.Cat1NewReach > 0 || r.Cat1OldReach > 0
                    || r.Cat2NewReach > 0 || r.Cat2OldReach > 0
                    || r.Cat3NewReach > 0 || r.Cat3OldReach > 0
                    || r.Cat4NewReach > 0 || r.Cat4OldReach > 0
                    || r.Cat5NewReach > 0 || r.Cat5OldReach > 0
                    || r.Cat6NewReach > 0 || r.Cat6OldReach > 0
                    || r.Cat7NewReach > 0 || r.Cat7OldReach > 0
                    || r.Cat8NewReach > 0 || r.Cat8OldReach > 0)
                {
                    BERDataPeopleWiseQuantity entityPeople = new BERDataPeopleWiseQuantity();

                    ModelCopier.CopyModel(r, entityPeople);

                    entityPeople.BEPDataId = entity.Id;
                    entityPeople.Date = entity.Date;
                    entityPeople.DistrictId = entity.DistrictId;
                    entityPeople.DivisionId = entity.DivisionId;
                    entityPeople.ProgramId = entity.ProgramId;
                    entityPeople.Type = ActivitiesDataType.Reach;

                    entityPeople.InsertedById = SessionHelper.UserId;
                    entityPeople.LastUpdated = DateTime.Now;

                    if (r.Id <= 0)
                    {
                        bepDataService.AddPeople(entityPeople);
                    }
                    else
                    {
                        bepDataService.UpdatePeople(entityPeople);
                    }

                }
                else if (r.Id > 0)
                {

                    bepDataService.DeletePeople(bepDataService.GetSinglePeople(r.Id));
                }


                foreach (var m in r.BEPDetailModels)
                {

                    if (m.Quantity > 0 || m.ReachCount > 0 || m.ReachCountFemale > 0)
                    {
                        BERDataItemWiseQuantity entityItem = new BERDataItemWiseQuantity();

                        ModelCopier.CopyModel(m, entityItem);

                        entityItem.BEPDataId = entity.Id;
                        entityItem.Date = entity.Date;
                        entityItem.DistrictId = entity.DistrictId;
                        entityItem.DivisionId = entity.DivisionId;
                        entityItem.ProgramId = entity.ProgramId;
                        entityItem.Type = ActivitiesDataType.Reach;

                        entityItem.InsertedById = SessionHelper.UserId;
                        entityItem.LastUpdated = DateTime.Now;

                        if (m.Id <= 0)
                        {
                            bepDataService.AddItem(entityItem);
                        }
                        else
                        {
                            bepDataService.UpdateItem(entityItem);
                        }
                    }
                    else if (m.Id > 0)
                    {
                        bepDataService.DeleteItem(bepDataService.GetSingleItem(m.Id));
                    }
                }

            }

            unitOfWork.Commit();

            return PartialView();

        }

        public JsonResult DataGrid()
        {
            int count = 0;
            bool visible = UserRole.Check("ALL_PROGRAM_DATA", SessionHelper.Role);

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

            int? srcId = null;
            try
            {
                srcId = Convert.ToInt32(Request.QueryString["ContentTypeId3"]);
            }
            catch { }


            DateTime FromDate = Convert.ToDateTime(Request.QueryString["FromDate"]);
            DateTime ToDate = Convert.ToDateTime(Request.QueryString["ToDate"]);

            List<BEPData> dataList = bepDataService.Get(divId, disId, srcId, FromDate, ToDate, skip, take, out count).ToList();

            if (skip <= 0 && dataList.Count() > 0)
            {
                IEnumerable<BEPData> dataListSum = bepDataService.Get(divId, disId, srcId, FromDate, ToDate, 0, 1000000000, out count);

                BEPData tot = dataListSum.GroupBy(q => 1)
                .Select(g => new BEPData
                {
                    Id = 0
                 ,
                    Date = ToDate
                ,
                    StandingData6 = new StandingData { Name = "<span style=\"color:#ff6a00; font-size:14px;\">Total</span>" }
                   ,
                    StandingData = new StandingData { Name = "" }
                    ,
                    StandingData1 = new StandingData { Name = "" }
                    ,
                    StandingData2 = new StandingData { Name = "" }
                    ,
                    StandingData3 = new StandingData { Name = "" }
                    ,
                    StandingData4 = new StandingData { Name = "" }
                    ,
                    StandingData5 = new StandingData { Name = "" }
                    ,
                    UpazilaCoverageCount = g.Sum(c => c.UpazilaCoverageCount)
                    ,
                    Target = g.Sum(c => c.Target)
                    ,
                    AffectedBracWorkerCount = g.Sum(c => c.AffectedBracWorkerCount)
                    ,
                    FinancialLoss = g.Sum(c => c.FinancialLoss)
                    ,
                    WorkFromHomeStaffCount = g.Sum(c => c.WorkFromHomeStaffCount)
                    ,
                    InsertedById = -1
                    ,
                    UserProfile = new UserProfile { UserName = "" }
                })
                .Single();

                //dataList.Add(tot);
                dataList.Insert(0, tot);
            }


            var obj = (from c in dataList
                       select new object[] {
                       String.Format("{0:dd MMM, yyyy}", c.Date)
                        ,c.StandingData6.Name //program 
                       ,c.StandingData2.Name //division
                       ,c.StandingData5.Name //district
                       ,c.UpazilaCoverageCount
                       ,c.UpazillaRemarks
                       ,c.ImpactProgramId==null?"": c.StandingData3.Name // how impacted
                       ,c.RestrictonId==null?"": c.StandingData4.Name // restriction         
                       ,c.AffectedBracWorkerCount
                       ,c.WorkFromHomeStaffCount
                       ,c.FinancialLoss
                       ,c.ComMaterialId==null?"": c.StandingData1.Name //comm material
                       ,c.ComChannelId==null?"": c.StandingData.Name //comm channel
                       ,c.Target
                       ,c.Remarks
                       ,c.UserProfile.UserName
                       ,new GridButtonModel[]
                            {
                                 new GridButtonModel{U=Url.Action("Edit",new {Id=c.Id}), T="Edit", D = GridButtonDialog.dialig1.ToString(), H="Edit", M="class=\"btn btn-mini btn-warning\""
                                 , V = (visible && c.Id>0)}
                            }
            }).ToArray();



            JQueryDataTable js = new JQueryDataTable();
            js.sEcho = ec;
            js.iTotalDisplayRecords = count.ToString();
            js.iTotalRecords = js.iTotalDisplayRecords;
            js.aaData = obj;

            return Json(js, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IndexItem()
        {
            SearchModel up = new SearchModel();

            var div = standingDataService.GetDivisions().Where(r => r.IsActive);
            up.ContentTypes1 = div.ToSelectList(null, "Id", "Name");

            var dis = standingDataService.GetDistricts(0).Where(r => r.IsActive);
            up.ContentTypes2 = dis.ToSelectList(null, "Id", "Name");

            var prog = standingDataService.GetSource().Where(r => r.IsActive);
            up.ContentTypes3 = prog.ToSelectList(null, "Id", "Name");

            var act = standingDataService.GetByType(StandingDataTypes.Activities).Where(r => r.IsActive);
            up.ContentTypes4 = act.ToSelectList(null, "Id", "Name");


            up.FromDate = DateTime.Now;
            up.ToDate = DateTime.Now;

            return View(up);
        }

        public JsonResult DataGridItem()
        {
            int count = 0;

            int ec = int.Parse(Request.QueryString["sEcho"]);
            int take = int.Parse(Request.QueryString["iDisplayLength"]);
            int skip = int.Parse(Request.QueryString["iDisplayStart"]);
            bool isReach = bool.Parse(Request.QueryString["isR"]);
            bool isShow = bool.Parse(Request.QueryString["isShow"]);

            //if (take == -1) { take = 1000000000; skip = 0; }

            if(!isShow)
            {
                object[][] obj1 = new object[0][];

                if (isReach)
                {
                }

                JQueryDataTable js1 = new JQueryDataTable();
                js1.sEcho = ec;
                js1.iTotalDisplayRecords = count.ToString();
                js1.iTotalRecords = js1.iTotalDisplayRecords;
                js1.aaData = obj1;

                return Json(js1, JsonRequestBehavior.AllowGet);
            }

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

            int? srcId = null;
            try
            {
                srcId = Convert.ToInt32(Request.QueryString["ContentTypeId3"]);
            }
            catch { }

            int? actId = null;
            try
            {
                actId = Convert.ToInt32(Request.QueryString["ContentTypeId4"]);
            }
            catch { }

            DateTime FromDate = Convert.ToDateTime(Request.QueryString["FromDate"]);
            DateTime ToDate = Convert.ToDateTime(Request.QueryString["ToDate"]);

            List<BERDataItemWiseQuantityExt> dataList = new List<BERDataItemWiseQuantityExt>();
            object[][] obj = null;

            if (isReach)
            {
                dataList = bepDataService.GetPeople(divId, disId, srcId, actId, FromDate, ToDate).ToList();

                if (dataList.Count() > 0)
                {
                    BERDataItemWiseQuantityExt tot = dataList.GroupBy(q => 1)
                        .Select(g => new BERDataItemWiseQuantityExt
                        {
                            Activity = "<span style=\"color:#ff6a00; font-size:14px;\">Total</span>"
                             ,
                            Cat1NewReach = g.Sum(c => c.Cat1NewReach)
                           ,
                            Cat1OldReach = g.Sum(c => c.Cat1OldReach)
                           ,
                            Cat2NewReach = g.Sum(c => c.Cat2NewReach)
                           ,
                            Cat2OldReach = g.Sum(c => c.Cat2OldReach)
                           ,
                            Cat3NewReach = g.Sum(c => c.Cat3NewReach)
                           ,
                            Cat3OldReach = g.Sum(c => c.Cat3OldReach)
                           ,
                            Cat4NewReach = g.Sum(c => c.Cat4NewReach)
                           ,
                            Cat4OldReach = g.Sum(c => c.Cat4OldReach)
                           ,
                            Cat5NewReach = g.Sum(c => c.Cat5NewReach)
                           ,
                            Cat5OldReach = g.Sum(c => c.Cat5OldReach)
                           ,
                            Cat6NewReach = g.Sum(c => c.Cat6NewReach)
                           ,
                            Cat6OldReach = g.Sum(c => c.Cat6OldReach)
                            ,
                            Cat7NewReach = g.Sum(c => c.Cat7NewReach)
                           ,
                            Cat7OldReach = g.Sum(c => c.Cat7OldReach)
                            ,
                            Cat8NewReach = g.Sum(c => c.Cat8NewReach)
                           ,
                            Cat8OldReach = g.Sum(c => c.Cat8OldReach)
                        }).Single();

                    dataList.Insert(0, tot);
                }

                obj = (from c in dataList
                       select new object[] {
                       c.Activity
                       ,c.Cat7NewReach
                       ,c.Cat7OldReach
                       ,c.Cat1NewReach
                       ,c.Cat1OldReach
                       ,c.Cat2NewReach
                       ,c.Cat2OldReach
                       ,c.Cat3NewReach
                       ,c.Cat3OldReach
                       ,c.Cat4NewReach
                       ,c.Cat4OldReach
                       ,c.Cat5NewReach
                       ,c.Cat5OldReach
                       ,c.Cat6NewReach
                       ,c.Cat6OldReach
                       ,c.Cat8NewReach
                       ,c.Cat8OldReach

            }).ToArray();
            }
            else
            {
                dataList = bepDataService.GetItem(divId, disId, srcId, actId, FromDate, ToDate).ToList();
                if (dataList.Count() > 0)
                {
                    BERDataItemWiseQuantityExt tot = dataList.GroupBy(q => 1)
                    .Select(g => new BERDataItemWiseQuantityExt
                    {
                        Item = "<span style=\"color:#ff6a00; font-size:14px;\">Total</span>"
                         ,
                        ReachCount = g.Sum(c => c.ReachCount)
                        ,
                        ReachCountFemale = g.Sum(c => c.ReachCountFemale)
                       ,
                        ResReachCount = g.Sum(c => c.ResReachCount)
                    }).Single();

                    dataList.Insert(0, tot);
                }

                obj = (from c in dataList
                       select new object[] {
                       c.Item
                       ,c.Quantity
                       ,c.ExpQuantity
                       ,c.ReachCount
                       ,c.ReachCountFemale
                       ,c.ResQuantity
                       ,c.ResReachCount
            }).ToArray();
            }


            JQueryDataTable js = new JQueryDataTable();
            js.sEcho = ec;
            js.iTotalDisplayRecords = count.ToString();
            js.iTotalRecords = js.iTotalDisplayRecords;
            js.aaData = obj;

            return Json(js, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IndexCount()
        {
            SearchModel up = new SearchModel();

            var div = standingDataService.GetDivisions().Where(r => r.IsActive);
            up.ContentTypes1 = div.ToSelectList(null, "Id", "Name");

            var dis = standingDataService.GetDistricts(0).Where(r => r.IsActive);
            up.ContentTypes2 = dis.ToSelectList(null, "Id", "Name");

            var prog = standingDataService.GetSource().Where(r => r.IsActive);
            up.ContentTypes3 = prog.ToSelectList(null, "Id", "Name");

            var act = standingDataService.GetByType(StandingDataTypes.Activities).Where(r => r.IsActive);
            up.ContentTypes4 = act.ToSelectList(null, "Id", "Name");


            up.FromDate = DateTime.Now;
            up.ToDate = DateTime.Now;

            return View(up);
        }

        public JsonResult DataGridCount()
        {
            int count = 0;

            int ec = int.Parse(Request.QueryString["sEcho"]);
            int take = int.Parse(Request.QueryString["iDisplayLength"]);
            int skip = int.Parse(Request.QueryString["iDisplayStart"]);
            bool isReach = bool.Parse(Request.QueryString["isR"]);
            bool isShow = bool.Parse(Request.QueryString["isShow"]);

            //if (take == -1) { take = 1000000000; skip = 0; }

            if (!isShow)
            {
                object[][] obj1 = new object[0][];

                JQueryDataTable js1 = new JQueryDataTable();
                js1.sEcho = ec;
                js1.iTotalDisplayRecords = count.ToString();
                js1.iTotalRecords = js1.iTotalDisplayRecords;
                js1.aaData = obj1;

                return Json(js1, JsonRequestBehavior.AllowGet);
            }

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

            int? srcId = null;
            try
            {
                srcId = Convert.ToInt32(Request.QueryString["ContentTypeId3"]);
            }
            catch { }

            int? actId = null;
            try
            {
                actId = Convert.ToInt32(Request.QueryString["ContentTypeId4"]);
            }
            catch { }

            DateTime FromDate = Convert.ToDateTime(Request.QueryString["FromDate"]);
            DateTime ToDate = Convert.ToDateTime(Request.QueryString["ToDate"]);

            List<BERDataItemWiseQuantityExt> dataList = new List<BERDataItemWiseQuantityExt>();
            object[][] obj = null;

            if (isReach)
            {
                dataList = bepDataService.GetPeopleDetail(divId, disId, srcId, actId, FromDate, ToDate).ToList();

                if (dataList.Count() > 0)
                {
                    BERDataItemWiseQuantityExt tot = dataList.GroupBy(q => 1)
                        .Select(g => new BERDataItemWiseQuantityExt
                        {                            
                            Program = "<span style=\"color:#ff6a00; font-size:14px;\">Total</span>"
                            ,
                            Division = ""
                            ,
                            District = ""
                            ,
                            Activity = ""
                            ,
                            Cat1NewReach = g.Sum(c => c.Cat1NewReach)
                           ,
                            Cat1OldReach = g.Sum(c => c.Cat1OldReach)
                           ,
                            Cat2NewReach = g.Sum(c => c.Cat2NewReach)
                           ,
                            Cat2OldReach = g.Sum(c => c.Cat2OldReach)
                           ,
                            Cat3NewReach = g.Sum(c => c.Cat3NewReach)
                           ,
                            Cat3OldReach = g.Sum(c => c.Cat3OldReach)
                           ,
                            Cat4NewReach = g.Sum(c => c.Cat4NewReach)
                           ,
                            Cat4OldReach = g.Sum(c => c.Cat4OldReach)
                           ,
                            Cat5NewReach = g.Sum(c => c.Cat5NewReach)
                           ,
                            Cat5OldReach = g.Sum(c => c.Cat5OldReach)
                           ,
                            Cat6NewReach = g.Sum(c => c.Cat6NewReach)
                           ,
                            Cat6OldReach = g.Sum(c => c.Cat6OldReach)
                            ,
                            Cat7NewReach = g.Sum(c => c.Cat7NewReach)
                           ,
                            Cat7OldReach = g.Sum(c => c.Cat7OldReach)
                            ,
                            Cat8NewReach = g.Sum(c => c.Cat8NewReach)
                           ,
                            Cat8OldReach = g.Sum(c => c.Cat8OldReach)
                        }).Single();

                    dataList.Insert(0, tot);
                }

                obj = (from c in dataList
                       select new object[] {
                       c.Program
                       ,c.Division
                       ,c.District
                       ,c.Activity
                       ,c.Cat7NewReach
                       ,c.Cat7OldReach
                       ,c.Cat1NewReach
                       ,c.Cat1OldReach
                       ,c.Cat2NewReach
                       ,c.Cat2OldReach
                       ,c.Cat3NewReach
                       ,c.Cat3OldReach
                       ,c.Cat4NewReach
                       ,c.Cat4OldReach
                       ,c.Cat5NewReach
                       ,c.Cat5OldReach
                       ,c.Cat6NewReach
                       ,c.Cat6OldReach
                       ,c.Cat8NewReach
                       ,c.Cat8OldReach

            }).ToArray();
            }
            else
            {
                dataList = bepDataService.GetItemDetails(divId, disId, srcId, actId, FromDate, ToDate).ToList();
                if (dataList.Count() > 0)
                {
                    BERDataItemWiseQuantityExt tot = dataList.GroupBy(q => 1)
                    .Select(g => new BERDataItemWiseQuantityExt
                    {
                        
                        Program = "<span style=\"color:#ff6a00; font-size:14px;\">Total</span>"
                        ,
                        Division = ""
                         ,
                        District = ""
                         ,
                        Item = ""
                         ,
                        ReachCount = g.Sum(c => c.ReachCount)
                        ,
                        ReachCountFemale = g.Sum(c => c.ReachCountFemale)
                       ,
                        ResReachCount = g.Sum(c => c.ResReachCount)
                    }).Single();

                    dataList.Insert(0, tot);
                }

                obj = (from c in dataList
                       select new object[] {
                       c.Program
                       ,c.Division
                       ,c.District
                       ,c.Item
                       ,c.Quantity
                       ,c.ExpQuantity
                       ,c.ReachCount
                       ,c.ReachCountFemale
                       ,c.ResQuantity
                       ,c.ResReachCount
            }).ToArray();
            }


            JQueryDataTable js = new JQueryDataTable();
            js.sEcho = ec;
            js.iTotalDisplayRecords = count.ToString();
            js.iTotalRecords = js.iTotalDisplayRecords;
            js.aaData = obj;

            return Json(js, JsonRequestBehavior.AllowGet);
        }

    }
}
