using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NetworkEquipments.Domain.ADO;
using NetworkEquipments.Domain.Interfaces;
using NetworkEquipments.Services.Interfaces;
using NetworkEquipments.Services.Services;

namespace NetworkEquipments.Web.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;
        private readonly IStreetService _streetService;
        private readonly IUserService _userService;
        private readonly AdoContext _context;

        public AddressController(IDatabaseConnectionFactory databaseConnectionFactory)
        {
            _context = new AdoContext(databaseConnectionFactory);
            _streetService = new StreetService(_context);
            _addressService = new AddressService(_context);
            _userService = new UserService(_context);
        }

        public ActionResult GetStreets(int id)
        {
            var streetsDto = _streetService.GetByTownId(id).OrderBy(a => a.Name).ToList();
            return PartialView(streetsDto);
        }

        public ActionResult GetAddresses(int id)
        {
            var addressesDto = _addressService.GetByStreetId(id).OrderBy(a => a.ComplexHouse).ToList();
            return PartialView(addressesDto);
        }
    }
}