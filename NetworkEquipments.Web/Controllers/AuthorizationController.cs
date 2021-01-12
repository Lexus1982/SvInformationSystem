using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using NetworkEquipments.Domain.ADO;
using NetworkEquipments.Domain.Interfaces;
using NetworkEquipments.Services.DTO;
using NetworkEquipments.Services.Interfaces;
using NetworkEquipments.Services.Services;
using NetworkEquipments.Web.Models;

namespace NetworkEquipments.Web.Controllers
{
    [Authorize]
    public class AuthorizationController : Controller
    {
        private readonly IUserService _userService;
        

        //public AuthorizationController(IUserService userService)
        //{
        //    _userService = userService;
        //}

        private readonly AdoContext _context;

        public AuthorizationController(IDatabaseConnectionFactory databaseConnectionFactory)
        {
            //_databaseConnectionFactory = databaseConnectionFactory;
            //_addressService = new AddressService(_databaseConnectionFactory);
            _context = new AdoContext(databaseConnectionFactory);
            _userService = new UserService(_context);
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Login,Password")] UserLogin model)
        {
            if (ModelState.IsValid)
            {
                //var userServices = new UserService(_context);
                if (_userService.Login(model.Login, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Login, true);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Пользователь с таким логином или паролем не существует");
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult AccessDeny()
        {
            return View();
        }
    }
}