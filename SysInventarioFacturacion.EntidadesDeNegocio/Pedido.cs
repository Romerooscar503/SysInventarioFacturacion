using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacion.EntidadesDeNegocio
{
    public class Pedido
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IdPedido { get; set; }

		[Required(ErrorMessage = "Telefono es obligatorio")]
		[StringLength(9, ErrorMessage = "Escriba su numero de telefono con guion")]
		public string? Telefono { get; set; }    

        [ForeignKey("Usuario")]
        [Required(ErrorMessage = "Usuario es obligatorio")]
        [Display(Name = "Usuario")]
        public int IdUsuario { get; set; }

        public Usuario? Usuario { get; set; }

        [NotMapped]
        public ICollection<DetallePedido>? DetallePedido { get; set; }

		[NotMapped]
		public int Top_Aux { get; set; }
	}
}
