using NetworkEquipments.Domain.Entities;

namespace NetworkEquipments.Services.DTO
{
    public class StreetDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TownId { get; set; }

        public TownDto Town { get; set; }
    }
}
