using System.Collections.Generic;
using NetworkEquipments.Services.DTO;

namespace NetworkEquipments.Web.Models.NetworkEquipment
{
    public class NetworkEquipmentViewModel
    {
        public NetworkDto Network { get; set; }
        public IEnumerable<NetworkEquipmentModel> NetworkEquipments { get; set; }
        public NetworkEquipmentSortModel Sort { get; set; }
        public NetworkEquipmentFilterModel Filter { get; set; }
    }
}