using AppProj.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Service.Services
{
    public interface IStandingDataService
    {
        IEnumerable<StandingData> GetUpazilla(int disId);        
        IEnumerable<StandingData> GetSource();
        IEnumerable<StandingData> GetGender();
        IEnumerable<StandingData> GetDistricts();
        IEnumerable<StandingData> GetDistricts(int divId);
        IEnumerable<StandingData> GetDivisions();
        IEnumerable<StandingData> GetRestrictions();
        IEnumerable<StandingData> GetImpactedReasons();
        IEnumerable<StandingData> GetInitiatives();
        IEnumerable<StandingData> GetMaterials();
        IEnumerable<StandingData> GetCommMaterials();
        IEnumerable<StandingData> GetCommChannels();
        IEnumerable<StandingData> GetReachTypes();
        IEnumerable<StandingData> GetQuestions();

        IEnumerable<StandingData> GetByType(string type);

        IEnumerable<StandingDataPcRelation> GetParentChild(string type);
        IEnumerable<StandingDataPcRelation> GetParentChild(int parentId, string type);
        StandingDataPcRelation GetParentChildById(int Id);
        void AddParentChild(StandingDataPcRelation entity);
        void DeleteParentChild(StandingDataPcRelation entity);

        StandingData GetDataById(int id);
        void Add(StandingData entity);
        void Update(StandingData entity);

        IEnumerable<StandingData> GetAllDataById(int id);
    }
}
