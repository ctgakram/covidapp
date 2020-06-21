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
                .OrderByDescending(c => c.EntryTime)
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
                    .OrderByDescending(c => c.EntryTime)
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
                    .OrderByDescending(c => c.EntryTime)
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
                .OrderBy(c => c.NextFollowupDate)
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
            List<int> suspectedTypes = new List<int> { 1, 2, 3, 4, 5, 7 };
            decimal tmp = 0;
            int staff = 861692; //staff effected person

            DoctorPoleDashboardModel model = new DoctorPoleDashboardModel();

            var totals = repositoryStatus.GetMany(c =>
            suspectedTypes.Contains(c.StandingData.IntValue.Value)
            && (sourceId == null ? true : (sourceId == c.DoctorsPole.ProgramId))
            && c.DoctorsPole.EffectedPersonId == staff
            ).ToList();
            //.Select(c=> c.DoctorPoleId).ToList();

            var totalIds = totals.Select(c => c.DoctorPoleId);

            //var totalPosIds= totals.Where(c=> c.StandingData.IntValue==3).Select(c => c.DoctorPoleId);
            //var totalNegIds = totals.Where(c => c.StandingData.IntValue == 4).Select(c => c.DoctorPoleId);
            //var totalResIds = totals.Where(c => c.StandingData.IntValue == 5).Select(c => c.DoctorPoleId);
            //var totalDeathIds = totals.Where(c => c.StandingData.IntValue == 7).Select(c => c.DoctorPoleId);

            

            var suspectedData = repository.GetMany(c => totalIds.Contains(c.Id))
                .ToList();

            //var totalPositive = suspectedData.Where(c => c.StandingData8.IntValue == 3);
            //var totalNegetive = suspectedData.Where(c => c.StandingData8.IntValue == 4);
            //var totalResolved = suspectedData.Where(c => c.StandingData8.IntValue == 5);

            var currentData = repository.GetMany(c =>
            c.EffectedPersonId == staff
            && (sourceId == null ? true : (sourceId == c.ProgramId))
            );

            var currentPositive = currentData.Where(c => c.StandingData8.IntValue.Value == 3).ToList();
            
            model.TotalCases = suspectedData.Count();

            model.CurrentPositive = currentPositive.Count();

            model.TotalNegetive = currentData.Where(c => c.StandingData8.IntValue.Value == 4).Count();

            model.TotalResolved = currentData.Where(c => c.StandingData8.IntValue.Value == 5).Count();

            model.TotalDeath = currentData.Where(c => c.StandingData8.IntValue.Value == 7).Count();

            model.TotalPositive = model.CurrentPositive + model.TotalNegetive + model.TotalResolved + model.TotalDeath;

            model.TotalCall = visitRepository.GetAll().Count();

            model.TotalTestPending = suspectedData.Where(c =>
            ((c.SampleTakenDate != null && c.TestResultId == null) || c.TestResultId == 861717)
            || ((c.SampleTakenDate1 != null && c.TestResultId1 == null) || c.TestResultId == 861717)
            || ((c.SampleTakenDate2 != null && c.TestResultId2 == null) || c.TestResultId == 861717)
            ).Count();

            model.TotalHomeIsolation = currentPositive
                .Where(c => c.StandingData12 == null ? false : c.StandingData12.IntValue == 1)
                .Count();

            model.TotalHospitalized = currentPositive
                .Where(c => c.StandingData12 == null ? false : c.StandingData12.IntValue == 2)
                .Count();

            

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


            model.DistrictWiseCases = suspectedData.GroupBy(c => c.StandingData6.Name)
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


            model.DistrictWiseCurrentPositiveCases = currentPositive.GroupBy(c => c.StandingData6.Name)
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
            var v = repositoryStatus
                .GetMany(c => c.DoctorPoleId == id && c.StatusId == statId)
                .Count();

            if (v <= 0)
            {
                repositoryStatus.Add(new DoctorPoleStatus { DoctorPoleId = id, StatusId = statId, StartDate = DateTime.Now });
            }
        }
    }
}
