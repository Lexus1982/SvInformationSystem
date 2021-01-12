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
    public class EquipmentTypeService : IEquipmentTypeService
    {
        private readonly AdoContext _context;
        
        public EquipmentTypeService(IAdoContext context)
        {
            _context = context as AdoContext;
        }

        public IEnumerable<EquipmentTypeDto> Get()
        {
            return MapEquipmentTypeModel(new EquipmentTypeRepository(_context).Get());
        }

        public EquipmentTypeDto GetById(int? typeId)
        {
            if (typeId == null)
                throw new ValidationException("Не задан Id типа оборудования", "");

            var equipmentType = new EquipmentTypeRepository(_context).GetById((int) typeId);
            if (equipmentType == null)
                throw new ValidationException("Тип оборудования не найден", "");

            return MapEquipmentTypeModel(equipmentType);
        }

        public bool IsTypeNameExists(string value, string skipId)
        {
            if (string.IsNullOrEmpty(value))
                throw new ValidationException("Не задано наименование типа оборудования", "");

            var equipmentType = new EquipmentTypeRepository(_context).GetByName(value);
            return equipmentType != null && (string.IsNullOrEmpty(skipId) || equipmentType.Id != int.Parse(skipId));
        }

        public IEnumerable<EquipmentTypeDto> Search(string value)
        {
            return MapEquipmentTypeModel(new EquipmentTypeRepository(_context).Search(value));
        }

        public int Create(EquipmentTypeDto equipmentTypeDto)
        {
            if (equipmentTypeDto == null)
                throw new ValidationException("Не задана модель типа оборудования", "");

            return new EquipmentTypeRepository(_context).Create(MapEquipmentTypeModel(equipmentTypeDto));
        }

        public int Delete(int? typeId)
        {
            if (typeId == null)
                throw new ValidationException("Не задан Id типа оборудования", "");

            return new EquipmentTypeRepository(_context).Delete((int)typeId);
        }

        public int Update(EquipmentTypeDto equipmentTypeDto)
        {
            if (equipmentTypeDto == null)
                throw new ValidationException("Не задана модель типа оборудования", "");

            return new EquipmentTypeRepository(_context).Update(MapEquipmentTypeModel(equipmentTypeDto));
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        private static EquipmentType MapEquipmentTypeModel(EquipmentTypeDto equipmentTypeDto)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EquipmentTypeDto, EquipmentType>()).CreateMapper();
            return mapper.Map<EquipmentTypeDto, EquipmentType>(equipmentTypeDto);
        }

        private static EquipmentTypeDto MapEquipmentTypeModel(EquipmentType equipmentType)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EquipmentType, EquipmentTypeDto>()).CreateMapper();
            return mapper.Map<EquipmentType, EquipmentTypeDto>(equipmentType);
        }
        
        private static IEnumerable<EquipmentTypeDto> MapEquipmentTypeModel(IEnumerable<EquipmentType> equipmentTypes)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EquipmentType, EquipmentTypeDto>()).CreateMapper();
            return mapper.Map<IEnumerable<EquipmentType>, List<EquipmentTypeDto>>(equipmentTypes);
        }

    }
}
