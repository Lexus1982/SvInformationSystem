using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class NetworkService : INetworkService
    {
        private readonly AdoContext _context;

        //public NetworkService(IDatabaseConnectionFactory connectionFactory)
        //{
        //    _context = new AdoContext(connectionFactory);
        //}

        public NetworkService(IAdoContext context)
        {
            _context = context as AdoContext;
        }

        public IEnumerable<NetworkDto> Get()
        {
            return MapNetworkModel(new NetworkRepository(_context).Get());
        }

        public NetworkDto GetById(int? networkId)
        {
            if (networkId == null)
                throw new ValidationException("Не задан Id узла сети", "");

            var network = new NetworkRepository(_context).GetById((int)networkId);
            if (network == null)
                throw new ValidationException("Узел сети не найден", "");
            
            return MapNetworkModel(network);
        }

        public bool IsIpIntervalExists(string value, string skipId)
        {
            if (string.IsNullOrEmpty(value))
                throw new ValidationException("Не задан IP диапазон узла сети", "");

            var network = new NetworkRepository(_context).GetByIpInterval(value);
            return network != null && (string.IsNullOrEmpty(skipId) || network.Id != int.Parse(skipId));
        }

        public IEnumerable<NetworkDto> Search(string value)
        {
            return MapNetworkModel(new NetworkRepository(_context).Search(value));
        }

        public int Create(NetworkDto networkDto)
        {
            if (networkDto == null)
                throw new ValidationException("Не задана модель узла сети", "");

            return new NetworkRepository(_context).Create(MapNetworkModel(networkDto));
        }

        public int Delete(int? networkId)
        {
            if (networkId == null)
                throw new ValidationException("Не задан Id узла сети", "");

            return new NetworkRepository(_context).Delete((int)networkId);
        }

        public int Update(NetworkDto networkDto)
        {
            if (networkDto == null)
                throw new ValidationException("Не задана модель узла сети", "");

            return new NetworkRepository(_context).Update(MapNetworkModel(networkDto));
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        private static Network MapNetworkModel(NetworkDto network)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<NetworkDto, Network>()).CreateMapper();
            return mapper.Map<NetworkDto, Network>(network);
        }

        private NetworkDto MapNetworkModel(Network network)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Network, NetworkDto>()).CreateMapper();
            var networkDto = mapper.Map<Network, NetworkDto>(network);
            networkDto.Address = new AddressService(_context).GetById(network.AddressId);

            return networkDto;
        }

        private IEnumerable<NetworkDto> MapNetworkModel(IEnumerable<Network> networks)
        {
            return from network in networks.AsEnumerable()
                   join address in new AddressService(_context).Get().AsEnumerable()
                       on network.AddressId equals address.Id
                   select new NetworkDto()
                   {
                       Id = network.Id,
                       AddressId = network.AddressId,
                       SegmentNumber = network.SegmentNumber,
                       VlanManage = network.VlanManage,
                       VlanInternet = network.VlanInternet,
                       IpInterval = network.IpInterval,
                       EquipmentsCount = network.EquipmentsCount,
                       Commentary = network.Commentary,
                       UserId = network.UserId,
                       ChangeDate = network.ChangeDate,
                       Address = new AddressDto()
                       {
                           Id = address.Id,
                           ComplexHouse = address.ComplexHouse,
                           StreetId = address.StreetId,
                           Street = address.Street
                       }
                   };
        }
    }
}
