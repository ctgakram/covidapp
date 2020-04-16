using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppProj.Service.Services;
using AppProj.Data.Repositories;
using AppProj.Data.Infrastructure;
using AppProj.Domain;

namespace AppProj.Service.ServicesImpl
{
    public class ProgramByUserProfileService : IProgramByUserProfileService
    {
        readonly IProgramByUserProfileRepository progByUserRepository;
        readonly IUnitOfWork unitOfWork;

        public ProgramByUserProfileService(IProgramByUserProfileRepository progByUserRepository, IUnitOfWork unitOfWork)
        {
            this.progByUserRepository = progByUserRepository;
            this.unitOfWork = unitOfWork;
        }

        public ProgramByUserProfile Get(int Id)
        {
            return progByUserRepository.Get(c => c.Id == Id);
        }

        public IEnumerable<ProgramByUserProfile> GetByUser(int userId)
        {
            return progByUserRepository.GetMany(c => c.UserInfoId == userId);
        }

        public void Add(ProgramByUserProfile entity)
        {
            progByUserRepository.Add(entity);
        }

        public void Delete(ProgramByUserProfile entity)
        {
            progByUserRepository.Delete(entity);
        }

    }
}
