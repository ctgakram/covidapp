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
    public class StandingDataService: IStandingDataService
    {
        readonly IStandingDataRepository serviceRepository;
        readonly IStandingDataPcRelationRepository servicePcRepository;
        readonly IUnitOfWork unitOfWork;

        public StandingDataService(IStandingDataRepository serviceRepository, IStandingDataPcRelationRepository servicePcRepository, IUnitOfWork unitOfWork)
        {
            this.serviceRepository = serviceRepository;
            this.servicePcRepository = servicePcRepository;
            this.unitOfWork = unitOfWork;
        }


        public IEnumerable<StandingData> GetUpazilla(int disId)
        {
            return serviceRepository
                .GetMany(c => c.Type == "UPZ" && c.ParentId==disId && c.IsActive)
                .OrderBy(d=> d.Name);
        }

        public IEnumerable<StandingData> GetSource()
        {
            return serviceRepository
                .GetMany(c => c.Type == "SRC" && c.IsActive)
                .OrderBy(d => d.Name);
        }

        public IEnumerable<StandingData> GetGender()
        {
            return serviceRepository
                .GetMany(c => c.Type == "GEN" && c.IsActive)
                .OrderBy(d => d.Name);
        }

        public IEnumerable<StandingData> GetDistricts()
        {
            return serviceRepository
                .GetMany(c => c.Type == "DIS" && c.IsActive)
                .OrderBy(d => d.Name);
        }

        public IEnumerable<StandingData> GetDistricts(int divId)
        {
            return serviceRepository
                .GetMany(c => c.Type == "DIS" && c.IsActive && c.ParentId==divId)
                .OrderBy(d => d.Name);
        }

        public IEnumerable<StandingData> GetDivisions()
        {
            return serviceRepository
                .GetMany(c => c.Type == "DIV" && c.IsActive)
                .OrderBy(d => d.Name);
        }

        public IEnumerable<StandingData> GetImpactedReasons()
        {
            return serviceRepository
                .GetMany(c => c.Type == "IMP" && c.IsActive)
                .OrderBy(d => d.Name);
        }

        public IEnumerable<StandingData> GetRestrictions()
        {
            return serviceRepository
                .GetMany(c => c.Type == "RES" && c.IsActive)
                .OrderBy(d => d.Name);
        }
        public IEnumerable<StandingData> GetInitiatives()
        {
            return serviceRepository
                .GetMany(c => c.Type == "PNT" && c.IsActive)
                .OrderBy(d => d.Name);
        }

        public IEnumerable<StandingData> GetMaterials()
        {
            return serviceRepository
                .GetMany(c => c.Type == "NME" && c.IsActive)
                .OrderBy(d => d.Name);
        }

        public IEnumerable<StandingData> GetCommMaterials()
        {
            return serviceRepository
                .GetMany(c => c.Type == "CML" && c.IsActive)
                .OrderBy(d => d.Name);
        }

        public IEnumerable<StandingData> GetCommChannels()
        {
            return serviceRepository
                .GetMany(c => c.Type == "CCL" && c.IsActive)
                .OrderBy(d => d.Name);
        }

        public IEnumerable<StandingData> GetReachTypes()
        {
            return serviceRepository
                .GetMany(c => c.Type == "RTP" && c.IsActive)
                .OrderBy(d => d.Name);
        }
        public IEnumerable<StandingData> GetQuestions()
        {
            return serviceRepository
                .GetMany(c => c.Type == "QES" && c.IsActive)
                .OrderBy(d => d.Name);
        }

        public IEnumerable<StandingData> GetByType(string type)
        {
            return serviceRepository
                .GetMany(c => c.Type == type)
                .OrderBy(d => d.Name);
        }

        public StandingData GetDataById(int id)
        {
            return serviceRepository.GetById(id);
        }
        public void Add(StandingData entity)
        {
            serviceRepository.Add(entity);
        }
        public void Update(StandingData entity)
        {
            serviceRepository.Update(entity);
        }

        public StandingDataPcRelation GetParentChildById(int Id)
        {
            return servicePcRepository.GetById(Id);
        }
        public IEnumerable<StandingDataPcRelation> GetParentChild(string type)
        {
            return servicePcRepository.GetMany(c => c.Type == type);
        }
        public IEnumerable<StandingDataPcRelation> GetParentChild(int parentId, string type)
        {
            return servicePcRepository.GetMany(c => c.ParentId == parentId && c.Type==type);
        }

        public void AddParentChild(StandingDataPcRelation entity)
        {
            servicePcRepository.Add(entity);
        }

        public void DeleteParentChild(StandingDataPcRelation entity)
        {
            servicePcRepository.Delete(entity);
        }
    }
}
