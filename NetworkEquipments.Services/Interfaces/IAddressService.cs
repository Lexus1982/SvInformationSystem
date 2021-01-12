using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkEquipments.Services.DTO;

namespace NetworkEquipments.Services.Interfaces
{
    public interface IAddressService : IDisposable
    {
        IEnumerable<AddressDto> Get();
        IEnumerable<AddressDto> GetByStreetId(int? streetId);
        AddressDto GetById(int? addressId);
    }
}
