using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppFerreteria.Models
{
    public class Clientes
    {
        [Key]

        public int ClientesID { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        [StringLength(30, ErrorMessage = "El Nombre no debe superar los 30 caracteres")]
        public string NombreCliente { get; set; }


        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        [StringLength(20, ErrorMessage = "El Apellido no debe superar los 20 caracteres")]
        public string ApellidoCliente { get; set; }


        [Display(Name = "D.N.I")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        [StringLength(8, ErrorMessage = "ATENCIÓN! Superaste el máximo permitido")]
        public string DniCliente { get; set; }



        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        [StringLength(10, ErrorMessage = "El Número no debe superar los 10 caracteres")]
        public string TelCliente { get; set; }


        public virtual ICollection<Alquiler> Alquilers { get; set; }
        public virtual ICollection<Devolucion> Devolucions { get; set; }


        [NotMapped]
        public string ClienteFN
        {
            get
            {
                return string.Format("{0} {1}", NombreCliente, ApellidoCliente);

            }

        }
               
         
    }
}