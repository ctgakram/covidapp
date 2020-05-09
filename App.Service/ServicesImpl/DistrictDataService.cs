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
    public class DistrictDataService: IDistrictDataService
    {
        readonly IDistrictDataRepository disRepository;
        readonly IDistrictSummeryRepository sumRepository;
        readonly IDistrictPatientRepository patientRepository;
        readonly IUnitOfWork unitOfWork;

        public DistrictDataService(
              IDistrictDataRepository disRepository
            , IDistrictSummeryRepository sumRepository
            , IDistrictPatientRepository patientRepository
            , IUnitOfWork unitOfWork)
        {
            this.disRepository = disRepository;
            this.sumRepository = sumRepository;
            this.patientRepository = patientRepository;
            this.unitOfWork = unitOfWork;
        }

        public DistrictData Get(int districtId, DateTime date)
        {
            return disRepository
                .GetMany(c => c.DistrictId == districtId && c.Date == date)
                .FirstOrDefault();
        }

        public DistrictSummery GetSummery(int districtId)
        {
            return sumRepository
                .GetMany(c => c.DistrictId == districtId)
                .FirstOrDefault();
        }

        public IEnumerable<DistrictData> Get(int? divId, int? disId,  DateTime? fromDate, DateTime? toDate, int skip, int take, out int count)
        {
            fromDate = fromDate ?? DateTime.Now;
            toDate = toDate ?? DateTime.Now;

            IEnumerable<DistrictData> data = disRepository.GetMany
                (c =>
                c.Date >= fromDate && c.Date <= toDate                
                && (divId == null ? true : c.DivisionId == divId)
                && (disId == null ? true : c.DistrictId == disId)
                )
                .OrderBy(c => c.StandingData.Name)
                .ThenBy(c => c.StandingData1.Name)
                .ThenBy(c=> c.Date)
                .Skip(skip).Take(take).ToArray();

            count = disRepository.GetCount(c =>
                c.Date >= fromDate && c.Date <= toDate
                && (divId == null ? true : c.DivisionId == divId)
                && (disId == null ? true : c.DistrictId == disId)
                );

            return data;
        }


        public IEnumerable<DistrictSummery> GetSummery(int? divId, int? disId, int skip, int take, out int count)
        {
            IEnumerable<DistrictSummery> data = sumRepository.GetMany(c => (divId == null ? true : c.DivisionId == divId)
                && (disId == null ? true : c.DistrictId == disId)
                )
                .OrderBy(c => c.StandingData.Name)
                .ThenBy(c => c.StandingData1.Name)                
                .Skip(skip).Take(take).ToArray();

            count = sumRepository.GetCount(c => (divId == null ? true : c.DivisionId == divId)
                && (disId == null ? true : c.DistrictId == disId)
                );

            return data;
        }
        public IEnumerable<DistrictSummery> GetSummery()
        {
            return sumRepository.GetAll();
        }

        public string GetTopDistricts(int take)
        {
            var model = sumRepository.GetAll()
                .OrderByDescending(c => c.PatientCount)
               .Take(take)
               .Skip(0);

            return string.Join(", ", model.Select(c => c.StandingData1.Name).ToArray());
        }

        public IEnumerable<CountModel> GetLastPatientCount(int take)
        {
            DateTime dt = DateTime.Now.AddDays(-take);
            return patientRepository.GetMany(c=> c.Date>= dt)
               .GroupBy(l => l.Date.ToString("dd MMM"))
               .Select(c => new CountModel { Name = c.Key, Count = c.Sum(d => d.TillPatientCount) });
        }

        public string GetTopDistrictsQurantine(int take)
        {
            var model = sumRepository.GetAll()
                .OrderByDescending(c => c.CurrentQuarantine)
               .Take(take)
               .Skip(0);

            return string.Join(", ", model.Select(c => c.StandingData1.Name).ToArray());
        }

        public string GetTopDistrictsRelief(int take)
        {
            var model = sumRepository.GetAll()
                .OrderByDescending(c => (c.TotalReliefFamily + c.TotalReliefPerson))
               .Take(take)
               .Skip(0);

            return string.Join(", ", model.Select(c => c.StandingData1.Name).ToArray());
        }


        public void Update(DistrictData disEntity, DistrictSummery sumEntity)
        {
            sumEntity.LastUpdateTime = DateTime.Now;
            if (disEntity.Id==0)
            {
                disRepository.Add(disEntity);
            }
            else
            {
                disRepository.Update(disEntity);
            }


            if (sumEntity.Id == 0)
            {
                sumRepository.Add(sumEntity);
            }
            else
            {
                sumRepository.Update(sumEntity);
            }
        }

        public IEnumerable<DistrictPatient> GetPatient(DateTime date)
        {
            return patientRepository.GetMany(c => c.Date == date);
        }

        public void AddPatient(DistrictPatient entity)
        {
            patientRepository.Add(entity);
        }

        public void UpdatePatient(DistrictPatient entity)
        {
            patientRepository.Update(entity);
        }

    }
}
