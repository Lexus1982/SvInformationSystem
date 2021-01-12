using System.Web.Mvc;
using NetworkEquipments.Domain.ADO;
using NetworkEquipments.Domain.Interfaces;
using NetworkEquipments.Domain.Repositories.Reports;
using NetworkEquipments.Services.Interfaces;
using NetworkEquipments.Services.Services;

namespace NetworkEquipments.Web.Controllers
{
    [Authorize(Roles = "Network Equipments. Администратор, Network Equipments. Пользователь")]
    public class ReportingController : Controller
    {
        private readonly IReportService _reportService;
        private readonly ISectionService _sectionService;
        private readonly IUserService _userService;
        private readonly AdoContext _context;

        public ReportingController(IDatabaseConnectionFactory databaseConnectionFactory)
        {
            _context = new AdoContext(databaseConnectionFactory);
            _reportService = new ReportService(_context);
            _sectionService = new SectionService(_context);
            _userService = new UserService(_context);
        }

        public ActionResult Index()
        {
            // TODO: Реализовать дерево отчетов.
            //var sections =_sectionService.GetUserSections(HttpContext.User.Identity.Name, 2);
            return View();
        }

        public ActionResult GetReportData(int id)
        {
            var section = _sectionService.GetById(id);
            ViewBag.ReportName = section.Name;
            
            var data = _reportService.GetReportData((ReportType) id);
            return View(data);
        }
    }
}


//ActionResult
//ContentResult
//EmptyResult
//FileContentResult
//FilePathResult
//FileResult
//FileStreamResult
//HttpNotFoundResult
//HttpStatusCodeResult
//HttpUnauthorizedResult
//JavaScriptResult
//JsonResult
//ModelValidationResult
//PartialViewResult
//RedirectResult
//RedirectToRouteResult
//ValueProviderResult
//ViewEngineResult
//AsyncResult
//CacheResult
//ValueProviderResult
//TimeMeasureResult
//ComplexModelResult