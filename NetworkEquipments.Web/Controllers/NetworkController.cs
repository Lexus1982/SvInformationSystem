using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;
using AutoMapper;
using NetworkEquipments.Domain.ADO;
using NetworkEquipments.Domain.Interfaces;
using NetworkEquipments.Services.DTO;
using NetworkEquipments.Services.Interfaces;
using NetworkEquipments.Services.Services;
using NetworkEquipments.Web.Models.Network;

namespace NetworkEquipments.Web.Controllers
{
    [Authorize(Roles = "Network Equipments. Администратор, Network Equipments. Пользователь")]
    public class NetworkController : Controller
    {
        private readonly INetworkService _networkService;
        private readonly IAddressService _addressService;
        private readonly ITownService _townService;
        private readonly IStreetService _streetService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        //private readonly IDatabaseConnectionFactory _databaseConnectionFactory;
        private readonly AdoContext _context;

        public NetworkController(IDatabaseConnectionFactory databaseConnectionFactory)
        {
            //_databaseConnectionFactory = databaseConnectionFactory;
            //_addressService = new AddressService(_databaseConnectionFactory);
            _context = new AdoContext(databaseConnectionFactory);

            _networkService = new NetworkService(_context);
            _addressService = new AddressService(_context);
            _streetService = new StreetService(_context);
            _townService = new TownService(_context);
            _userService = new UserService(_context);
            _roleService = new RoleService(_context);
        }

       public ActionResult Index(string search, NetworkSortState sort = NetworkSortState.IpIntervalAsc)
        {
            var networksModel = MapNetworkModel(search.IsEmpty() ? _networkService.Get() : _networkService.Search(search));
            var viewModel = new NetworkViewModel
            {
                Networks = SortNetworkItems(sort, networksModel),
                Sort = new NetworkSortModel(sort),
                Filter = new NetworkFilterModel(search)
            };

            // Доступ на запись разрешен только роли Администратор
            ViewBag.IsWriteAccess = _roleService.IsUserInRole(HttpContext.User.Identity.Name, "Network Equipments. Администратор");
            return View(viewModel);
        }

