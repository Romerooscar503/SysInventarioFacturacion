using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacion.EntidadesDeNegocio
{
    public class Inventario
    {
        [Key]
        public int IdInventario { get; set; }
        [Required(ErrorMessage = "Cantidad inicial de produto es obligatorio")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string? CantidadInicialProducto { get; set; }
        [Required(ErrorMessage = "Cantidad disponible de producto es obligatorio")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string? CantidadDisponibleProducto { get; set; }
        [NotMapped]
        public int Top_Aux { get; set; }
    }
}
