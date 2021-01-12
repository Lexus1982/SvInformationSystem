using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using NetworkEquipments.Domain.ADO;
using NetworkEquipments.Domain.Interfaces;
using NetworkEquipments.Web.Infrastructure;
using Ninject;
using Ninject.Modules;

namespace NetworkEquipments.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //private IDatabaseConnectionFactory _factory;
        //public static IKernel kernel;
        protected void Application_Start()
        {
            //var connection = ConfigurationManager.ConnectionStrings["SybaseConnection"];
            //var factory = new DatabaseConnectionFactory(connection.ConnectionString, connection.ProviderName);
            ////NinjectModule registrations = new NinjectRegistrations(_factory);
            ////kernel = new StandardKernel(registrations);
            ////DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
            ////DependencyResolver.SetResolver(new NinjectDependencyResolver(factory));

            DependencyResolver.SetResolver(new NinjectDependencyResolver());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
    }
}
