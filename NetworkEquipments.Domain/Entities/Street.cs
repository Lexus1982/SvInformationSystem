using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkEquipments.Domain.Entities
{
    public class Street
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TownId { get; set; }

        //public Town Town { get; set; }

        //public Street()
        //{
        //    Town = new Town();
        //}
    }
}
