using System;

namespace NetworkEquipments.Domain.Entities
{
    public class EquipmentType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public int UserId { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}
