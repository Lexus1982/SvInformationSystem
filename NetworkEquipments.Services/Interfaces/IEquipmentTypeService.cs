using System;
using System.Collections.Generic;
using NetworkEquipments.Services.DTO;

namespace NetworkEquipments.Services.Interfaces
{
    public interface IEquipmentTypeService : IDisposable
    {
        IEnumerable<EquipmentTypeDto> Get();
        EquipmentTypeDto GetById(int? typeId);
        bool IsTypeNameExists(string value, string skipId);
        IEnumerable<EquipmentTypeDto> Search(string value);
        int Create(EquipmentTypeDto equipmentTypeDto);
        int Delete(int? id);
        int Update(EquipmentTypeDto equipmentTypeDto);
    }
}
