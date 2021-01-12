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
    public class SectionService : ISectionService
    {
        private readonly AdoContext _context;

        public SectionService(IAdoContext context)
        { 
            _context = context as AdoContext;
        }

        //public NetworkDto GetById(int? networkId)
        //{
        //    if (networkId == null)
        //        throw new ValidationException("Не задан Id узла сети", "");

        //    var network = new NetworkRepository(_context).GetById((int)networkId);
        //    if (network == null)
        //        throw new ValidationException("Узел сети не найден", "");

        //    return MapNetworkModel(network);
        //}
        public SectionDto GetById(int? sectionId)
        {
            if (sectionId == null)
                throw new ValidationException("Не задан Id отчета", "");

            var section = new SectionRepository(_context).GetById((int)sectionId);
            if (section == null)
                throw new ValidationException("Раздел не найден", "");

            return MapRoleModel(section);
        }

        public IEnumerable<SectionDto> GetUserSections(string login, int groupId)
        {
            if (string.IsNullOrEmpty(login))
                throw new ValidationException("Не задан логин пользователя", "");

            var user = new UserRepository(_context).FindByLogin(login);
            if (user == null)
                throw new ValidationException($"Пользователь '{login}' не найден", "");

            return MapRoleModel(new SectionRepository(_context).GetByUserId(user.Id, groupId));
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        private static IEnumerable<SectionDto> MapRoleModel(IEnumerable<Section> sections)
        {
            //TODO: проверить типы !!!!!!!!!
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<IEnumerable<Section>, List<SectionDto>>()).CreateMapper();
            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Section, SectionDto>()).CreateMapper();
            return mapper.Map<IEnumerable<Section>, List<SectionDto>>(sections);
        }

        private static SectionDto MapRoleModel(Section section)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Section, SectionDto>()).CreateMapper();
            return mapper.Map<Section, SectionDto>(section);
        }
    }
}
