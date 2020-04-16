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
                    ExpQuantity = g.Where(c=> c.Type== "PRE").Sum(c => c.ExpQuantity)
                      ,
                    Quantity = g.Where(c => c.Type == "PRE").Sum(c => c.Quantity)
                    ,
                    ReachCount = g.Where(c => c.Type == "PRE").Sum(c => c.ReachCount)
                    ,
                    ResQuantity = g.Where(c => c.Type == "RCH").Sum(c => c.Quantity)
                    ,
                    ResReachCount = g.Where(c => c.Type == "RCH").Sum(c => c.ReachCount)
                }
                );
        }
        
        public IEnumerable<BERDataItemWiseQuantity> GetItem(int bepDataId, string type)
        {
            return detailRepository.GetMany(c => c.BEPDataId == bepDataId && c.Type==type);
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
                );
        }
        public IEnumerable<BERDataPeopleWiseQuantity> GetPeople(int bepDataId, string type)
        {
            return peopleRepository.GetMany(c => c.BEPDataId == bepDataId && c.Type==type);
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
    }
}
