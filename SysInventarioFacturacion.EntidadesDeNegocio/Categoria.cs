using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacion.EntidadesDeNegocio
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }
        [Required(ErrorMessage = "Codigo es obligatorio")]

        public int Codigo { get; set; }
        [Required(ErrorMessage = "Nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string? Nombre { get; set;}
        [Required(ErrorMessage = "Descripcion es obligatorio")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string? Descripcion { get; set;}
        [NotMapped]
        public int Top_Aux { get; set; }

    }
}
