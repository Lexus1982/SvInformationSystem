using System.Collections.Generic;

namespace NetworkEquipments.Web.Models.EquipmentType
{
    public class EquipmentTypeViewModel
    {
        public IEnumerable<EquipmentTypeModel> EquipmentType { get; set; }
        public EquipmentTypeSortModel Sort { get; set; }
        public EquipmentTypeFilterModel Filter { get; set; }
    }
}