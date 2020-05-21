using AppProj.Data.Infrastructure;
using AppProj.Data.Repositories;
using AppProj.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Data.RepositoriesImpl
{
    public class DoctorsPoleVisitDetailRepository : RepositoryBase<DoctorsPoleVisitDetail>, IDoctorsPoleVisitDetailRepository
    {
        public DoctorsPoleVisitDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
