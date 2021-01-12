using System;
using System.Collections.Generic;
using System.Text;
using NetworkEquipments.Domain.Entities;
using NetworkEquipments.Services.DTO;

namespace NetworkEquipments.Services.Interfaces
{
    public interface IUserService : IDisposable
    {
        IEnumerable<UserDto> Get();
        UserDto GetByLogin(string login);
        bool Login(string login, string password);
    }
}
