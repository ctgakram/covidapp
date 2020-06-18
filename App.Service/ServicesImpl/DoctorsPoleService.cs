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
    public class DoctorsPoleService : IDoctorsPoleService
    {
        readonly IDoctorsPolesRepository repository;
        readonly IDoctorsPoleVisitRepository visitRepository;
        readonly IDoctorsPoleVisitDetailRepository visitDetailRepository;
        readonly IDoctorPoleCouncillingRepository councilingRepository;
        readonly IDoctorPoleStatusesRepository repositoryStatus;
        readonly IStandingDataRepository standingRepository;
        readonly IUnitOfWork unitOfWork;

        public DoctorsPoleService(IDoctorsPolesRepository repository
            , IUnitOfWork unitOfWork
            , IDoctorsPoleVisitRepository visitRepository
            , IDoctorsPoleVisitDetailRepository visitDetailRepository
            , IDoctorPoleCouncillingRepository councilingRepository
            , IDoctorPoleStatusesRepository repositoryStatus
            , IStandingDataRepository standingRepository)
        {
            this.repository = repository;
            this.standingRepository = standingRepository;
            this.visitRepository = visitRepository;
            this.visitDetailRepository = visitDetailRepository;
            this.councilingRepository = councilingRepository;
            this.repositoryStatus = repositoryStatus;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<DoctorsPole> GetPersonal(string pinOrMobile)
        {
            return repository.GetMany(c => c.PIN == pinOrMobile || c.MobileNo == pinOrMobile);
        }

        public IEnumerable<DoctorsPole> Get(int? sourceId, int? divId, int? disId, DateTime? fromDate, DateTime? toDate, int skip, int take, string byDept, out int count)
        {
            fromDate = fromDate ?? DateTime.Now;
            toDate = toDate ?? DateTime.Now;

            toDate = toDate.Value.AddDays(1);

            IEnumerable<DoctorsPole> data = repository.GetMany
                (c => c.EntryByDept == byDept
                && c.EntryTime >= fromDate
                && c.EntryTime <= toDate
                && (sourceId == null ? true : c.ProgramId == sourceId)
                && (disId == null ? true : c.DistrictId == disId)
                && (divId == null ? true : c.DivisionId == divId)
                )
                //.OrderBy(c => c.StandingData.Name)
                //.ThenBy(c => c.StandingData1.Name)
                //.ThenBy(c => c.StandingData3.Name)
                .OrderBy(c => c.EntryTime)
                .Skip(skip).Take(take).ToArray();

            count = repository.GetCount
                (c => c.EntryByDept == byDept
                && c.EntryTime >= fromDate
                && c.EntryTime <= toDate
                && (sourceId == null ? true : c.ProgramId == sourceId)
                && (disId == null ? true : c.DistrictId == disId)
                && (divId == null ? true : c.DivisionId == divId)
                );

            return data;
        }

        public IEnumerable<DoctorsPole> Get(int? effectedTypeId, int? sourceId, int? divId, int? disId, DateTime? fromDate, DateTime? toDate, string dateType, List<int> statusTypeIds, string txt, int skip, int take, out int count)
        {
            fromDate = fromDate ?? DateTime.Now;
            toDate = toDate ?? DateTime.Now;
            IEnumerable<DoctorsPole> data = null;
            toDate = toDate.Value.AddDays(1);

            if (string.IsNullOrEmpty(txt))
            {
                data = repository.GetMany(c =>
                       (dateType == "L" ? (c.LastFollowupDate >= fromDate && c.LastFollowupDate < toDate) : true)
                    && (dateType == "N" ? (c.NextFollowupDate >= fromDate && c.NextFollowupDate < toDate) : true)
                    && (dateType == "E" ? (c.EntryTime >= fromDate && c.EntryTime < toDate) : true)
                    //dateType == "A" always true
                    && statusTypeIds.Contains(c.StatusId)
                    && (sourceId == null ? true : c.ProgramId == sourceId)
                    && (disId == null ? true : c.DistrictId == disId)
                    && (divId == null ? true : c.DivisionId == divId)
                    && (effectedTypeId == null ? true : c.EffectedPersonId == effectedTypeId)
                    )
                    //.OrderBy(c => c.StandingData.Name)
                    //.ThenBy(c => c.StandingData1.Name)
                    //.ThenBy(c => c.StandingData3.Name)
                    .OrderBy(c => c.EntryTime)
                    .Skip(skip).Take(take).ToArray();

                count = repository.GetCount(c =>
                       (dateType == "L" ? (c.LastFollowupDate >= fromDate && c.LastFollowupDate < toDate) : true)
                    && (dateType == "N" ? (c.NextFollowupDate >= fromDate && c.NextFollowupDate < toDate) : true)
                    && (dateType == "E" ? (c.EntryTime >= fromDate && c.EntryTime < toDate) : true)
                    && statusTypeIds.Contains(c.StatusId)
                    && (sourceId == null ? true : c.ProgramId == sourceId)
                    && (disId == null ? true : c.DistrictId == disId)
                    && (divId == null ? true : c.DivisionId == divId)
                    && (effectedTypeId == null ? true : c.EffectedPersonId == effectedTypeId)
                    );
            }
            else
            {
                data = repository.GetMany(c =>
                       (c.PIN == txt || c.MobileNo == txt)
                    && (sourceId == null ? true : c.ProgramId == sourceId)
                    && (disId == null ? true : c.DistrictId == disId)
                    && (divId == null ? true : c.DivisionId == divId)
                    && (effectedTypeId == null ? true : c.EffectedPersonId == effectedTypeId)
                    )
                    //.OrderBy(c => c.StandingData.Name)
                    //.ThenBy(c => c.StandingData1.Name)
                    //.ThenBy(c => c.StandingData3.Name)
                    .OrderBy(c => c.EntryTime)
                    .Skip(skip).Take(take).ToArray();

                count = repository.GetCount(c =>
                       (c.PIN == txt || c.MobileNo == txt)
                    && (sourceId == null ? true : c.ProgramId == sourceId)
                    && (disId == null ? true : c.DistrictId == disId)
                    && (divId == null ? true : c.DivisionId == divId)
                    && (effectedTypeId == null ? true : c.EffectedPersonId == effectedTypeId)
                    );
            }

            return data;
        }

        public IEnumerable<DoctorsPole> GetFollowup(int? effectedTypeId, int? sourceId, int? divId, int? disId, List<int> statusTypeIds, int skip, int take, out int count)
        {
            DateTime dt = DateTime.Now;


            IEnumerable<DoctorsPole> data = repository.GetMany(c =>
                  (c.NextFollowupDate <= dt || c.NextFollowupDate == null)
               && statusTypeIds.Contains(c.StatusId)
               && (sourceId == null ? true : c.ProgramId == sourceId)
               && (disId == null ? true : c.DistrictId == disId)
               && (divId == null ? true : c.DivisionId == divId)
               && (effectedTypeId == null ? true : c.EffectedPersonId == effectedTypeId)
                )
                //.OrderBy(c => c.StandingData.Name)
                //.ThenBy(c => c.StandingData1.Name)
                //.ThenBy(c => c.StandingData3.Name)
                .OrderBy(c => c.EntryTime)
                .Skip(skip).Take(take).ToArray();

            count = repository.GetCount(c =>
                (c.NextFollowupDate <= dt || c.NextFollowupDate == null)
                && statusTypeIds.Contains(c.StatusId)
                && (sourceId == null ? true : c.ProgramId == sourceId)
                && (disId == null ? true : c.DistrictId == disId)
                && (divId == null ? true : c.DivisionId == divId)
                && (effectedTypeId == null ? true : c.EffectedPersonId == effectedTypeId)
                );

            return data;
        }

        public DoctorsPole Get(int Id)
        {
            return repository.GetById(Id);
        }


        public IEnumerable<DoctorsPoleVisit> GetVisitsByParent(int Id)
        {
            return visitRepository.GetMany(c => c.DoctorPoleId == Id);
        }

        public IEnumerable<DoctorPoleCouncilling> GetCouncilByParent(int Id)
        {
            return councilingRepository.GetMany(c => c.DoctorPoleId == Id);
        }
        public IEnumerable<DoctorsPoleVisitDetail> GetVisitDetailsByParent(int Id)
        {
            return visitDetailRepository.GetMany(c => c.DoctorsPoleVisitId == Id);
        }

        public DoctorPoleDashboardModel Dashboard(int? sourceId, int minVar)
        {
            List<int> suspectedTypes = new List<int> { 1, 2, 3, 4 };
            decimal tmp = 0;

            DoctorPoleDashboardModel model = new DoctorPoleDashboardModel();

            var suspectedIds = repositoryStatus.GetMany(c =>
            suspectedTypes.Contains(c.StandingData.IntValue.Value)
            && (sourceId == null ? true : (sourceId == c.DoctorsPole.ProgramId))
            ).Select(c=> c.DoctorPoleId).ToList();

            var suspectedData = repository.GetMany(c => suspectedIds.Contains(c.Id))
                .ToList();

            var totalPositive = suspectedData.Where(c => c.StandingData6.IntValue == 2);
            var totalNegetive = suspectedData.Where(c => c.StandingData6.IntValue == 3);
            var totalResolved = suspectedData.Where(c => c.StandingData6.IntValue == 4);

            var currentPositive = repository.GetMany(c =>
            c.StandingData6.IntValue.Value == 2
            && (sourceId == null ? true : (sourceId == c.ProgramId))
            ).ToList();


            model.TotalCases = suspectedData.Count();

            model.TotalPositive = totalPositive.Count();

            model.TotalNegetive = totalNegetive.Count();

            model.TotalResolved = totalResolved.Count();

            model.TotalTestPending = suspectedData.Where(c => c.SampleTakenDate != null 
            && c.TestResultId == null).Count();

            model.TotalHomeIsolation = currentPositive
                .Where(c => c.StandingData8 == null ? false : c.StandingData8.IntValue == 1)
                .Count();

            model.TotalHospitalized = currentPositive
                .Where(c => c.StandingData8 == null ? false : c.StandingData8.IntValue == 2)
                .Count();

            model.CurrentPositive = currentPositive.Count();

            //suspected

            model.ProgramWiseCases = suspectedData                
                .GroupBy(c => c.StandingData1.Name)
            .Select(s => new DoctorPoleDashboardDetailModel
            {
                Particular = s.Key
                ,
                Value = s.Count()
            })
            .OrderBy(o => o.Particular)
            .ToList();

            tmp = model.ProgramWiseCases.Sum(c => c.Value);
            if (tmp > 0)
            {
                model.ProgramWiseCases.ForEach(c => c.Percent = (c.Value * 100) / tmp);
            }

            model.SexWiseCases = suspectedData.GroupBy(c => c.StandingData.Name)
           .Select(s => new DoctorPoleDashboardDetailModel
           {
               Particular = s.Key
               ,
               Value = s.Count()
           })
           .OrderBy(o => o.Particular)
           .ToList();

            tmp = model.SexWiseCases.Sum(c => c.Value);
            if (tmp > 0)
            {
                model.SexWiseCases.ForEach(c => c.Percent = (c.Value * 100) / tmp);
            }

            model.AgeWiseCases = suspectedData.GroupBy(c =>
            c.Age <= 30 ? "Upto 30"
            : (c.Age > 30 && c.Age <= 40 ? "31-40"
            : (c.Age > 40 && c.Age <= 50 ? "41-50" : "50+")
            ))
           .Select(s => new DoctorPoleDashboardDetailModel
           {
               Particular = s.Key
               ,
               Value = s.Count()
           })
           .OrderBy(o => o.Particular)
           .ToList();

            tmp = model.AgeWiseCases.Sum(c => c.Value);
            if (tmp > 0)
            {
                model.AgeWiseCases.ForEach(c => c.Percent = (c.Value * 100) / tmp);
            }


            model.DistrictWiseCases = suspectedData.GroupBy(c => c.StandingData3.Name)
           .Select(s => new DoctorPoleDashboardDetailModel
           {
               Particular = s.Key
               ,
               Value = s.Count()
           })
           .OrderBy(o => o.Particular)
           .ToList();

            tmp = model.DistrictWiseCases.Sum(c => c.Value);
            if (tmp > 0)
            {
                model.DistrictWiseCases.ForEach(c => c.Percent = (c.Value * 100) / tmp);
            }

            model.DistrictWiseCases_MinVariable = model.DistrictWiseCases
                .Where(c => c.Value >= minVar)
                .ToList();

            
            //current positive

            model.ProgramWiseCurrentPositiveCases = currentPositive.GroupBy(c => c.StandingData1.Name)
            .Select(s => new DoctorPoleDashboardDetailModel
            {
                Particular = s.Key
                ,
                Value = s.Count()
            })
            .OrderBy(o => o.Particular)
            .ToList();

            tmp = model.ProgramWiseCurrentPositiveCases.Sum(c => c.Value);
            if (tmp > 0)
            {
                model.ProgramWiseCurrentPositiveCases.ForEach(c => c.Percent = (c.Value * 100) / tmp);
            }

            model.SexWiseCurrentPositiveCases = currentPositive.GroupBy(c => c.StandingData.Name)
           .Select(s => new DoctorPoleDashboardDetailModel
           {
               Particular = s.Key
               ,
               Value = s.Count()
           })
           .OrderBy(o => o.Particular)
           .ToList();

            tmp = model.SexWiseCurrentPositiveCases.Sum(c => c.Value);
            if (tmp > 0)
            {
                model.SexWiseCurrentPositiveCases.ForEach(c => c.Percent = (c.Value * 100) / tmp);
            }

            model.AgeWiseCurrentPositiveCases = currentPositive.GroupBy(c =>
            c.Age <= 30 ? "Upto 30"
            : (c.Age > 30 && c.Age <= 40 ? "31-40"
            : (c.Age > 40 && c.Age <= 50 ? "41-50" : "50+")
            ))
           .Select(s => new DoctorPoleDashboardDetailModel
           {
               Particular = s.Key
               ,
               Value = s.Count()
           })
           .OrderBy(o => o.Particular)
           .ToList();

            tmp = model.AgeWiseCurrentPositiveCases.Sum(c => c.Value);
            if (tmp > 0)
            {
                model.AgeWiseCurrentPositiveCases.ForEach(c => c.Percent = (c.Value * 100) / tmp);
            }


            model.DistrictWiseCurrentPositiveCases = currentPositive.GroupBy(c => c.StandingData3.Name)
           .Select(s => new DoctorPoleDashboardDetailModel
           {
               Particular = s.Key
               ,
               Value = s.Count()
           })
           .OrderBy(o => o.Particular)
           .ToList();

            tmp = model.DistrictWiseCurrentPositiveCases.Sum(c => c.Value);
            if (tmp > 0)
            {
                model.DistrictWiseCurrentPositiveCases.ForEach(c => c.Percent = (c.Value * 100) / tmp);
            }




            return model;
        }

        public void Update(DoctorsPole entity)
        {
            repository.Update(entity);

            UpdateStatus(entity.Id, entity.StatusId);
        }
        public void Add(DoctorsPole entity)
        {
            repository.Add(entity);

            UpdateStatus(entity.Id, entity.StatusId);
        }


        public void UpdateVisit(DoctorsPoleVisit entity)
        {
            visitRepository.Update(entity);

            UpdateStatus(entity.DoctorPoleId, entity.StatusId);
        }
        public void AddVisit(DoctorsPoleVisit entity)
        {
            visitRepository.Add(entity);

            UpdateStatus(entity.DoctorPoleId, entity.StatusId);
        }

        public void AddVisitDetail(DoctorsPoleVisitDetail entity)
        {
            visitDetailRepository.Add(entity);
        }
        public void DeleteVisitDetail(DoctorsPoleVisitDetail entity)
        {
            visitDetailRepository.Delete(entity);
        }
        public void DeleteVisitDetailAllByParent(int id)
        {
            var v = visitDetailRepository.GetMany(c => c.DoctorsPoleVisitId == id);

            foreach (var r in v)
            {
                visitDetailRepository.Delete(r);
            }
        }

        public void AddCounciling(DoctorPoleCouncilling entity)
        {
            councilingRepository.Add(entity);
        }

        void UpdateStatus(int id, int statId)
        {
            var v = repositoryStatus.Get(c => c.DoctorPoleId == id && c.StatusId == statId);

            if (v == null)
            {
                repositoryStatus.Add(new DoctorPoleStatus { DoctorPoleId = id, StatusId = statId });
            }
        }
    }
}
