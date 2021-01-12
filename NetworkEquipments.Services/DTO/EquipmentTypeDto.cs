using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkEquipments.Services.DTO
{
    public class EquipmentTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public int UserId { get; set; }
        public DateTime ChangeDate { get; set; }
    } 
}
