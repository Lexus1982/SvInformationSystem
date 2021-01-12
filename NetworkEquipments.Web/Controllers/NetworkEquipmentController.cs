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
using NetworkEquipments.Web.Models.NetworkEquipment;

namespace NetworkEquipments.Web.Controllers
{
    [Authorize(Roles = "Network Equipments. Администратор, Network Equipments. Пользователь")]
    public class NetworkEquipmentController : Controller
    {
        private readonly INetworkService _networkService;
        private readonly INetworkEquipmentService _networkEquipmentService;
        private readonly IAddressService _addressService;
        private readonly ITownService _townService;
        private readonly IStreetService _streetService;
        private readonly IEquipmentTypeService _equipmentTypeService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        private readonly AdoContext _context;

        public NetworkEquipmentController(IDatabaseConnectionFactory databaseConnectionFactory)
        {
            _context = new AdoContext(databaseConnectionFactory);

            _networkService = new NetworkService(_context);
            _networkEquipmentService = new NetworkEquipmentService(_context);
            _addressService = new AddressService(_context);
            _streetService = new StreetService(_context);
            _townService = new TownService(_context);
            _equipmentTypeService = new EquipmentTypeService(_context);
            _userService = new UserService(_context);
            _roleService = new RoleService(_context);
        }

        public ActionResult Index(int? networkId, string search, NetworkEquipmentSortState sort = NetworkEquipmentSortState.IpAsc)
        {
            if (networkId == null) return HttpNotFound();
            var networkEquipmentsModel = MapNetworkEquipmentModel(search.IsEmpty()
                ? _networkEquipmentService.GetByNetworkId((int) networkId)
                : _networkEquipmentService.Search((int)networkId, search));

            var viewModel = new NetworkEquipmentViewModel
            {
                Network = _networkService.GetById((int)networkId),
                NetworkEquipments = SortNetworkEquipmentItems(sort, networkEquipmentsModel),
                Sort = new NetworkEquipmentSortModel(sort),
                Filter = new NetworkEquipmentFilterModel(search)
            };

            // Доступ на запись разрешен только роли Администратор
            ViewBag.IsWriteAccess = _roleService.IsUserInRole(HttpContext.User.Identity.Name, "Network Equipments. Администратор");

            return View(viewModel);
        }

