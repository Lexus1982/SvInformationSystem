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
    public class TownService : ITownService
    {
        //private readonly IDatabaseConnectionFactory _connectionFactory;
        private readonly AdoContext _context;

        public TownService(IDatabaseConnectionFactory connectionFactory)
        {
            _context = new AdoContext(connectionFactory);
        }

        public TownService(IAdoContext context)
        {
            _context = context as AdoContext;
        }

        public IEnumerable<TownDto> Get()
        {
            return MapTownModel(new TownRepository(_context).Get());
        }

        public TownDto GetById(int? townId)
        {
            if (townId == null)
                throw new ValidationException("Не задан Id города", "");

            var town = new TownRepository(_context).GetById((int)townId);
            if (town == null)
                throw new ValidationException("Город не найден", "");

            return MapTownModel(town);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        private static IEnumerable<TownDto> MapTownModel(IEnumerable<Town> towns)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Town, TownDto>()).CreateMapper();
            return mapper.Map<IEnumerable<Town>, List<TownDto>>(towns);
        }

        private static TownDto MapTownModel(Town town)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Town, TownDto>()).CreateMapper();
            return mapper.Map<Town, TownDto>(town);
        }
    }
}
