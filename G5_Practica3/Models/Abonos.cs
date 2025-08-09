using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace G5_Practica3.Models
{
    public class Abonos
    {
        public long Id_Abono { get; set; }
        public long Id_Compra { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public virtual Principal Compra { get; set; }
    }
}