using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppFerreteria.Models
{
    public class Motosierras
    {

        [Key]
        public int MotosierrasID { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        [StringLength(10, ErrorMessage = "El código no debe superar los 10 caracteres")]
        public string CodigoMotosierra { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string PrecioAlquiler { get; set; }

        [Display(Name = "Código de Fabrica")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        [StringLength(10, ErrorMessage = "El código no debe superar los 10 caracteres")]
        public string CodDeFabrica { get; set; }


        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public EstadoMotosierra EstadoMotosierra { get; set; }

        public byte [] ImagenMoto { get; set; }

        public virtual ICollection<Alquiler> Alquilers { get; set; }
        public virtual ICollection<Devolucion> Devolucions { get; set; }

        [NotMapped]
        public string MotoImagenPrincipalString { get; set; }
    }

    public enum EstadoMotosierra
    {
        Disponible,
        Alquilada
    }
}