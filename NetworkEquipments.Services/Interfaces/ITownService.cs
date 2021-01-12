using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkEquipments.Services.DTO;

namespace NetworkEquipments.Services.Interfaces
{
    public interface ITownService : IDisposable
    {
        IEnumerable<TownDto> Get();
        TownDto GetById(int? townId);
    }
}
