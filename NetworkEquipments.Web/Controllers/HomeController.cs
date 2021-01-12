using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NetworkEquipments.Domain.ADO;
using NetworkEquipments.Domain.Entities;
using NetworkEquipments.Domain.Interfaces;
using NetworkEquipments.Domain.Repositories;
using NetworkEquipments.Services.Interfaces;
using NetworkEquipments.Services.Services;
using AutoMapper;
using Ninject;

namespace NetworkEquipments.Web.Controllers
{
    //public class OuterSource
    //{
    //    public int Value { get; set; }
    //    public InnerSource Inner { get; set; }
    //}

    //public class InnerSource
    //{
    //    public int OtherValue { get; set; }
    //}
    ////-------------------------
    //public class OuterDest
    //{
    //    public int Value { get; set; }
    //    public InnerDest Inner { get; set; }
    //}

    //public class InnerDest
    //{
    //    public int OtherValue { get; set; }
    //}

    [Authorize(Roles = "Network Equipments. Администратор, Network Equipments. Пользователь")]
    public class HomeController : Controller
    {
        private IDatabaseConnectionFactory _factory;
        //private readonly IAdoContext _context;


        //private readonly INetworkService _networkService;

        //public HomeController(INetworkService networkService)
        //{
        //    _networkService = networkService;
        //}

        //private readonly IDatabaseConnectionFactory _databaseConnectionFactory;
        //private readonly AdoContext _context;

        [Inject]
        public IDatabaseConnectionFactory Factory { private get; set; }

        //private readonly IMapper _mapper;
        public HomeController()
        {
            //IDatabaseConnectionFactory factory = Factory;
            //_databaseConnectionFactory = databaseConnectionFactory;
            //_addressService = new AddressService(_databaseConnectionFactory);
            //_context = new AdoContext(databaseConnectionFactory);
        }

        public ActionResult Index()
        {
            //_factory = Factory;

            //var config = new MapperConfiguration(cfg => {
            //    cfg.CreateMap<OuterSource, OuterDest>();
            //cfg.CreateMap<InnerSource, InnerDest>();
        //});
        //config.AssertConfigurationIsValid();

        //var source = new OuterSource
        //{
        //    Value = 5,
        //    Inner = new InnerSource { OtherValue = 15 }
        //};
        //var mapper = config.CreateMapper();
        //var dest = mapper.Map<OuterSource, OuterDest>(source);

        //dest.Value.ShouldEqual(5);
        //dest.Inner.ShouldNotBeNull();
        //dest.Inner.OtherValue.ShouldEqual(15);

        //var factory = new DatabaseConnectionFactory("SybaseConnection");
            //var context = new DatabaseContext(factory);

            //using (var uow = context.CreateUnitOfWork())
            //{
            //    var repo = new NetworkRepository(context);
            //    var networksList = repo.Get();

            //    // do changes
            //    // [...]

            //    //uow.SaveChanges();
            //}
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               //_context?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}