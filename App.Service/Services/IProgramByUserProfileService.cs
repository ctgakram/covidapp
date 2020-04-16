using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppProj.Domain;

namespace AppProj.Service.Services
{
    public interface IProgramByUserProfileService
    {
        ProgramByUserProfile Get(int Id);
        IEnumerable<ProgramByUserProfile> GetByUser(int userId);
        void Add(ProgramByUserProfile entity);
        void Delete(ProgramByUserProfile entity);
    }
}
