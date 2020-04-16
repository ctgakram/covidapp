using AppProj.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Service.Services
{
    public interface IDistrictQuestionService
    {
        List<int> GetDistrictQuestions(int districtId);

        IEnumerable<DistrictQuestion> GetByQuestion(int questionId);

        DistrictQuestion Get(int id);

        void Add(DistrictQuestion entity);

        void Delete(DistrictQuestion entity);
    }
}
