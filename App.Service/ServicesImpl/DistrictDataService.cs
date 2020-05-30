using AppProj.Data.Infrastructure;
using AppProj.Data.Repositories;
using AppProj.Domain;
using AppProj.Domain.ModelExt;
using AppProj.Domain.ViewModel;
using AppProj.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Service.ServicesImpl
{
    public class DistrictDataService : IDistrictDataService
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

        public IEnumerable<DistrictData> Get(int? divId, int? disId, DateTime? fromDate, DateTime? toDate, int skip, int take, out int count)
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
                .ThenBy(c => c.Date)
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
            return patientRepository.GetMany(c => c.Date >= dt)
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

        public string GetTopDistrictsBracReliefMoney(int take)
        {
            var model = sumRepository.GetAll()
                .OrderByDescending(c => (c.BracMoneyDistribution))
               .Take(take)
               .Skip(0);

            return string.Join(", ", model.Select(c => c.StandingData1.Name).ToArray());
        }

        public string GetTopDistrictsBracReliefFood(int take)
        {
            var model = sumRepository.GetAll()
                .OrderByDescending(c => (c.BracFoodDistribution))
               .Take(take)
               .Skip(0);

            return string.Join(", ", model.Select(c => c.StandingData1.Name).ToArray());
        }


        public void Update(DistrictData disEntity, DistrictSummery sumEntity)
        {
            sumEntity.LastUpdateTime = DateTime.Now;
            if (disEntity.Id == 0)
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
        public List<SummeryStatus> GetSummeryStatus(int take)
        {
            var output = new List<SummeryStatus>();
            var dissummery = sumRepository.GetAll();

            SummeryStatus pperticipate = new SummeryStatus();
            SummeryStatus qurantine = new SummeryStatus();
            SummeryStatus aiddistribution = new SummeryStatus();

            pperticipate.Name = "Top 5 Districts - Probable cases in programme participants";
            pperticipate.Color = "orange";
            pperticipate.TopItems = formatdistrict(string.Join(", ", dissummery.OrderByDescending(c => c.PatientCount).Take(take).Skip(0).Select(c => c.StandingData1.Name).ToArray()));
            pperticipate.TopDisplayItems = string.Join(", ", dissummery.OrderByDescending(c => c.PatientCount).Take(take).Skip(0).Select(c => c.StandingData1.Name + "(" + c.PatientCount + ")").ToArray());
            pperticipate.DistrictCnt = dissummery.OrderByDescending(c => c.PatientCount).Select(x => new DistrictMapInfo
            {
                NameId = formatdistrict(x.StandingData1.Name),
                Count = (int)x.PatientCount,
                Display = x.StandingData1.Name
            }).ToList();

            //pperticipate.ItemCounts = dissummery.OrderByDescending(c => c.PatientCount).Take(take).Select(x => new DistrictMapInfo
            //{
            //    NameId = formatdistrict(x.StandingData1.Name),
            //    Count = (int)x.PatientCount,
            //    Display = x.StandingData1.Name
            //}).ToList();





            qurantine.Name = "Top 5 Districts - Number of citizens in quarantine by GoB";
            qurantine.Color = "red";
            qurantine.TopItems = formatdistrict(string.Join(", ", dissummery.OrderByDescending(c => c.CurrentQuarantine).Take(take).Skip(0).Select(c => c.StandingData1.Name).ToArray()));
            qurantine.TopDisplayItems = string.Join(", ", dissummery.OrderByDescending(c => c.CurrentQuarantine).Take(take).Skip(0).Select(c => c.StandingData1.Name + "(" + c.CurrentQuarantine + ")").ToArray());
            qurantine.DistrictCnt = dissummery.OrderByDescending(c => c.CurrentQuarantine).Select(x => new DistrictMapInfo
            {
                NameId = formatdistrict(x.StandingData1.Name),
                Count = (int)x.CurrentQuarantine,
                Display = x.StandingData1.Name
            }).ToList();
            //qurantine.ItemCounts = dissummery.OrderByDescending(c => c.CurrentQuarantine).Take(take).Select(x => new DistrictMapInfo
            //{
            //    NameId = formatdistrict(x.StandingData1.Name),
            //    Count = (int)x.CurrentQuarantine,
            //    Display = x.StandingData1.Name
            //}).ToList();



            aiddistribution.Name = "Top 5 Districts - Food & Cash Distribution by GoB";
            aiddistribution.Color = "green";
            aiddistribution.TopItems = formatdistrict(string.Join(", ", dissummery.OrderByDescending(c => (c.TotalReliefFamily + c.TotalReliefPerson)).Take(take).Skip(0).Select(c => c.StandingData1.Name).ToArray()));
            aiddistribution.TopDisplayItems = string.Join(", ", dissummery.OrderByDescending(c => (c.TotalReliefFamily + c.TotalReliefPerson)).Take(take).Skip(0).Select(c => c.StandingData1.Name + "(" + (c.TotalReliefFamily + c.TotalReliefPerson) + ")").ToArray());
            aiddistribution.DistrictCnt = dissummery.OrderByDescending(c => (c.TotalReliefFamily + c.TotalReliefPerson)).Select(x => new DistrictMapInfo
            {
                NameId = formatdistrict(x.StandingData1.Name),
                Count = (int)x.TotalReliefFamily + (int)x.TotalReliefPerson,
                Display = x.StandingData1.Name
            }).ToList();
            //aiddistribution.ItemCounts = dissummery.OrderByDescending(c => (c.TotalReliefFamily + c.TotalReliefPerson)).Take(take).Select(x => new DistrictMapInfo
            //{
            //    NameId = formatdistrict(x.StandingData1.Name),
            //    Count = (int)x.TotalReliefFamily + (int)x.TotalReliefPerson,
            //    Display = x.StandingData1.Name
            //}).ToList();

            output.Add(pperticipate);
            output.Add(qurantine);
            output.Add(aiddistribution);
            return output;
        }
        public void UpdatePatient(DistrictPatient entity)
        {
            patientRepository.Update(entity);
        }
        public string formatdistrict(string p_districts)
        {
            var output = string.Empty;
            output = p_districts.ToLower().Trim().Replace("cox's bazar", "coxs_bazar").Replace("chapai nababganj", "chapai_nababganj").Replace(" ", "");
            return output;

        }

    }
}
