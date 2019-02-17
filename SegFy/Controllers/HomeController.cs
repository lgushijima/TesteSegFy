using SegFy.Classes;
using System.Web.Mvc;

namespace SegFy.Controllers
{
    public class HomeController : Controller
    {
        [Authorization]
        public ActionResult Index()
        {
            return View();
        }

    }
}