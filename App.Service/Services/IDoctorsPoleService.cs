using AppProj.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Service.Services
{
    public interface IDoctorsPoleService
    {
        IEnumerable<DoctorsPole> GetPersonal(string pin);
        IEnumerable<DoctorsPole> Get(int? sourceId, int? divId, int? disId, DateTime? fromDate, DateTime? toDate, int skip, int take, string byDept , out int count);
        IEnumerable<DoctorsPole> Get(int? sourceId, int? divId, int? disId,  int skip, int take, out int count);
        IEnumerable<DoctorsPole> Get(int? sourceId, int? divId, int? disId, DateTime? fromDate, DateTime? toDate, int skip, int take, out int count);

        DoctorsPole Get(int Id);


        IEnumerable<DoctorsPoleVisit> GetVisitsByParent(int Id);

        void Update(DoctorsPole entity);
        void Add(DoctorsPole entity);
        void UpdateVisit(DoctorsPoleVisit entity);
        void AddVisit(DoctorsPoleVisit entity);
    }
}
