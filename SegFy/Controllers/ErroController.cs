using System.Web.Mvc;

namespace SegFy.Controllers
{
    public class ErroController : Controller
    {
        public ActionResult PaginaNaoEncontrada()
        {
            return View("Erro404");
        }
        public ActionResult ErroInesperado()
        {
            return View("Erro500");
        }
    }
}