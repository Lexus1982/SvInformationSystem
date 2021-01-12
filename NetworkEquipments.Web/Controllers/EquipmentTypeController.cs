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
using NetworkEquipments.Web.Models.EquipmentType;

namespace NetworkEquipments.Web.Controllers
{
    [Authorize(Roles = "Network Equipments. Администратор")]
    public class EquipmentTypeController : Controller
    {
        private readonly IEquipmentTypeService _equipmentTypeService;
        private readonly IUserService _userService;
        private readonly AdoContext _context;

        public EquipmentTypeController(IDatabaseConnectionFactory databaseConnectionFactory)
        {
            _context = new AdoContext(databaseConnectionFactory);

            _equipmentTypeService = new EquipmentTypeService(_context);
            _userService = new UserService(_context);
        }

        public ActionResult Index(string search, EquipmentTypeSortState sort = EquipmentTypeSortState.PositionAsc)
        {
            var equipmentTypeModel = MapEquipmentTypeModel(search.IsEmpty() ? _equipmentTypeService.Get() : _equipmentTypeService.Search(search));
            var viewModel = new EquipmentTypeViewModel
            {
                EquipmentType = SortEquipmentTypeItems(sort, equipmentTypeModel),
                Sort = new EquipmentTypeSortModel(sort),
                Filter = new EquipmentTypeFilterModel(search)
            };

            return View(viewModel);
        }

        public ActionResult Create()
        {
            var model = new EquipmentTypeModel()
            {
                Position = 1
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Position")] EquipmentTypeModel equipmentType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var equipmentTypeDto = MapEquipmentTypeDto(equipmentType);
                    equipmentTypeDto.UserId = _userService.GetByLogin(HttpContext.User.Identity.Name).Id;
                    _equipmentTypeService.Create(equipmentTypeDto);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.ToString());
                }
            }

            return View(equipmentType);
        }

        public ActionResult Edit(int id)
        {
            var equipmentTypeModel = MapEquipmentTypeModel(_equipmentTypeService.GetById(id));
            return View(equipmentTypeModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Position")] EquipmentTypeModel equipmentTypeModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var equipmentTypeDto = MapEquipmentTypeDto(equipmentTypeModel);
                    equipmentTypeDto.UserId = _userService.GetByLogin(HttpContext.User.Identity.Name).Id;
                    _equipmentTypeService.Update(equipmentTypeDto);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.ToString());
                }
            }

            return View(equipmentTypeModel);
        }

        public ActionResult Delete(int id)
        {
            var equipmentTypeModel = MapEquipmentTypeModel(_equipmentTypeService.GetById(id));
            return View(equipmentTypeModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _equipmentTypeService.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.ToString());
                var equipmentTypeModel = MapEquipmentTypeModel(_equipmentTypeService.GetById(id));
                return View(equipmentTypeModel);
            }
        }

        public JsonResult ValidateEquipmentTypeName(string Name, string Id)
        {
            if (string.IsNullOrEmpty(Name))
                return Json(true, JsonRequestBehavior.AllowGet);

            try
            {
                if (_equipmentTypeService.IsTypeNameExists(Name, Id))
                {
                    return Json("Указанный тип уже существует", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("Ошибка проверки наименования типа оборудования: " + ex, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _equipmentTypeService?.Dispose();
                _userService?.Dispose();
                _context?.Dispose();
            }

            base.Dispose(disposing);
        }

        private IEnumerable<EquipmentTypeModel> SortEquipmentTypeItems(EquipmentTypeSortState? sortOrder, List<EquipmentTypeModel> elements)
        {
            switch (sortOrder)
            {
                case EquipmentTypeSortState.NameAsc:
                    return elements.OrderBy(n => n.Name);
                case EquipmentTypeSortState.NameDesc:
                    return elements.OrderByDescending(n => n.Name);
                case EquipmentTypeSortState.PositionDesc:
                    return elements.OrderByDescending(n => n.Name);
                default:
                    return elements.OrderBy(n => n.Position);
            }
        }

        private List<EquipmentTypeModel> MapEquipmentTypeModel(IEnumerable<EquipmentTypeDto> equipmentTypeDto)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EquipmentTypeDto, EquipmentTypeModel>()).CreateMapper();
            var equipmentTypeModel = mapper.Map<IEnumerable<EquipmentTypeDto>, List<EquipmentTypeModel>>(equipmentTypeDto);
            return equipmentTypeModel;
        }

        private EquipmentTypeModel MapEquipmentTypeModel(EquipmentTypeDto equipmentTypeDto)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EquipmentTypeDto, EquipmentTypeModel>()).CreateMapper();
            return mapper.Map<EquipmentTypeDto, EquipmentTypeModel>(equipmentTypeDto);
        }

        private EquipmentTypeDto MapEquipmentTypeDto(EquipmentTypeModel equipmentTypeModel)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EquipmentTypeModel, EquipmentTypeDto>()).CreateMapper();
            return mapper.Map<EquipmentTypeModel, EquipmentTypeDto>(equipmentTypeModel);
        }
    }
}
