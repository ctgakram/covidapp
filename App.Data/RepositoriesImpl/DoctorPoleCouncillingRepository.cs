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
    public class DoctorPoleCouncillingRepository : RepositoryBase<DoctorPoleCouncilling>, IDoctorPoleCouncillingRepository
    {
        public DoctorPoleCouncillingRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

    }
}
