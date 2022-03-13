using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppFerreteria.Models
{
    public class Devolucion
    {

        [Key]
        public int DevolucionID { get; set; }

        [Display(Name = "Fecha de Devolución")]
        [DataType(DataType.Date)]
        public DateTime FechaDevolucion { get; set; }


        [Display(Name = "Cliente")]
        public int ClientesID { get; set; }
        public virtual Clientes Clientes { get; set; }

        public int MotosierrasID { get; set; }
        public virtual Motosierras Motosierras { get; set; }
    }
}