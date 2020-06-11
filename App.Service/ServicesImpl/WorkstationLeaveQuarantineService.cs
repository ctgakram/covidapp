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
    public class WorkstationLeaveQuarantineService : IWorkstationLeaveQuarantineService
    {
        readonly IWorkstationLeaveQuarantineRepository repository;
        readonly IStandingDataRepository standingRepository;
        readonly IUnitOfWork unitOfWork;

        public WorkstationLeaveQuarantineService(IWorkstationLeaveQuarantineRepository repository
            , IUnitOfWork unitOfWork
            , IStandingDataRepository standingRepository)
        {
            this.repository = repository;
            this.standingRepository = standingRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<WorkstationLeaveQuarantine> Get(int? sourceId, int? divId, int? disId, DateTime? fromDate, DateTime? toDate, int skip, int take, string pin, out int count)
        {
            fromDate = fromDate ?? DateTime.Now;
            toDate = toDate ?? DateTime.Now;

            toDate = toDate.Value.AddDays(1);

            IEnumerable<WorkstationLeaveQuarantine> data = repository.GetMany
                (c => c.EntryTime >= fromDate
                && c.EntryTime <= toDate
                && (sourceId == null ? true : c.SourceId == sourceId)
                && (disId == null ? true : c.DistrictId == disId)
                && (divId == null ? true : c.DivisionId == divId)
                && (pin == null ? true : c.PIN == pin)
                )
                .OrderBy(c => c.EntryTime)
                .Skip(skip).Take(take).ToArray();

            count = repository.GetCount
                (c => c.EntryTime >= fromDate
                && c.EntryTime <= toDate
                && (sourceId == null ? true : c.SourceId == sourceId)
                && (disId == null ? true : c.DistrictId == disId)
                && (divId == null ? true : c.DivisionId == divId)
                && (pin == null ? true : c.PIN == pin)
                );

            return data;
        }

        public IEnumerable<WorkstationLeaveQuarantine> Get(string pin, int skip, int take, out int count)
        {
            IEnumerable<WorkstationLeaveQuarantine> data = repository.GetMany
                (c => (pin == null ? true : c.PIN == pin)
                )
                .OrderBy(c => c.EntryTime)
                .Skip(skip).Take(take).ToArray();

            count = repository.GetCount
                (c => (pin == null ? true : c.PIN == pin)
                );

            return data;
        }

        public WorkstationLeaveQuarantine Get(int Id)
        {
            return repository.GetById(Id);
        }

        public IEnumerable<WorkstationLeaveQuarantine> GetPersonal(string pin)
        {
            return repository.GetMany(c => c.PIN == pin);
        }
        public void Add(WorkstationLeaveQuarantine entity)
        {
            repository.Add(entity);
        }

        public void Update(WorkstationLeaveQuarantine entity)
        {
            repository.Update(entity);
        }

        public void Delete(WorkstationLeaveQuarantine entity)
        {
            repository.Delete(entity);
        }
    }
}
