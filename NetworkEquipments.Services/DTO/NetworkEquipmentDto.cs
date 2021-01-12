using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkEquipments.Domain.Entities;

namespace NetworkEquipments.Services.DTO
{
    public class NetworkEquipmentDto 
    {
        public int Id { get; set; }
        public int NetworkId { get; set; }
        public int AddressId { get; set; }
        public AddressDto Address { get; set; }
        public int? Entrance { get; set; }
        public int EquipmentTypeId { get; set; }
        public EquipmentTypeDto EquipmentType { get; set; }
        public string Ip { get; set; }
        public string Commentary { get; set; }
        public int UserId { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}
