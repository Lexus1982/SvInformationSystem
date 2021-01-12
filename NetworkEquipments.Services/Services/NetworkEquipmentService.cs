using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NetworkEquipments.Domain.ADO;
using NetworkEquipments.Domain.Entities;
using NetworkEquipments.Domain.Interfaces;
using NetworkEquipments.Domain.Repositories;
using NetworkEquipments.Services.DTO;
using NetworkEquipments.Services.Infrastructure;
using NetworkEquipments.Services.Interfaces;

namespace NetworkEquipments.Services.Services
{
    public class NetworkEquipmentService : INetworkEquipmentService
    {
        private readonly AdoContext _context; 
         
        public NetworkEquipmentService(IAdoContext context)
        {
            _context = context as AdoContext;
        }

        public IEnumerable<NetworkEquipmentDto> Get()
        {
            return MapNetworkEquipmentModel(new NetworkEquipmentRepository(_context).Get());
        }

        public IEnumerable<NetworkEquipmentDto> GetByNetworkId(int? networkId)
        {
            if (networkId == null)
                throw new ValidationException("Не задан Id узла сети", "");

            return MapNetworkEquipmentModel(new NetworkEquipmentRepository(_context).GetByNetworkId((int)networkId));
        }

        public NetworkEquipmentDto GetById(int? equipmentId) 
        {
            if (equipmentId == null)
                throw new ValidationException("Не задан Id оборудования", "");

            var networkEquipment = new NetworkEquipmentRepository(_context).GetById((int)equipmentId);
            if (networkEquipment == null)
                throw new ValidationException("Оборудование на узле сети не найдено", "");

            return MapNetworkEquipmentModel(networkEquipment);
        }

        public bool IsIpAddressExists(string value, string skipId)
        {
            if (string.IsNullOrEmpty(value))
                throw new ValidationException("Не задан IP адрес оборудования", "");

            var networkEquipment = new NetworkEquipmentRepository(_context).GetByIpAddress(value);
            return networkEquipment != null && (string.IsNullOrEmpty(skipId) || networkEquipment.Id != int.Parse(skipId));
        }

        public IEnumerable<NetworkEquipmentDto> Search(string value)
        {
            return MapNetworkEquipmentModel(new NetworkEquipmentRepository(_context).Search(value));
        }

        public IEnumerable<NetworkEquipmentDto> Search(int? networkId, string value)
        {
            if (networkId == null)
                throw new ValidationException("Не задан Id узла сети", "");

            return MapNetworkEquipmentModel(new NetworkEquipmentRepository(_context).Search((int)networkId, value));
        }

        public int Create(NetworkEquipmentDto networkEquipmentDto)
        {
            if (networkEquipmentDto == null)
                throw new ValidationException("Не задана модель оборудования", "");

            return new NetworkEquipmentRepository(_context).Create(MapNetworkEquipmentModel(networkEquipmentDto));
        }

        public int Delete(int? equipmentId)
        {
            if (equipmentId == null)
                throw new ValidationException("Не задан Id оборудования", "");

            return new NetworkEquipmentRepository(_context).Delete((int)equipmentId);
        }

        public int Update(NetworkEquipmentDto networkEquipmentDto)
        {
            if (networkEquipmentDto == null)
                throw new ValidationException("Не задана модель оборудования", "");

            return new NetworkEquipmentRepository(_context).Update(MapNetworkEquipmentModel(networkEquipmentDto));
        }

        private static NetworkEquipment MapNetworkEquipmentModel(NetworkEquipmentDto networkEquipmentDto)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<NetworkEquipmentDto, NetworkEquipment>()).CreateMapper();
            var networkEquipment = mapper.Map<NetworkEquipmentDto, NetworkEquipment>(networkEquipmentDto);
            return networkEquipment;
        }

        private NetworkEquipmentDto MapNetworkEquipmentModel(NetworkEquipment networkEquipment)
        {
            var address = new AddressService(_context).GetById(networkEquipment.AddressId);
            var equipmentType = new EquipmentTypeService(_context).GetById(networkEquipment.EquipmentTypeId);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<NetworkEquipment, NetworkEquipmentDto>()).CreateMapper();
            var networkDto = mapper.Map<NetworkEquipment, NetworkEquipmentDto>(networkEquipment);

            networkDto.Address = address;
            networkDto.EquipmentType = equipmentType;

            return networkDto;
        }

        private IEnumerable<NetworkEquipmentDto> MapNetworkEquipmentModel(IEnumerable<NetworkEquipment> networkEquipments)
        {
            var addresses = new AddressService(_context).Get();
            var equipmentTypes = new EquipmentTypeService(_context).Get();

            return from networkEquipment in networkEquipments
                   join address in addresses
                       on networkEquipment.AddressId equals address.Id
                   join equipmentType in equipmentTypes
                       on networkEquipment.EquipmentTypeId equals equipmentType.Id
                   select new NetworkEquipmentDto()
                   {
                       Id = networkEquipment.Id,
                       NetworkId = networkEquipment.NetworkId,
                       AddressId = networkEquipment.AddressId,
                       Entrance = networkEquipment.Entrance,
                       EquipmentTypeId = networkEquipment.EquipmentTypeId,
                       Ip = networkEquipment.Ip,
                       Commentary = networkEquipment.Commentary,
                       UserId = networkEquipment.UserId,
                       ChangeDate = networkEquipment.ChangeDate,
                       Address = new AddressDto()
                       {
                           Id = address.Id,
                           ComplexHouse = address.ComplexHouse,
                           StreetId = address.StreetId,
                           Street = address.Street
                       },
                       EquipmentType = new EquipmentTypeDto()
                       {
                           Id = equipmentType.Id,
                           Name = equipmentType.Name,
                           Position = equipmentType.Position,
                           UserId = equipmentType.UserId,
                           ChangeDate = equipmentType.ChangeDate
                       }
                   };
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
