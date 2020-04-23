using AppProj.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Service.Services
{
    public interface IMapSummaryService
    {
        IEnumerable<MapSummary> Get();
    }
}
