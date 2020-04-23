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
    public class MapSummaryService : IMapSummaryService
    {
        readonly IMapSummaryRepository repository;
        readonly IUnitOfWork unitOfWork;

        public MapSummaryService(IMapSummaryRepository repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<MapSummary> Get()
        {
            return repository.GetAll();
        }
    }
}
