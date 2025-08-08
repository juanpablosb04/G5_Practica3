using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G5_Practica3.Models
{
    public class Principal
    {
        public int ID_Compra { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public decimal Saldo { get; set; }
        public string Estado { get; set; }
    }
}