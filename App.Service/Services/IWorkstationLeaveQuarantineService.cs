using AppProj.Domain;
using AppProj.Domain.ModelExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Service.Services
{
    public interface IWorkstationLeaveQuarantineService
    {
        IEnumerable<WorkstationLeaveQuarantine> GetPersonal(string pin);
        IEnumerable<WorkstationLeaveQuarantine> Get(int? sourceId, int? divId, int? disId, DateTime? fromDate, DateTime? toDate, int skip, int take, string pin, out int count);

        IEnumerable<WorkstationLeaveQuarantine> Get(string pin, int skip, int take, out int count);

        WorkstationLeaveQuarantine Get(int Id);

        void Add(WorkstationLeaveQuarantine entity);
        void Update(WorkstationLeaveQuarantine entity);
        void Delete(WorkstationLeaveQuarantine entity);

    }
}
