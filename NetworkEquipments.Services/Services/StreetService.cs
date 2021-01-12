using System;
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
    public class StreetService : IStreetService
    {
        //private readonly IDatabaseConnectionFactory _connectionFactory;
        private readonly AdoContext _context;

        //public StreetService(IDatabaseConnectionFactory connectionFactory)
        //{
        //    _context = new AdoContext(connectionFactory);
        //}

        public StreetService(IAdoContext context)
        {
            //_connectionFactory = connectionFactory;
            _context = context as AdoContext;
        }

        public IEnumerable<StreetDto> Get()
        {
            return MapStreetModel(new StreetRepository(_context).Get());
        }

        public IEnumerable<StreetDto> GetByTownId(int? townId)
        {
            if (townId == null)
                throw new ValidationException("Не задан Id города", "");

            return MapStreetModel(new StreetRepository(_context).GetByTownId((int)townId), townId);
        }

        public StreetDto GetById(int? streetId)
        {
            if (streetId == null)
                throw new ValidationException("Не задан Id улицы", "");

            var street = new StreetRepository(_context).GetById((int)streetId);
            if (street == null)
                throw new ValidationException("Улица не найдена", "");

            return MapStreetModel(street);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        private StreetDto MapStreetModel(Street street)
        {
            var town = new TownService(_context).GetById(street.TownId);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Street, StreetDto>()).CreateMapper();
            var streetDto = mapper.Map<Street, StreetDto>(street);
            streetDto.Town = town;
            return streetDto;
        }

        private IEnumerable<StreetDto> MapStreetModel(IEnumerable<Street> streets, int? townId = null)
        {
            var townService = new TownService(_context);
            var towns = townId == null ? townService.Get() : new List<TownDto> { townService.GetById((int)townId) };

            // Join как расширение IEnumerable менее читабелен.
            //return streets.Join(towns, street => street.TownId, town => town.Id,
            //    (street, town) => new StreetDto()
            //    {
            //        Id = street.Id,
            //        Name = street.Name,
            //        TownId = street.TownId,
            //        Town = town
            //    });

            return from street in streets.AsEnumerable()
                   join town in towns.AsEnumerable() on street.TownId equals town.Id
                   select new StreetDto()
                   {
                       Id = street.Id,
                       Name = street.Name,
                       TownId = street.TownId,
                       Town = town
                   };
        }
    }
}
