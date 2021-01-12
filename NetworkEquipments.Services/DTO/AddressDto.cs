using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkEquipments.Domain.Entities;

namespace NetworkEquipments.Services.DTO
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string House { get; set; }
        public string Corp { get; set; }
        public string ComplexHouse { get; set; }
        public int StreetId { get; set; }
        public StreetDto Street { get; set; }

        //public string FullAddress { get; set; }
    }
}
