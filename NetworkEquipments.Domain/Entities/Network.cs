using System;

namespace NetworkEquipments.Domain.Entities
{
    public class Network
    {
        public int Id { get; set; }
        public int AddressId { get; set; }
        public int? SegmentNumber { get; set; }
        public string VlanManage { get; set; }
        public string VlanInternet { get; set; }
        public string IpInterval { get; set; }
        public int EquipmentsCount { get; set; }
        public string Commentary { get; set; }
        public int UserId { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}
