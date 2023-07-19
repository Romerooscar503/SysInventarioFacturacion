using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacion.EntidadesDeNegocio
{

    public class CompraProveedor
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El codigo es obligatorio")]
        
        public int Codigo { get; set; }

        [Required(ErrorMessage = "la fecha es obligatorio")]
        public DateTime? Fecha { get; set; }

        [Required(ErrorMessage = "el total compras es obligatorio")]
        public decimal? TotalCompras { get; set; }
        [NotMapped]
        public Proveedor? Proveedor { get; set; }


        [NotMapped]
        public int Top_Aux { get; set; }
    }


}
