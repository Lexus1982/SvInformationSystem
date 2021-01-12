using System.Web.Mvc;
using NetworkEquipments.Web.Infrastructure;

namespace NetworkEquipments.Web.Controllers
{
    //[Authorize(Roles = "Network Equipments. Администратор")]
    [AdminAccessAuth]
    public class AdministratingController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
