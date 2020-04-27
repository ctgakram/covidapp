using AppProj.Domain;
using AppProj.Domain.ModelExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Service.Services
{
    public interface IBepDataService
    {
        IEnumerable<BEPData> Get();
        BEPData Get(int id);
        IEnumerable<BEPData> Get(int? divId, int? disId, int? srcId, DateTime? fromDate, DateTime? toDate, int skip, int take, out int count);        
        void Update(BEPData entity);
        void Add(BEPData entity);
        void Delete(BEPData entity);
        int GetCount();

        IEnumerable<BERDataItemWiseQuantityExt> GetItem(int? divId, int? disId, int? srcId,int? activityId ,DateTime? fromDate, DateTime? toDate);
        IEnumerable<BERDataItemWiseQuantityExt> GetItemDetails(int? divId, int? disId, int? srcId, int? activityId, DateTime? fromDate, DateTime? toDate);

        //IEnumerable<BERDataItemWiseQuantity> GetItem(int divId, int disId, int srcId, DateTime date, string type);
        IEnumerable<BERDataItemWiseQuantity> GetItem(int bepDataId, string type);
        void UpdateItem(BERDataItemWiseQuantity entity);
        void AddItem(BERDataItemWiseQuantity entity);
        BERDataItemWiseQuantity GetSingleItem(int id);
        void DeleteItem(BERDataItemWiseQuantity entity);
        
        IEnumerable<BERDataItemWiseQuantityExt> GetPeople(int? divId, int? disId, int? srcId, int? activityId, DateTime? fromDate, DateTime? toDate);
        IEnumerable<BERDataItemWiseQuantityExt> GetPeopleDetail(int? divId, int? disId, int? srcId, int? activityId, DateTime? fromDate, DateTime? toDate);

        IEnumerable<BERDataPeopleWiseQuantity> GetPeople(int bepDataId,string type);
        void UpdatePeople(BERDataPeopleWiseQuantity entity);
        void AddPeople(BERDataPeopleWiseQuantity entity);
        BERDataPeopleWiseQuantity GetSinglePeople(int id);
        void DeletePeople(BERDataPeopleWiseQuantity entity);

        //dashboard
        List<BepDataSummeryModelReach> GetReachForDashboard();
        List<BepDataSummeryModelMaterial> GetMaterialForDashboard();
        List<BepDataSummeryModelMaterial> GetDistributionForDashboard();

    }
}
