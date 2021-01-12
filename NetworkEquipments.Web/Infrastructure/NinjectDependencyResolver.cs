using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using NetworkEquipments.Domain.ADO;
using NetworkEquipments.Domain.Interfaces;
using NetworkEquipments.Web.Providers;
using Ninject;
using Ninject.Web.Common;

namespace NetworkEquipments.Web.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;
        //private readonly IDatabaseConnectionFactory _connectionFactory;
        private readonly ConnectionStringSettings _settingsCollection;

        public NinjectDependencyResolver()
        {
            _settingsCollection = ConfigurationManager.ConnectionStrings["SybaseConnection"];
            kernel = new StandardKernel();
            //kernel.Inject(Roles.Provider);
            //var kernel = (new Bootstrapper()).Kernel;
            //kernel.Inject(Roles.Provider);
            AddBindings();
        }

        //public NinjectDependencyResolver(IDatabaseConnectionFactory connectionFactory)
        //{
        //    _connectionFactory = connectionFactory;
        //    kernel = new StandardKernel();
        //    AddBindings();
        //}

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _settingsCollection = ConfigurationManager.ConnectionStrings["SybaseConnection"];
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {

            //kernel.Bind<IDatabaseConnectionFactory>().To<DatabaseConnectionFactory>()
            //    .WithConstructorArgument("connectionString", _settingsCollection.ConnectionString)
            //    .WithConstructorArgument("providerName", _settingsCollection.ProviderName);

            //kernel.Bind<IUserService>().To<UserService>().WithConstructorArgument("connectionFactory", _connectionFactory);
            //kernel.Bind<INetworkService>().To<NetworkService>().WithConstructorArgument("connectionFactory", _connectionFactory);
            //kernel.Bind<IAddressService>().To<AddressService>().WithConstructorArgument("connectionFactory", _connectionFactory);
            //kernel.Bind<IStreetService>().To<StreetService>().WithConstructorArgument("connectionFactory", _connectionFactory);
            //kernel.Bind<ITownService>().To<TownService>().WithConstructorArgument("connectionFactory", _connectionFactory);

             kernel.Bind<IDatabaseConnectionFactory>().To<DatabaseConnectionFactory>().InRequestScope()
                .WithConstructorArgument("connectionString", _settingsCollection.ConnectionString)
                .WithConstructorArgument("providerName", _settingsCollection.ProviderName);

             kernel.Bind<CustomRoleProvider>().ToSelf();

            //kernel.Bind<RoleProvider>().To<CustomRoleProvider>();


            //kernel.AddBinding(new Binding());
            //kernel.Bind<IAdoContext>().To<AdoContext>().WithConstructorArgument("connectionFactory", _connectionFactory);
        }
    }
}