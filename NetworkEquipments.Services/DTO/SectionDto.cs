using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkEquipments.Services.DTO
{
    public class SectionDto
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public string Position { get; set; }
    }
}
