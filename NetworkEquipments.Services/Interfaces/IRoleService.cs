using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkEquipments.Domain.Entities;
using NetworkEquipments.Services.DTO;

namespace NetworkEquipments.Services.Interfaces
{
    public interface IRoleService : IDisposable
    {
        IEnumerable<RoleDto> GetRoles();
        IEnumerable<RoleDto> GetUserRoles(string login);
        bool IsUserInRole(string login, string roleName);
    }
}
