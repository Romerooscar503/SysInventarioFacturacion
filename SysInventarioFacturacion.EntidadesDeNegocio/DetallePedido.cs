using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SysInventarioFacturacion.EntidadesDeNegocio
{
    public class DetallePedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDetallePedido { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Ingrese la cantidad.")]
        public int Cantidad { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaPedido {get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }

            [ForeignKey("Producto")]
        [Required(ErrorMessage = "Producto es obligatorio")]
        [Display(Name = "Producto")]
        public int IdProducto { get; set; }


        [ForeignKey("Pedido")]
        [Required(ErrorMessage = "Pedido es obligatorio")]
        [Display(Name = "Pedido")]
        public int IdPedido { get; set; }

        [ForeignKey("Proveedor")]
        [Required(ErrorMessage = "Proveedor es obligatorio")]
        [Display(Name = "Proveedor")]
        public int IdProveedor { get; set; }

        public Proveedor? Proveedor { get; set; }

        public Producto? Producto { get; set; }

        public Pedido? Pedido { get; set; }
    }
}