       public ActionResult Create()
        {
            var model = new NetworkModel()
            {
            };

            PopulateDropDownLists(6201,50149, 51191); // г.Кстово, ул. Мира, 21А
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AddressId,TownId,StreetId,SegmentNumber,VlanManage,VlanInternet,IpInterval,Commentary")] NetworkModel network)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var networkDto = MapNetworkModel(network);
                    networkDto.UserId = _userService.GetByLogin(HttpContext.User.Identity.Name).Id;
                    _networkService.Create(networkDto);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.ToString());
                }
            }

            PopulateDropDownLists(network.TownId, network.StreetId, network.AddressId);
            return View(network);
        }

        public ActionResult Edit(int id)
        {
            var networkModel = MapNetworkModel(_networkService.GetById(id));
            PopulateDropDownLists(networkModel.TownId, networkModel.StreetId, networkModel.AddressId);
            return View(networkModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AddressId,TownId,StreetId,SegmentNumber,VlanManage,VlanInternet,IpInterval,Commentary")] NetworkModel network)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var networkDto = MapNetworkModel(network);
                    networkDto.UserId = _userService.GetByLogin(HttpContext.User.Identity.Name).Id;
                    _networkService.Update(networkDto);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.ToString());
                }
            }

            PopulateDropDownLists(network.TownId, network.StreetId, network.AddressId);
            return View(network);
        }

        public ActionResult Delete(int id)
        {
            return View(MapNetworkModel(_networkService.GetById(id)));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NetworkModel network = null;

            try
            {
                network = MapNetworkModel(_networkService.GetById(id));
                if(network.EquipmentsCount > 0)
                    throw new Exception("В сегменте найдены данные по оборудованию. Удаление невозможно.");

                _networkService.Delete(id);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(network);
            }
        }

        public JsonResult ValidateIpInterval(string IpInterval, string Id)
        {
            if (string.IsNullOrEmpty(IpInterval))
                return Json(true, JsonRequestBehavior.AllowGet);

            try
            {
                if (_networkService.IsIpIntervalExists(IpInterval, Id))
                {
                    return Json("Указанный диапазон IP уже существует", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("Ошибка проверки диапазона IP: " + ex, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _networkService?.Dispose();
                _addressService?.Dispose();
                _townService?.Dispose();
                _streetService?.Dispose();
                _userService?.Dispose();

                _context?.Dispose();
            }

            base.Dispose(disposing);
        }

        private static IEnumerable<NetworkModel> SortNetworkItems(NetworkSortState? sortOrder, List<NetworkModel> elements)
        {
            switch (sortOrder)
            {
                case NetworkSortState.TownNameAsc:
                    return elements.OrderBy(n => n.TownName);
                case NetworkSortState.TownNameDesc:
                    return elements.OrderByDescending(n => n.TownName);
                case NetworkSortState.StreetNameAsc:
                    return elements.OrderBy(n => n.StreetName);
                case NetworkSortState.StreetNameDesc:
                    return elements.OrderByDescending(n => n.StreetName);
                case NetworkSortState.ComplexHouseAsc:
                    return elements.OrderBy(n => n.ComplexHouse);
                case NetworkSortState.ComplexHouseDesc:
                    return elements.OrderByDescending(n => n.ComplexHouse);
                case NetworkSortState.SegmentNumberAsc:
                    return elements.OrderBy(n => n.SegmentNumber);
                case NetworkSortState.SegmentNumberDesc:
                    return elements.OrderByDescending(n => n.SegmentNumber);
                case NetworkSortState.VlanManageAsc:
                    return elements.OrderBy(n => n.VlanManage);
                case NetworkSortState.VlanManageDesc:
                    return elements.OrderByDescending(n => n.VlanManage);
                case NetworkSortState.VlanInternetAsc:
                    return elements.OrderBy(n => n.VlanInternet);
                case NetworkSortState.VlanInternetDesc:
                    return elements.OrderByDescending(n => n.VlanInternet);
                case NetworkSortState.IpIntervalAsc:
                    return elements.OrderBy(n => n.IpInterval);
                case NetworkSortState.IpIntervalDesc:
                    return elements.OrderByDescending(n => n.IpInterval);
                case NetworkSortState.EquipmentsCountAsc:
                    return elements.OrderBy(n => n.EquipmentsCount);
                case NetworkSortState.EquipmentsCountDesc:
                    return elements.OrderByDescending(n => n.EquipmentsCount);
                default:
                    return elements;
            }
        }

        private void PopulateDropDownLists(int townId, int streetId, int addressId)
        {
            PopulateTownDropDownList(townId);
            PopulateStreetDropDownList(townId, streetId);
            PopulateAddressDropDownList(streetId, addressId);
        }

        private int? PopulateTownDropDownList(object selectedTownId = null)
        {
            var townDto = _townService.Get().OrderBy(a => a.Name).ToList();
            var selectedId = selectedTownId ?? townDto.FirstOrDefault()?.Id;
            ViewBag.TownId = new SelectList(townDto, "Id", "Name", selectedId);

            return (int?)selectedId;
        }

        private int? PopulateStreetDropDownList(int townId, object selectedStreetId = null)
        {
            var streetsDto = _streetService.GetByTownId(townId).OrderBy(a => a.Name).ToList();
            var selectedId = selectedStreetId ?? streetsDto.FirstOrDefault()?.Id;
            ViewBag.StreetId = new SelectList(streetsDto, "Id", "Name", selectedId);

            return (int?)selectedId;
        }

        private int? PopulateAddressDropDownList(int streetId, object selectedAddressId = null)
        {
            var addressDto = _addressService.GetByStreetId(streetId).OrderBy(a => a.ComplexHouse).ToList();
            var selectedId = selectedAddressId ?? addressDto.FirstOrDefault()?.Id;
            ViewBag.AddressId = new SelectList(addressDto, "Id", "ComplexHouse", selectedId);

            return (int?)selectedId;
        }

        private static NetworkDto MapNetworkModel(NetworkModel networkModel)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<NetworkModel, NetworkDto>()).CreateMapper();
            return mapper.Map<NetworkModel, NetworkDto>(networkModel);
        }

        private static NetworkModel MapNetworkModel(NetworkDto networkDto)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<NetworkDto, NetworkModel>()
                .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => src.Address.Id))
                .ForMember(dest => dest.TownId, opt => opt.MapFrom(src => src.Address.Street.Town.Id))
                .ForMember(dest => dest.StreetId, opt => opt.MapFrom(src => src.Address.Street.Id))
                .ForMember(dest => dest.ComplexHouse, opt => opt.MapFrom(src => src.Address.ComplexHouse))
                .ForMember(dest => dest.StreetName, opt => opt.MapFrom(src => src.Address.Street.Name))
                .ForMember(dest => dest.TownName, opt => opt.MapFrom(src => src.Address.Street.Town.Name))
            ).CreateMapper();

            return mapper.Map<NetworkDto, NetworkModel>(networkDto);
        }

        private static List<NetworkModel> MapNetworkModel(IEnumerable<NetworkDto> networksDto)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<NetworkDto, NetworkModel>()
                .ForMember(dest => dest.ComplexHouse, opt => opt.MapFrom(src => src.Address.ComplexHouse))
                .ForMember(dest => dest.StreetName, opt => opt.MapFrom(src => src.Address.Street.Name))
                .ForMember(dest => dest.TownName, opt => opt.MapFrom(src => src.Address.Street.Town.Name))
            ).CreateMapper();

            return mapper.Map<IEnumerable<NetworkDto>, List<NetworkModel>>(networksDto);
        }
    }
}
