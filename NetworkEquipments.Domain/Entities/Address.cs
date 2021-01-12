namespace NetworkEquipments.Domain.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string House { get; set; }
        public string Corp { get; set; }
        public int StreetId { get; set; }

        //public Street Street { get; set; }

        //public Address()
        //{
        //    Street = new Street();
        //}
    }
}
