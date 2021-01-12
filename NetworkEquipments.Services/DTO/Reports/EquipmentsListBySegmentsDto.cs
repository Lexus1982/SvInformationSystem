using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkEquipments.Services.DTO.Reports
{
    public class EquipmentsListBySegmentsDto
    {
        public int? SegmentNumber { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
