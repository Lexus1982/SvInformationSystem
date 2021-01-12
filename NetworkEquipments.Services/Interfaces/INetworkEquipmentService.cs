using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkEquipments.Services.DTO;

namespace NetworkEquipments.Services.Interfaces
{
    public interface INetworkEquipmentService : IDisposable
    {
        IEnumerable<NetworkEquipmentDto> Get();
        NetworkEquipmentDto GetById(int? equipmentId);
        IEnumerable<NetworkEquipmentDto> GetByNetworkId(int? networkId);
        bool IsIpAddressExists(string value, string skipId);
        IEnumerable<NetworkEquipmentDto> Search(string value);
        IEnumerable<NetworkEquipmentDto> Search(int? networkId, string value);
        int Create(NetworkEquipmentDto networkEquipmentDto);
        int Delete(int? equipmentId);
        int Update(NetworkEquipmentDto networkEquipmentDto);

    }
}
