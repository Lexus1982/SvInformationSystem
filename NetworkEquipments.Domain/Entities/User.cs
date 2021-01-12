using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkEquipments.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public decimal? GroupId { get; set; }
        public string Active { get; set; }

        //public IEnumerable<Role> Roles { get; set; }

        //public User()
        //{
        //    Roles = new List<Role>();
        //}
    }
}
