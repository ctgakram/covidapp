using AppProj.Domain;
using AppProj.Domain.ModelExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Service.Services
{
    public interface IDoctorsPoleService
    {
        IEnumerable<DoctorsPole> GetPersonal(string pinOrMobile);
        IEnumerable<DoctorsPole> Get(int? sourceId, int? divId, int? disId, DateTime? fromDate, DateTime? toDate, int skip, int take, string byDept , out int count);
        IEnumerable<DoctorsPole> Get(int? effectedTypeId, int? sourceId, int? divId, int? disId, DateTime? fromDate, DateTime? toDate, string dateType, List<int> statusTypeIds, string txt, int skip, int take, out int count);

        IEnumerable<DoctorsPole> GetFollowup(int? effectedTypeId, int? sourceId, int? divId, int? disId, List<int> statusTypeIds, int skip, int take, out int count);
        
        DoctorsPole Get(int Id);


        IEnumerable<DoctorsPoleVisit> GetVisitsByParent(int Id);

        IEnumerable<DoctorPoleCouncilling> GetCouncilByParent(int Id);

        IEnumerable<DoctorsPoleVisitDetail> GetVisitDetailsByParent(int Id);

        DoctorPoleDashboardModel Dashboard(int? sourceId, int minVar);

        void Update(DoctorsPole entity);
        void Add(DoctorsPole entity);
        void UpdateVisit(DoctorsPoleVisit entity);
        void AddVisit(DoctorsPoleVisit entity);

        void AddVisitDetail(DoctorsPoleVisitDetail entity);
        void DeleteVisitDetail(DoctorsPoleVisitDetail entity);
        void DeleteVisitDetailAllByParent(int id);

        void AddCounciling(DoctorPoleCouncilling entity);

        void UpdateKioskQuota(KioskQuota entity);
        KioskQuota GetKiosk(string key, int kioskId);
    }
}
