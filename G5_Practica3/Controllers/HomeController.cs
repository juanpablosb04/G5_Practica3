using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G5_Practica3.EF;
using G5_Practica3.Models;

namespace G5_Practica3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

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

        public ActionResult Lista()
        {
            using (var dbContext = new PracticaS12Entities())
            {
                var lista = dbContext.Principal
                .OrderByDescending(p => p.Estado == "Pendiente")
                .ThenBy(p => p.Id_Compra)
                .Select(p => new Models.Principal
                {
                    Id_Compra = (int)p.Id_Compra,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    Saldo = p.Saldo,
                    Estado = p.Estado
                })
                .ToList();

                return View(lista);
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