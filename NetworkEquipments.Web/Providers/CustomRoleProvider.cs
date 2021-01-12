using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using NetworkEquipments.Domain.ADO;
using NetworkEquipments.Domain.Interfaces;
using NetworkEquipments.Services.Interfaces;
using NetworkEquipments.Services.Services;
using Ninject;

namespace NetworkEquipments.Web.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        //TODO: Реализовать через IoC контейнер
        //[Inject]
        //public IDatabaseConnectionFactory Factory { private get; set; }
        
        //private IDatabaseConnectionFactory _factory;
        //private readonly AdoContext _context;

        
        public CustomRoleProvider(/*IDatabaseConnectionFactory factory*/)
        {
            ////_factory = factory;
            ////_factory = (IDatabaseConnectionFactory)DependencyResolver.Current.GetService(typeof(IDatabaseConnectionFactory));
            //var connection = ConfigurationManager.ConnectionStrings["SybaseConnection"];
            //_factory = new DatabaseConnectionFactory(connection.ConnectionString, connection.ProviderName);
            ////_context = new AdoContext(_factory);
        }

        //[Inject]
        //public void SetFactory(IDatabaseConnectionFactory factory)
        //{
        //    _factory = factory;
        //}

        //    [Inject]
        //public IAdoContext Context { get; set; }

        //public CustomRoleProvider(IAdoContext context)
        //{
        //    _context = context;

        //}

        public override string ApplicationName { get; set; }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException(); 
        }

        public override string[] GetRolesForUser(string login)
        {
            var connection = ConfigurationManager.ConnectionStrings["SybaseConnection"];
            var factory = new DatabaseConnectionFactory(connection.ConnectionString, connection.ProviderName);

            using (var context = new AdoContext(factory))
            {
                IRoleService roleServices = new RoleService(context);
                var roles = roleServices.GetUserRoles(login);
                return roles?.Select(r => r.Name).ToArray();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //_context?.Dispose();
        }
    }
}