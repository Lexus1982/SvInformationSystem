using System.Web.Mvc;

namespace NetworkEquipments.Web.Infrastructure
{
    public class AdminAccessAuthAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool auth = filterContext.HttpContext.User.IsInRole("Network Equipments. Администратор");
            if (!auth)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary {
                        { "controller", "Authorization" }, { "action", "AccessDeny" }
                    });
            }
        }
    }
}