using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacion.EntidadesDeNegocio
{
    public class Factura
    {
        [Key]
        public int IdFactura { get; set; }
        [ForeignKey("Usuario")]
        [Required(ErrorMessage = "Usuario es obligatorio")]
        [Display(Name = "Usuario")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Numero factura es obligatorio")]
        public int NumeroFactura { get; set; }
        [Required(ErrorMessage = "Fecha facturacion es obligatorio")]
   
        public DateTime FechaFacturacion { get; set; }
        [Required(ErrorMessage = "Cantidad es obligatorio")]

        public int Cantidad { get; set; }
        [Required(ErrorMessage = "Descripcion es obligatorio")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string? Descripcion { get; set; }
        [Required(ErrorMessage = "Direccion es obligatorio")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string? Direccion { get; set; }
        [Required(ErrorMessage = "Telefono es obligatorio")]
        [StringLength(15, ErrorMessage = "Maximo 15 caracteres")]
        public string? Telefono { get; set; }
        [Required(ErrorMessage = "Correo es obligatorio")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string? Correo { get; set; }
        [Required(ErrorMessage = "Total es obligatorio")]
        public decimal Total { get; set; }
        [Required(ErrorMessage = "Descuento es obligatorio")]
        public decimal Descuento { get; set; }
        [Required(ErrorMessage = "Impuesto es obligatorio")]
        public decimal Impuesto { get; set; }
        [Required(ErrorMessage = "Total pagado es obligatorio")]
        public decimal TotalPagado { get; set; }
        [NotMapped]
        public int Top_Aux { get; set; }
        //cambios
    }
}
