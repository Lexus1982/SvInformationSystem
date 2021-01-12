using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkEquipments.Services.DTO;

namespace NetworkEquipments.Services.Interfaces
{
    public interface IStreetService : IDisposable
    {
        IEnumerable<StreetDto> Get();
        IEnumerable<StreetDto> GetByTownId(int? townId);
        StreetDto GetById(int? streetId);
    }
}
