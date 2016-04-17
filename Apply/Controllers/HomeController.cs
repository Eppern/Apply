using System.Web.Mvc;

namespace Apply.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Run this only like a seed
            //UserHelpers.CreateAspNetRoles();
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}