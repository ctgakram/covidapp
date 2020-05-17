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
    public class DoctorPoleStatusesRepository : RepositoryBase<DoctorPoleStatus>, IDoctorPoleStatusesRepository
    {
        public DoctorPoleStatusesRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
