using System;
using System.Collections.Generic;
using NetworkEquipments.Services.DTO;

namespace NetworkEquipments.Services.Interfaces
{
    public interface INetworkService : IDisposable
    {
        IEnumerable<NetworkDto> Get();
        NetworkDto GetById(int? networkId);
        IEnumerable<NetworkDto> Search(string value);
        bool IsIpIntervalExists(string value, string skipId);
        int Create(NetworkDto networkDto);
        int Delete(int? networkId);
        int Update(NetworkDto networkDto);

    }
}
