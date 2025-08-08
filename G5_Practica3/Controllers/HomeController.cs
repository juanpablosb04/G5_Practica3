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

            return View();
        }


        public ActionResult Lista()
        {
            using (var dbContext = new G5_Practica3Entities())
            {
                var lista = dbContext.Principal
                .OrderByDescending(p => p.Estado == "Pendiente")  
                .ThenBy(p => p.ID_Compra)
                .Select(p => new Models.Principal
                {
                    ID_Compra = p.ID_Compra,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    Saldo = p.Saldo,
                    Estado = p.Estado
                })
                .ToList();

                return View(lista);
            }
        }
    }
}