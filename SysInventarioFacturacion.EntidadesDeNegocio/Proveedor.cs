using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacion.EntidadesDeNegocio
{
    public class Proveedor
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El codigo es obligatorio")]

        public int Codigo { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(30, ErrorMessage = "Maximo30 caracteres")]

        public string? Nombre { get; set; }
        [Required(ErrorMessage = "La direccion es obligatoria")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string? Direccion { get; set; }
        [Required(ErrorMessage = "El telefono es obligatorio")]
        [StringLength(20, ErrorMessage = "Maximo 20 caracteres")]
        public string? Telefono { get; set;  }
        [NotMapped]
        public List <CompraProveedor>? CompraProveedor { get; set; }
        [NotMapped]
        public int Top_Aux { get; set; }
    }
}
