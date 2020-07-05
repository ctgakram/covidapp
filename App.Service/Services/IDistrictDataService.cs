using AppProj.Domain;
using AppProj.Domain.ModelExt;
using AppProj.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Service.Services
{
    public interface IDistrictDataService
    {
        DistrictData Get(int districtId, DateTime date);
        DistrictSummery GetSummery(int districtId);
        IEnumerable<DistrictData> Get(int? divId, int? disId, DateTime? fromDate, DateTime? toDate, int skip, int take, out int count);
        IEnumerable<DistrictSummery> GetSummery(int? divId, int? disId, int skip, int take, out int count);
        IEnumerable<DistrictSummery> GetSummery(int? divId, int? disId, DateTime? fromDate, DateTime? toDate, int skip, int take, out int count);
        IEnumerable<DistrictSummery> GetSummery();
        string GetTopDistricts(int take);
        IEnumerable<CountModel> GetLastPatientCount(int take);
        void Update(DistrictData disEntity, DistrictSummery sumEntity);

        string GetTopDistrictsQurantine(int take);
        string GetTopDistrictsRelief(int take);
        string GetTopDistrictsBracReliefMoney(int take);
        string GetTopDistrictsBracReliefFood(int take);

        IEnumerable<DistrictPatient> GetPatient(DateTime date);
        void AddPatient(DistrictPatient entity);
        void UpdatePatient(DistrictPatient entity);
        List<SummeryStatus> GetSummeryStatus(int take);

    }
}
