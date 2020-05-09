using AppProj.Data.Infrastructure;
using AppProj.Data.Repositories;
using AppProj.Domain;
using AppProj.Domain.ModelExt;
using AppProj.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Service.ServicesImpl
{
    public class BepDataService : IBepDataService
    {
        readonly IBepDataRepository repository;
        readonly IBERDataItemWiseQuantityRepository detailRepository;
        readonly IBERDataPeopleWiseQuantityRepository peopleRepository;
        readonly IUnitOfWork unitOfWork;

        public BepDataService(IBepDataRepository repository
            , IUnitOfWork unitOfWork
            , IBERDataItemWiseQuantityRepository detailRepository
            , IBERDataPeopleWiseQuantityRepository peopleRepository)
        {
            this.repository = repository;
            this.peopleRepository = peopleRepository;
            this.detailRepository = detailRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<BEPData> Get()
        {
            return repository.GetAll();
        }
        public BEPData Get(int id)
        {
            return repository.GetById(id);
        }
        public IEnumerable<BEPData> Get(int? divId, int? disId, int? srcId, DateTime? fromDate, DateTime? toDate, int skip, int take, out int count)
        {
            fromDate = fromDate ?? DateTime.Now;
            toDate = toDate ?? DateTime.Now;

            IEnumerable<BEPData> data = repository.GetMany(c =>
            c.Date >= fromDate && c.Date <= toDate
            && (divId == null ? true : c.DivisionId == divId)
            && (disId == null ? true : c.DistrictId == disId)
            && (srcId == null ? true : c.ProgramId == srcId)
            )
                .OrderBy(c => c.StandingData6.Name)
                .OrderBy(c => c.StandingData2.Name)
                .OrderBy(c => c.StandingData5.Name)
                .ThenBy(c => c.Date)
                .Skip(skip).Take(take).ToArray();

            count = repository.GetCount(c =>
            c.Date >= fromDate && c.Date <= toDate
            && (divId == null ? true : c.DivisionId == divId)
            && (disId == null ? true : c.DistrictId == disId)
            && (srcId == null ? true : c.ProgramId == srcId)
                );

            return data;
        }
        public void Update(BEPData entity)
        {
            repository.Update(entity);
        }
        public void Add(BEPData entity)
        {
            repository.Add(entity);
        }
        public void Delete(BEPData entity)
        {
            repository.Delete(entity);
        }

        public int GetCount()
        {
            return repository.GetCount();
        }

        public IEnumerable<BERDataItemWiseQuantityExt> GetItem(int? divId, int? disId, int? srcId, int? activityId, DateTime? fromDate, DateTime? toDate)
        {
            fromDate = fromDate ?? DateTime.Now;
            toDate = toDate ?? DateTime.Now;

            return detailRepository
                .GetMany(c =>
            c.Date >= fromDate && c.Date <= toDate
            && (divId == null ? true : c.DivisionId == divId)
            && (disId == null ? true : c.DistrictId == disId)
            && (srcId == null ? true : c.ProgramId == srcId)
            && (activityId == null ? true : c.ActivityId == activityId)
            ).GroupBy(q => q.StandingData.Name)
                .Select(g => new BERDataItemWiseQuantityExt
                {
                    Item = g.Key
                    ,
                    ExpQuantity = g.Where(c => c.Type == "PRE").Sum(c => c.ExpQuantity)
                      ,
                    Quantity = g.Where(c => c.Type == "PRE").Sum(c => c.Quantity)
                    ,
                    ReachCount = g.Where(c => c.Type == "PRE").Sum(c => c.ReachCount)
                    ,
                    ResQuantity = g.Where(c => c.Type == "RCH").Sum(c => c.Quantity)
                    ,
                    ResReachCount = g.Where(c => c.Type == "RCH").Sum(c => c.ReachCount)
                }
                ).OrderBy(o => o.Item);
        }

        public IEnumerable<BERDataItemWiseQuantityExt> GetItemDetails(int? divId, int? disId, int? srcId, int? activityId, DateTime? fromDate, DateTime? toDate)
        {
            fromDate = fromDate ?? DateTime.Now;
            toDate = toDate ?? DateTime.Now;

            return detailRepository
                .GetMany(c =>
            c.Date >= fromDate && c.Date <= toDate
            && (divId == null ? true : c.DivisionId == divId)
            && (disId == null ? true : c.DistrictId == disId)
            && (srcId == null ? true : c.ProgramId == srcId)
            && (activityId == null ? true : c.ActivityId == activityId)
            ).GroupBy(q => new
            {
                Item = q.StandingData.Name
            ,
                Program = q.StandingData1.Name
            ,
                Division = q.StandingData2.Name
            ,
                District = q.StandingData3.Name
            })
                .Select(g => new BERDataItemWiseQuantityExt
                {
                    Item = g.Key.Item
                    ,
                    Program = g.Key.Program
                    ,
                    Division = g.Key.Division
                    ,
                    District = g.Key.District
                    ,
                    ExpQuantity = g.Where(c => c.Type == "PRE").Sum(c => c.ExpQuantity)
                      ,
                    Quantity = g.Where(c => c.Type == "PRE").Sum(c => c.Quantity)
                    ,
                    ReachCount = g.Where(c => c.Type == "PRE").Sum(c => c.ReachCount)                    
                    ,
                    ReachCountFemale = g.Where(c => c.Type == "PRE").Sum(c => c.ReachCountFemale)
                    ,
                    ResQuantity = g.Where(c => c.Type == "RCH").Sum(c => c.Quantity)
                    ,
                    ResReachCount = g.Where(c => c.Type == "RCH").Sum(c => c.ReachCount)
                    
                }
                ).OrderBy(o => o.Program)
                .ThenBy(o => o.Division)
                .ThenBy(o => o.District)
                .ThenBy(o => o.Item);
        }

        public IEnumerable<BERDataItemWiseQuantity> GetItem(int bepDataId, string type)
        {
            return detailRepository.GetMany(c => c.BEPDataId == bepDataId && c.Type == type);
        }
        public void UpdateItem(BERDataItemWiseQuantity entity)
        {
            detailRepository.Update(entity);
        }
        public void AddItem(BERDataItemWiseQuantity entity)
        {
            detailRepository.Add(entity);
        }
        public BERDataItemWiseQuantity GetSingleItem(int id)
        {
            return detailRepository.GetById(id);
        }
        public void DeleteItem(BERDataItemWiseQuantity entity)
        {
            detailRepository.Delete(entity);
        }


        public IEnumerable<BERDataItemWiseQuantityExt> GetPeople(int? divId, int? disId, int? srcId, int? activityId, DateTime? fromDate, DateTime? toDate)
        {
            fromDate = fromDate ?? DateTime.Now;
            toDate = toDate ?? DateTime.Now;

            return peopleRepository
                .GetMany(c =>
            c.Date >= fromDate && c.Date <= toDate
            && (divId == null ? true : c.DivisionId == divId)
            && (disId == null ? true : c.DistrictId == disId)
            && (srcId == null ? true : c.ProgramId == srcId)
            && (activityId == null ? true : c.ActivityId == activityId)
            ).GroupBy(q => q.StandingData3.Name)
                .Select(g => new BERDataItemWiseQuantityExt
                {
                    Activity = g.Key
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

                }
                ).OrderBy(o => o.Activity);
        }

        public IEnumerable<BERDataItemWiseQuantityExt> GetPeopleDetail(int? divId, int? disId, int? srcId, int? activityId, DateTime? fromDate, DateTime? toDate)
        {
            fromDate = fromDate ?? DateTime.Now;
            toDate = toDate ?? DateTime.Now;

            return peopleRepository
                .GetMany(c =>
            c.Date >= fromDate && c.Date <= toDate
            && (divId == null ? true : c.DivisionId == divId)
            && (disId == null ? true : c.DistrictId == disId)
            && (srcId == null ? true : c.ProgramId == srcId)
            && (activityId == null ? true : c.ActivityId == activityId)
            ).GroupBy(q => new
            {
                Activity = q.StandingData3.Name
                ,
                Program = q.StandingData.Name
                ,
                Division = q.StandingData1.Name
            ,
                District = q.StandingData2.Name
            })
                .Select(g => new BERDataItemWiseQuantityExt
                {
                    Activity = g.Key.Activity
                    ,
                    Program = g.Key.Program
                    ,
                    Division = g.Key.Division
                     ,
                    District = g.Key.District
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
                }
                )
                .OrderBy(o=> o.Program)
                .ThenBy(o => o.Division)
                .ThenBy(o => o.District)
                .ThenBy(o => o.Activity);
        }

        public IEnumerable<BERDataPeopleWiseQuantity> GetPeople(int bepDataId, string type)
        {
            return peopleRepository.GetMany(c => c.BEPDataId == bepDataId && c.Type == type);
        }
        public void UpdatePeople(BERDataPeopleWiseQuantity entity)
        {
            peopleRepository.Update(entity);
        }
        public void AddPeople(BERDataPeopleWiseQuantity entity)
        {
            peopleRepository.Add(entity);
        }
        public BERDataPeopleWiseQuantity GetSinglePeople(int id)
        {
            return peopleRepository.GetById(id);
        }
        public void DeletePeople(BERDataPeopleWiseQuantity entity)
        {
            peopleRepository.Delete(entity);
        }


        //Dashboard

        public List<BepDataSummeryModelReach> GetReachForDashboard()
        {
            List<BepDataSummeryModelReach> model = peopleRepository
                .GetAll().GroupBy(q => new
            {
                Activity = q.StandingData3.Name
            })
                .Select(g => new BepDataSummeryModelReach
                {
                    Activity = g.Key.Activity
                    ,
                    Male = g.Sum(c => c.Cat1NewReach)
                    //,
                    //Cat1OldReach = g.Sum(c => c.Cat1OldReach)
                    ,
                    Female = g.Sum(c => c.Cat2NewReach)
                    //,
                    //Cat2OldReach = g.Sum(c => c.Cat2OldReach)
                    ,
                    Boy = g.Sum(c => c.Cat3NewReach)
                    //,
                    //Cat3OldReach = g.Sum(c => c.Cat3OldReach)
                    ,
                    Girl = g.Sum(c => c.Cat4NewReach)
                    //,
                    //Cat4OldReach = g.Sum(c => c.Cat4OldReach)
                    ,
                    PWD = g.Sum(c => c.Cat5NewReach)
                    //,
                    //Cat5OldReach = g.Sum(c => c.Cat5OldReach)
                    ,
                    HHS = g.Sum(c => c.Cat6NewReach)
                    //,
                    //Cat6OldReach = g.Sum(c => c.Cat6OldReach)
                    // ,
                    //Cat7NewReach = g.Sum(c => c.Cat7NewReach)
                    //,
                    //Cat7OldReach = g.Sum(c => c.Cat7OldReach)
                     ,
                    Pregnant = g.Sum(c => c.Cat8NewReach)
                    //,
                    //Cat8OldReach = g.Sum(c => c.Cat8OldReach)
                }
                )
                .OrderBy(o => o.Activity)
                .ToList();

            return model;
        }

        public List<BepDataSummeryModelMaterial> GetMaterialForDashboard()
        {
            List<BepDataSummeryModelMaterial> model = detailRepository
               .GetAll().GroupBy(q => new
               {
                   Item = q.StandingData.Name
               })
               .Select(g => new BepDataSummeryModelMaterial
               {
                   Item = g.Key.Item
                   ,
                   Qnt = g.Where(c => c.Type == "PRE").Sum(c => c.Quantity)
                     ,
                   ExpQnt = g.Where(c => c.Type == "PRE").Sum(c => c.ExpQuantity)
                   ,
                   Male = g.Where(c => c.Type == "PRE").Sum(c => c.ReachCount)
                   ,
                   Female = g.Where(c => c.Type == "PRE").Sum(c => c.ReachCountFemale)
               }
               ).OrderBy(o => o.Item)
               .ToList();

            return model;
        }

        public List<BepDataSummeryModelMaterial> GetDistributionForDashboard()
        {
            List<BepDataSummeryModelMaterial> model = detailRepository
                .GetAll().GroupBy(q => new
                {
                    Item = q.StandingData.Name
                    ,
                    Activity =q.StandingData4.Name
                })
                .Select(g => new BepDataSummeryModelMaterial
                {
                    Item = g.Key.Item
                    ,
                    Activity=g.Key.Activity
                    ,
                    Qnt = g.Where(c => c.Type == "RCH").Sum(c => c.Quantity)                     
                    ,
                    Reach = g.Where(c => c.Type == "RCH").Sum(c => c.ReachCount)                    
                }
                ).OrderBy(o => o.Activity)
                .ThenBy(o => o.Item)
                .ToList();

            return model;
        }

        public BepDataSummeryModelFixedMaterial GetDistributionFixedMaterialForDashboard()
        {
            BepDataSummeryModelFixedMaterial model = detailRepository
                .GetAll().GroupBy(q => 1 == 1)
                .Select(g => new BepDataSummeryModelFixedMaterial
                {
                    Festun = g.Where(c => c.ItemId == 860600).Sum(c => c.Quantity)
                    ,
                    Aproan = g.Where(c => c.ItemId == 860692).Sum(c => c.Quantity)
                     ,
                    Gloves = g.Where(c => c.ItemId == 860573).Sum(c => c.Quantity)
                    ,
                    Mask = g.Where(c => c.ItemId == 860571).Sum(c => c.Quantity)
                    ,
                    Miking = g.Where(c => c.ItemId == 860687).Sum(c => c.Quantity)
                    ,
                    Sanitizer = g.Where(c => c.ItemId == 860572).Sum(c => c.Quantity)
                }
                ).Single();

            return model;
        }
    }
}