        public ActionResult Create(int? networkId)
        {
            if (networkId == null) return HttpNotFound();
            var network = _networkService.GetById((int) networkId);
            var model = new NetworkEquipmentModel()
            {
                NetworkId = network.Id,
                Entrance = 1,
                Ip = network.IpInterval
            };

            PopulateDropDownLists(6201, 50149, 51191); // г.Кстово, ул. Мира, 21А
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NetworkId,AddressId,TownId,StreetId,Entrance,EquipmentTypeId,Ip,Commentary")] NetworkEquipmentModel networkEquipment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var networkEquipmentDto = MapNetworkEquipmentModel(networkEquipment);
                    networkEquipmentDto.UserId = _userService.GetByLogin(HttpContext.User.Identity.Name).Id;
                    _networkEquipmentService.Create(networkEquipmentDto);

                    return RedirectToAction("Index", new { networkId = networkEquipment.NetworkId});
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.ToString());
                }
            }

            PopulateDropDownLists(networkEquipment.TownId, networkEquipment.StreetId, networkEquipment.AddressId, networkEquipment.EquipmentTypeId);
            return View(networkEquipment);
        }

        public ActionResult Edit(int id)
        {
            var networkEquipmentModel = MapNetworkEquipmentModel(_networkEquipmentService.GetById(id));//MapNetworkEquipmentModel(id);
            PopulateDropDownLists(networkEquipmentModel.TownId, networkEquipmentModel.StreetId, networkEquipmentModel.AddressId, networkEquipmentModel.EquipmentTypeId);
            return View(networkEquipmentModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NetworkId,AddressId,TownId,StreetId,Entrance,EquipmentTypeId,Ip,Commentary")] NetworkEquipmentModel networkEquipment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var networkEquipmentDto = MapNetworkEquipmentModel(networkEquipment);
                    networkEquipmentDto.UserId = _userService.GetByLogin(HttpContext.User.Identity.Name).Id;
                    _networkEquipmentService.Update(networkEquipmentDto);
                    return RedirectToAction("Index", new { networkId = networkEquipment.NetworkId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.ToString());
                }
            }

            PopulateDropDownLists(networkEquipment.TownId, networkEquipment.StreetId, networkEquipment.AddressId, networkEquipment.EquipmentTypeId);
            return View(networkEquipment);
        }

        public ActionResult Delete(int id)
        {
            //return View(MapNetworkEquipmentModel(id));
            return View(MapNetworkEquipmentModel(_networkEquipmentService.GetById(id)));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NetworkEquipmentModel networkEquipment = null;

            try
            {
                networkEquipment = MapNetworkEquipmentModel(_networkEquipmentService.GetById(id));//MapNetworkEquipmentModel(id);
                _networkEquipmentService.Delete(id);
                return RedirectToAction("Index", new { networkId = networkEquipment.NetworkId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(networkEquipment);
            }
        }

        public JsonResult ValidateIpAddress(string ip, string id)
        {
            if (string.IsNullOrEmpty(ip))
                return Json(true, JsonRequestBehavior.AllowGet);

            try
            {
                if (_networkEquipmentService.IsIpAddressExists(ip, id))
                {
                    return Json("Указанный IP адрес уже существует", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("Ошибка проверки IP адреса: " + ex, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _networkService?.Dispose();
                _networkEquipmentService?.Dispose();
                _addressService?.Dispose();
                _townService?.Dispose();
                _streetService?.Dispose();
                _userService?.Dispose();
                _equipmentTypeService?.Dispose();
                _context?.Dispose();
            }

            base.Dispose(disposing);
        }

        private static IEnumerable<NetworkEquipmentModel> SortNetworkEquipmentItems(NetworkEquipmentSortState? sortOrder, List<NetworkEquipmentModel> elements)
        {
            switch (sortOrder)
            {
                case NetworkEquipmentSortState.TownNameAsc:
                    return elements.OrderBy(n => n.TownName);
                case NetworkEquipmentSortState.TownNameDesc:
                    return elements.OrderByDescending(n => n.TownName);
                case NetworkEquipmentSortState.StreetNameAsc:
                    return elements.OrderBy(n => n.StreetName);
                case NetworkEquipmentSortState.StreetNameDesc:
                    return elements.OrderByDescending(n => n.StreetName);
                case NetworkEquipmentSortState.ComplexHouseAsc:
                    return elements.OrderBy(n => n.ComplexHouse);
                case NetworkEquipmentSortState.ComplexHouseDesc:
                    return elements.OrderByDescending(n => n.ComplexHouse);
                case NetworkEquipmentSortState.EntranceAsc:
                    return elements.OrderBy(n => n.Entrance);
                case NetworkEquipmentSortState.EntranceDesc:
                    return elements.OrderByDescending(n => n.Entrance);
                case NetworkEquipmentSortState.EquipmentTypeNameAsc:
                    return elements.OrderBy(n => n.EquipmentTypeName);
                case NetworkEquipmentSortState.EquipmentTypeNameDesc:
                    return elements.OrderByDescending(n => n.EquipmentTypeName);
                case NetworkEquipmentSortState.IpAsc:
                    return elements.OrderBy(n => n.Ip);
                case NetworkEquipmentSortState.IpDesc:
                    return elements.OrderByDescending(n => n.Ip);
                default:
                    return elements;
            }
        }

        private void PopulateDropDownLists(int townId, int streetId, int addressId, int? equipmentTypeId = null)
        {
            PopulateEquipmentTypeDropDownList(equipmentTypeId);
            PopulateTownDropDownList(townId);
            PopulateStreetDropDownList(townId, streetId);
            PopulateAddressDropDownList(streetId, addressId);
        }

        private int? PopulateEquipmentTypeDropDownList(object selectedEquipmentTypeId = null)
        {
            var equipmentTypeDto = _equipmentTypeService.Get().OrderBy(a => a.Name).ToList();
            var selectedId = selectedEquipmentTypeId ?? equipmentTypeDto.FirstOrDefault()?.Id;
            ViewBag.EquipmentTypeId = new SelectList(equipmentTypeDto, "Id", "Name", selectedEquipmentTypeId);

            return (int?)selectedId;
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

        //private NetworkEquipmentModel MapNetworkEquipmentModel(int id)
        //{
        //    var networkEquipmentDto = _networkEquipmentService.GetById(id);
        //    return MapNetworkEquipmentModel(networkEquipmentDto);
        //}

        private static NetworkEquipmentDto MapNetworkEquipmentModel(NetworkEquipmentModel networkEquipmentModel)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<NetworkEquipmentModel, NetworkEquipmentDto>()).CreateMapper();
            return mapper.Map<NetworkEquipmentModel, NetworkEquipmentDto>(networkEquipmentModel);
        }

        private static NetworkEquipmentModel MapNetworkEquipmentModel(NetworkEquipmentDto networkEquipmentDto)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<NetworkEquipmentDto, NetworkEquipmentModel>()
                .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => src.Address.Id))
                .ForMember(dest => dest.TownId, opt => opt.MapFrom(src => src.Address.Street.Town.Id))
                .ForMember(dest => dest.StreetId, opt => opt.MapFrom(src => src.Address.Street.Id))
                .ForMember(dest => dest.ComplexHouse, opt => opt.MapFrom(src => src.Address.ComplexHouse))
                .ForMember(dest => dest.StreetName, opt => opt.MapFrom(src => src.Address.Street.Name))
                .ForMember(dest => dest.TownName, opt => opt.MapFrom(src => src.Address.Street.Town.Name))
                .ForMember(dest => dest.EquipmentTypeId, opt => opt.MapFrom(src => src.EquipmentType.Id))
                .ForMember(dest => dest.EquipmentTypeName, opt => opt.MapFrom(src => src.EquipmentType.Name))
            ).CreateMapper();

            return mapper.Map<NetworkEquipmentDto, NetworkEquipmentModel>(networkEquipmentDto);
        }

        private static List<NetworkEquipmentModel> MapNetworkEquipmentModel(IEnumerable<NetworkEquipmentDto> networksEquipmentDto)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<NetworkEquipmentDto, NetworkEquipmentModel>()
                .ForMember(dest => dest.ComplexHouse, opt => opt.MapFrom(src => src.Address.ComplexHouse))
                .ForMember(dest => dest.StreetName, opt => opt.MapFrom(src => src.Address.Street.Name))
                .ForMember(dest => dest.TownName, opt => opt.MapFrom(src => src.Address.Street.Town.Name))
                .ForMember(dest => dest.EquipmentTypeName, opt => opt.MapFrom(src => src.EquipmentType.Name))
            ).CreateMapper();

            return mapper.Map<IEnumerable<NetworkEquipmentDto>, List<NetworkEquipmentModel>>(networksEquipmentDto);
        }
    }
}
