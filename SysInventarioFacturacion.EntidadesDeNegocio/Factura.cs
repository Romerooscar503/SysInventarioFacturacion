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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFactura { get; set; }

        [ForeignKey("Usuario")]
        [Required(ErrorMessage = "Usuario es obligatorio")]
        [Display(Name = "Usuario")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Numero factura es obligatorio")]
        public int NumeroFactura { get; set; }
        [Required(ErrorMessage = "Fecha facturacion es obligatorio")]
   
        public DateTime FechaFacturacion { get; set; }

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
        [Display(Name = "Total")]
        [Required(ErrorMessage = "El campo Total es obligatorio.")]
        public decimal? Total { get; set; }
        [Display(Name = "Descuento")]
        [Required(ErrorMessage = "El campo Descuento es obligatorio.")]
        [Range(0.01, 10000, ErrorMessage = "El campo Precio debe estar entre 0.01 y 10,000.")]
        public decimal? Descuento { get; set; }
        [Display(Name = "Impuesto")]
        [Required(ErrorMessage = "El campo Impuesto es obligatorio.")]
        [Range(0.01, 10000, ErrorMessage = "El campo Precio debe estar entre 0.01 y 10,000.")]
        public decimal? Impuesto { get; set; }

        [Display(Name = "Total Pagado")]
        [Required(ErrorMessage = "El campo Total Pagado es obligatorio.")]
        [Range(0.01, 10000, ErrorMessage = "El campo Precio debe estar entre 0.01 y 10,000.")]
        public decimal? TotalPagado { get; set; }

        public Usuario? Usuario { get; set; }
       
        public List<DetalleFactura>? DetalleFactura { get; set; }
      
        [NotMapped]
        public int Top_Aux { get; set; }

        
        //cambios
    }
}
