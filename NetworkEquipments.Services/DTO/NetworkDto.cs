using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkEquipments.Services.DTO
{
    public class NetworkDto
    {
        public int Id { get; set; }
        public int AddressId { get; set; }
        public AddressDto Address { get; set; }
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
