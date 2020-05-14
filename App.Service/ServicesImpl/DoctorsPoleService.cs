using AppProj.Data.Infrastructure;
using AppProj.Data.Repositories;
using AppProj.Domain;
using AppProj.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Service.ServicesImpl
{
    public class DoctorsPoleService: IDoctorsPoleService
    {
        readonly IDoctorsPolesRepository repository;
        readonly IDoctorsPoleVisitRepository visitRepository;
        readonly IStandingDataRepository standingRepository;
        readonly IUnitOfWork unitOfWork;

        public DoctorsPoleService(IDoctorsPolesRepository repository
            , IUnitOfWork unitOfWork
            , IDoctorsPoleVisitRepository visitRepository
            , IStandingDataRepository standingRepository)
        {
            this.repository = repository;
            this.standingRepository = standingRepository;
            this.visitRepository = visitRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<DoctorsPole> GetPersonal(string pin)
        {
            return repository.GetMany(c => c.PIN == pin);
        }

        public IEnumerable<DoctorsPole> Get(int? sourceId, int? divId, int? disId, DateTime? fromDate, DateTime? toDate, int skip, int take, string byDept, out int count)
        {
            fromDate = fromDate ?? DateTime.Now;
            toDate = toDate ?? DateTime.Now;

            toDate = toDate.Value.AddDays(1);

            IEnumerable<DoctorsPole> data = repository.GetMany
                (c => c.ProgramId != 860538 
                && c.EntryByDept==byDept
                && c.EntryTime >= fromDate 
                && c.EntryTime <= toDate                
                && (sourceId == null ? true : c.ProgramId == sourceId)
                && (disId == null ? true : c.DistrictId == disId)
                && (divId == null ? true : c.DivisionId == divId)
                )
                .OrderBy(c => c.StandingData.Name)
                .ThenBy(c => c.StandingData1.Name)
                .ThenBy(c => c.StandingData2.Name)
                .ThenBy(c => c.EntryTime)
                .Skip(skip).Take(take).ToArray();

            count = repository.GetCount
                (c => c.ProgramId != 860538
                && c.EntryByDept == byDept
                && c.EntryTime >= fromDate 
                && c.EntryTime <= toDate
                && (sourceId == null ? true : c.ProgramId == sourceId)
                && (disId == null ? true : c.DistrictId == disId)
                && (divId == null ? true : c.DivisionId == divId)
                );

            return data;
        }

        public IEnumerable<DoctorsPole> Get(int? sourceId, int? divId, int? disId, DateTime? fromDate, DateTime? toDate, int skip, int take, out int count)
        {
            fromDate = fromDate ?? DateTime.Now;
            toDate = toDate ?? DateTime.Now;

            toDate = toDate.Value.AddDays(1);

            IEnumerable<DoctorsPole> data = repository.GetMany
                (c => c.ProgramId != 860538
                && c.FirstDoctorCallTime != null
                && c.EntryTime >= fromDate
                && c.EntryTime <= toDate
                && (sourceId == null ? true : c.ProgramId == sourceId)
                && (disId == null ? true : c.DistrictId == disId)
                && (divId == null ? true : c.DivisionId == divId)
                )
                .OrderBy(c => c.StandingData.Name)
                .ThenBy(c => c.StandingData1.Name)
                .ThenBy(c => c.StandingData2.Name)
                .ThenBy(c => c.EntryTime)
                .Skip(skip).Take(take).ToArray();

            count = repository.GetCount
                (c => c.ProgramId != 860538
                && c.FirstDoctorCallTime != null
                && c.EntryTime >= fromDate
                && c.EntryTime <= toDate
                && (sourceId == null ? true : c.ProgramId == sourceId)
                && (disId == null ? true : c.DistrictId == disId)
                && (divId == null ? true : c.DivisionId == divId)
                );

            return data;
        }
        public IEnumerable<DoctorsPole> Get(int? sourceId, int? divId, int? disId, int skip, int take, out int count)
        {
            DateTime dt = DateTime.Now;

            IEnumerable<DoctorsPole> data = repository.GetMany
                (c => c.ProgramId != 860538
                && (c.NextFollowupDate <= dt || c.NextFollowupDate == null)
                && (sourceId == null ? true : c.ProgramId == sourceId)
                && (disId == null ? true : c.DistrictId == disId)
                && (divId == null ? true : c.DivisionId == divId)
                )
                .OrderBy(c => c.StandingData.Name)
                .ThenBy(c => c.StandingData1.Name)
                .ThenBy(c => c.StandingData2.Name)
                .ThenBy(c => c.EntryTime)
                .Skip(skip).Take(take).ToArray();

            count = repository.GetCount
                (c => c.ProgramId != 860538
                 && (c.NextFollowupDate <= dt || c.NextFollowupDate == null)
                && (sourceId == null ? true : c.ProgramId == sourceId)
                && (disId == null ? true : c.DistrictId == disId)
                && (divId == null ? true : c.DivisionId == divId)
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

        public void Update(DoctorsPole entity)
        {
            repository.Update(entity);
        }
        public void Add(DoctorsPole entity)
        {
            repository.Add(entity);
        }


        public void UpdateVisit(DoctorsPoleVisit entity)
        {
            visitRepository.Update(entity);
        }
        public void AddVisit(DoctorsPoleVisit entity)
        {
            visitRepository.Add(entity);
        }
    }
}
