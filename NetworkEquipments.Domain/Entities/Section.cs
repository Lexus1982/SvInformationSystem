namespace NetworkEquipments.Domain.Entities
{
    public class Section
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public string Position { get; set; }
    }
}
