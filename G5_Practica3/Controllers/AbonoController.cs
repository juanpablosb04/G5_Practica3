using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G5_Practica3.EF;

namespace G5_Practica3.Controllers
{
    public class AbonoController : Controller
    {
        public ActionResult Registro()
        {
            using (var db = new PracticaS12Entities())
            {
                ViewBag.ComprasPendientes = db.Principal
                    .Where(c => c.Estado == "Pendiente")
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id_Compra.ToString(),
                        Text = c.Descripcion
                    })
                    .ToList();

                return View();
            }

        }

        public JsonResult ObtenerSaldo(int idCompra)
        {
            using (var db = new PracticaS12Entities())
            {
                var saldo = db.Principal
                              .Where(c => c.Id_Compra == idCompra)
                              .Select(c => c.Saldo)
                              .FirstOrDefault();

                return Json(saldo, JsonRequestBehavior.AllowGet);
            }
        }
    }
}