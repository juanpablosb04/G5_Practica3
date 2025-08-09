using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G5_Practica3.EF;

namespace G5_Practica3.Controllers
{
    public class ConsultaController : Controller
    {

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
    }
}