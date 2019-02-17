using SegFy.Business.Models;
using SegFy.Classes;
using System;
using System.Web.Mvc;

namespace SegFy.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            CookiesManagement.Close(Resources.Resource.UserCookieName);
            return View();
        }
        
        public JsonResult ValidarLogin(UsuarioModel model)
        {
            Result result = new Result();

            try
            {
                var service = new UsuarioService(new Business.DBContext());
                var user = service.ValidateLogin(model);
                if (user != null)
                {
                    result.setSuccess();
                    result.URL = "/Home";
                    model.ID = user.ID;
                    
                    CookiesManagement.Save(model, Resources.Resource.UserCookieName);
                }
                else
                    result.setError(Resources.Resource.UsuarioSenhaInvalido);

            }
            catch (Exception ex){
                result.setError(Resources.Resource.ErroInesperado);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorization]
        public ActionResult Logout()
        {
            CookiesManagement.Close(Resources.Resource.UserCookieName);
            return Redirect("/Login");
        }
    }
}