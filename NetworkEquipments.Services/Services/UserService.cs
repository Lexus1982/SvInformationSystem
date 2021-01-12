using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using NetworkEquipments.Domain.ADO;
using NetworkEquipments.Domain.Entities;
using NetworkEquipments.Domain.Interfaces;
using NetworkEquipments.Domain.Repositories;
using NetworkEquipments.Services.DTO;
using NetworkEquipments.Services.Infrastructure;
using NetworkEquipments.Services.Interfaces;

namespace NetworkEquipments.Services.Services
{
    public class UserService : IUserService
    {
        //private readonly IDatabaseConnectionFactory _connectionFactory;
        private readonly AdoContext _context;

        //public UserService(IDatabaseConnectionFactory connectionFactory)
        //{
        //    _connectionFactory = connectionFactory;
        //    _context = new AdoContext(_connectionFactory);
        //}

        public UserService(IAdoContext context)
        {
            _context = context as AdoContext;
        }

        public IEnumerable<UserDto> Get()
        {
            return MapUserModel(new UserRepository(_context).Get());
        }

        public UserDto GetByLogin(string login)
        {
            if (login == null)
                throw new ValidationException("Не задан логин пользователя", "");

            var user = new UserRepository(_context).FindByLogin(login);
            if (user == null)
                throw new ValidationException("Пользователь не найден", "");

            return MapUserModel(user);
        }

        public bool Login(string login, string password)
        {
            if (login == null)
                throw new ValidationException("Не задан логин пользователя", "");

            var user = GetByLogin(login);
            return user != null && user.Password.Equals(password);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        private static IEnumerable<UserDto> MapUserModel(IEnumerable<User> users)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDto>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, List<UserDto>>(users);
        }

        private static UserDto MapUserModel(User user)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDto>()).CreateMapper();
            return mapper.Map<User, UserDto>(user);
        }
    }
}
