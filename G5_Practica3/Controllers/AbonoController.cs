using G5_Practica3.EF;
using G5_Practica3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace G5_Practica3.Controllers
{
    public class AbonoController : Controller
    {


        [HttpGet]
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

        [HttpPost]
        public ActionResult RegistrarAbono(EF.Abonos abono)
        {
            using (var dbContext = new PracticaS12Entities())
            {
                decimal saldoPendiente = dbContext.Principal
                    .Where(c => c.Id_Compra == abono.Id_Compra)
                    .Select(c => c.Saldo)
                    .FirstOrDefault();

                decimal precioProducto = dbContext.Principal
                    .Where(c => c.Id_Compra == abono.Id_Compra)
                    .Select(c => c.Precio)
                    .FirstOrDefault();


                if (abono.Monto > 0 && saldoPendiente > 0 && abono.Monto <= saldoPendiente)
                {
                    abono.Fecha = DateTime.Now;
                    dbContext.Abonos.Add(abono);

                    var compra = dbContext.Principal.First(c => c.Id_Compra == abono.Id_Compra);
                    compra.Saldo -= abono.Monto;
                    
                    if (compra.Saldo == 0)
                    {
                        compra.Saldo = 0;
                        compra.Estado = "Pagado";
                    }

                    dbContext.SaveChanges();
                }
                else
                {
                    TempData["Mensaje"] = "No se pudo registrar el abono. Digite un monto correcto";
                    return RedirectToAction("Registro", "Abono");
                }
            }

            TempData["Mensaje"] = "Abono registrado correctamente";
            return RedirectToAction("Index", "Home");
        }
        public JsonResult ObtenerSaldoRestante(int idCompra)
        {
            using (var dbContext = new PracticaS12Entities())
            {
                decimal saldoPendiente = dbContext.Principal
                    .Where(c => c.Id_Compra == idCompra)
                    .Select(c => c.Saldo)
                    .FirstOrDefault();

                return Json(new { saldoPendiente }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}