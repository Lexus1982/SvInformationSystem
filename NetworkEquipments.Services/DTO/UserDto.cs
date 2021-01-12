using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkEquipments.Services.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int? GroupId { get; set; }
        public string Active { get; set; }

        public IEnumerable<RoleDto> Roles { get; set; }
    }
}
