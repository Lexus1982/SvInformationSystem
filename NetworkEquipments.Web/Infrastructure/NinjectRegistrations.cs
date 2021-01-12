using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using NetworkEquipments.Domain.ADO;
using NetworkEquipments.Domain.Interfaces;
using NetworkEquipments.Services.Interfaces;
using NetworkEquipments.Services.Sevices;
using NetworkEquipments.Web.Providers;
using Ninject.Modules;

namespace NetworkEquipments.Web.Infrastructure
{
    public class NinjectRegistrations : NinjectModule
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;

        public NinjectRegistrations(IDatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public override void Load()
        {
           // Bind<IRoleServices>().To<RoleServices>().WithConstructorArgument("connectionFactory", _factory);
            

            //Bind<RoleProvider>().To<CustomRoleProvider>().WithConstructorArgument("connectionFactory", _factory);//.InRequestScope();
            //Bind<IDatabaseConnectionFactory>().To<DatabaseConnectionFactory>();
            Bind<IAdoContext>().To<AdoContext>().WithConstructorArgument("connectionFactory", _connectionFactory);
            //Bind<IAdoContext>().To<AdoContext>().WithPropertyValue("Factory", _factory);

            Bind<INetworkService>().To<NetworkService>().WithConstructorArgument("connectionFactory", _connectionFactory);
        }

        
    }
}