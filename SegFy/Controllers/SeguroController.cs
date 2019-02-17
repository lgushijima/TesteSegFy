using SegFy.Business.Models;
using SegFy.Classes;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SegFy.Controllers
{
    public class SeguroController : Controller
    {
        [Authorization]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListarGrid(Grid.Request request, SeguroModel model)
        {
            Grid.Result<object> gridModel = new Grid.Result<object>();

            try
            {
                var service = new SeguroService(new Business.DBContext());
                var list = service.GetAll(model);

                gridModel.total = list.Count();
                gridModel.list = list.ToList<object>();
            }
            catch (Exception ex)
            {
                gridModel.error = true;
                gridModel.errorCode = 999;
                gridModel.message = ex.Message;
            }

            return Json(gridModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Salvar(SeguroModel model)
        {
            Result result = new Result();

            try
            {
                var service = new SeguroService(new Business.DBContext());
                if (model.ID==0)
                    service.Insert(model);
                else
                    service.Update(model);

                result.setSuccess();
            }
            catch (Exception ex)
            {
                result.setError(ex.Message);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Excluir(SeguroModel model)
        {
            Result result = new Result();

            try
            {
                var service = new SeguroService(new Business.DBContext());
                service.Delete(model.ID);
            }
            catch (Exception ex)
            {
                result.setError(ex.Message);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}