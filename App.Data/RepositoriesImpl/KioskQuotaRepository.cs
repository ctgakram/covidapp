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
    public class KioskQuotaRepository : RepositoryBase<KioskQuota>, IKioskQuotaRepository
    {
        public KioskQuotaRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
