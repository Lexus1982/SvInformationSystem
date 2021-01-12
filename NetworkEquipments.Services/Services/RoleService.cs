using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NetworkEquipments.Domain.ADO;
using NetworkEquipments.Domain.Entities;
using NetworkEquipments.Domain.Interfaces;
using NetworkEquipments.Domain.Repositories;
using NetworkEquipments.Services.DTO;
using NetworkEquipments.Services.Interfaces;
using NetworkEquipments.Services.Infrastructure;

namespace NetworkEquipments.Services.Services
{
    public class RoleService : IRoleService
    {
        private readonly AdoContext _context;

        public RoleService(IAdoContext context)
        {
            _context = context as AdoContext;
        }

        public IEnumerable<RoleDto> GetRoles()
        {
            return MapRoleModel(new RoleRepository(_context).Get());
        }

        public IEnumerable<RoleDto> GetUserRoles(string login)
        {
            if (string.IsNullOrEmpty(login))
                throw new ValidationException("Не задан логин пользователя", "");

            var user = new UserRepository(_context).FindByLogin(login);
            if (user == null)
                throw new ValidationException($"Пользователь '{login}' не найден", "");

            return MapRoleModel(new RoleRepository(_context).GetByUserId(user.Id));
        }

        public bool IsUserInRole(string login, string roleName)
        {
            if (string.IsNullOrEmpty(login))
                throw new ValidationException("Не задан логин пользователя", "");

            if (string.IsNullOrEmpty(roleName))
                throw new ValidationException("Не задана роль пользователя", "");

            return GetUserRoles(login).Any(r => r.Name.Equals(roleName));
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        private static IEnumerable<RoleDto> MapRoleModel(IEnumerable<Role> roles)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Role, RoleDto>()).CreateMapper();
            return mapper.Map<IEnumerable<Role>, List<RoleDto>>(roles);
        }
    }
}
