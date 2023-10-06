using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SysInventarioFacturacion.EntidadesDeNegocio
{
	public class Producto
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IdProducto { get; set; }

        [ForeignKey("Categoria")]
        [Required(ErrorMessage = "Categoria es obligatoria")]
        [Display(Name = "Categoria")]
        public int IdCategoria { get; set; }

        [ForeignKey("Proveedor")]
		[Required(ErrorMessage = "Proveedor es obligatorio")]
		[Display(Name = "Proveedor")]
		public int IdProveedor { get; set; }


		[Required(ErrorMessage = "el codigo es obligatorio")]
        public int? Codigo { get; set; }

		[Required(ErrorMessage = "El nombre es obligatorio")]
		[StringLength(30, ErrorMessage = "Maximo 30 caracteres")]
		public string? Nombre { get; set; }

		[Required(ErrorMessage = "La descripcion es obligatoria")]
		[StringLength(50, ErrorMessage = "Maximo50 caracteres")]
		public string? Descripcion { get; set; }

		[Required(ErrorMessage = "La talla es obligatoria")]
		[StringLength(10, ErrorMessage = "Maximo 10 caracteres")]
		public string? Talla { get; set; }

		[Required(ErrorMessage = "La marca es obligatoria")]
		[StringLength(30, ErrorMessage = "Maximo 30 caracteres")]
		public string? Marca { get; set; }

		[Required(ErrorMessage = "El cantidad es obligatorio")]
		public int Cantidad { get; set; }

		[Required(ErrorMessage = "El color es obligatorio")]
		[StringLength(20, ErrorMessage = "Maximo 20 caracteres")]
		public string? Color { get; set; }

		[Required(ErrorMessage = "El precio unitario es obligatorio")]
		public decimal PrecioUnitario { get; set; }

        //RELACIONES CON PROVEEDOR Y CATEGORIA

        public Categoria? Categoria { get; set; }

        public Proveedor? Proveedor { get; set; }

		public ICollection<DetallePedido>? DetallePedido { get; set; }

		public ICollection<DetalleFactura>? DetalleFactura { get; set; }

		[NotMapped]
		public int Top_Aux { get; set; }

	}
}
