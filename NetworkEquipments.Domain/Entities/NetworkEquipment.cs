using System;

namespace NetworkEquipments.Domain.Entities
{
    public class NetworkEquipment
    {
        public int Id { get; set; }
        public int NetworkId { get; set; }
        public int AddressId { get; set; }
        public int? Entrance { get; set; }
        public int EquipmentTypeId { get; set; }
        public string Ip { get; set; }
        public string Commentary { get; set; }
        public int UserId { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}
