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
    public class DistrictQuestionService : IDistrictQuestionService
    {
        private IDistrictQuestionRepository _repository;
        private IUnitOfWork _unitOfWork;

        public DistrictQuestionService(IDistrictQuestionRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public List<int> GetDistrictQuestions(int districtId)
        {
            var quesIds = _repository.GetMany(q => q.DistrictId == districtId)
                .OrderByDescending(o => o.QuestionId)
                .Select(s => s.QuestionId).ToList();

            return quesIds;
        }

        public DistrictQuestion Get(int id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<DistrictQuestion> GetByQuestion(int questionId)
        {
            return _repository.GetMany(c => c.QuestionId == questionId);
        }

        public void Add(DistrictQuestion entity)
        {
            _repository.Add(entity);
        }

        public void Delete(DistrictQuestion entity)
        {
            _repository.Delete(entity);
        }
    }
}
