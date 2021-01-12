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
    public class AddressService : IAddressService
    {
        //private readonly IDatabaseConnectionFactory _connectionFactory;
        private readonly AdoContext _context;

        //public AddressService(IDatabaseConnectionFactory connectionFactory)
        //{
        //    _context = new AdoContext(connectionFactory);
        //}

        public AddressService(IAdoContext context)
        {
            _context = context as AdoContext;
        }

        public IEnumerable<AddressDto> Get()
        {
            return MapAddressModel(new AddressRepository(_context).Get());
        }

        public IEnumerable<AddressDto> GetByStreetId(int? streetId)
        {
            if (streetId == null)
                throw new ValidationException("Не задан Id улицы", "");

            return MapAddressModel(new AddressRepository(_context).GetByStreetId((int)streetId), streetId);
        }

        public AddressDto GetById(int? addressId)
        {
            if (addressId == null)
                throw new ValidationException("Не задан Id адреса", "");

            var address = new AddressRepository(_context).GetById((int)addressId);
            if (address == null)
                throw new ValidationException("Адрес не найден", "");

            return MapAddressModel(address);
        }

        private AddressDto MapAddressModel(Address address)
        {
            var street = new StreetService(_context).GetById(address.StreetId);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Address, AddressDto>()).CreateMapper();
            var addressDto = mapper.Map<Address, AddressDto>(address);
            addressDto.Street = street;
            return addressDto;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        private string GetComplexHouse(string house, string corp)
        {
            var postfix = corp?.Length > 0 && char.IsDigit(corp, 0)
                ? " корп. " + corp
                : !string.IsNullOrEmpty(corp) ? corp : "";

            return $"{house}{postfix}";
        }

        private IEnumerable<AddressDto> MapAddressModel(IEnumerable<Address> addresses, int? streetId = null)
        {
            var streetService = new StreetService(_context);
            var streets = streetId == null ? streetService.Get() : new List<StreetDto> { streetService.GetById((int)streetId) };

            return from address in addresses.AsEnumerable()
                   join street in streets.AsEnumerable()
                       on address.StreetId equals street.Id
                   select new AddressDto()
                   {
                       Id = address.Id,
                       House = address.House,
                       Corp = address.Corp,
                       StreetId = address.StreetId,
                       Street = new StreetDto()
                       {
                           Id = street.Id,
                           Name = street.Name,
                           TownId = street.TownId,
                           Town = street.Town
                       },
                       ComplexHouse = GetComplexHouse(address.House, address.Corp)
                   };
        }
    }
}
