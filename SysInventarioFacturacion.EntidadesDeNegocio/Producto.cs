﻿using System;
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

        [Required(ErrorMessage = "el codigo es obligatorio")]
        
        public int Codigo { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(30,ErrorMessage = "Maximo 30 caracteres")]
        public string? Nombre { get; set;}

        [Required(ErrorMessage = "La descripcion es obligatoria")]
        [StringLength(50, ErrorMessage = "Maximo50 caracteres")]
        public string? Descripcion { get; set;}

        [Required(ErrorMessage = "La talla es obligatoria")]
        [StringLength(10, ErrorMessage = "Maximo 10 caracteres")]
        public string? Talla { get; set;}

        [Required(ErrorMessage = "La marca es obligatoria")]
        [StringLength(30, ErrorMessage = "Maximo 30 caracteres")]
        public string? Marca { get; set;}

        [Required(ErrorMessage = "El color es obligatorio")]
        [StringLength(20, ErrorMessage = "Maximo 20 caracteres")]
        public string? Color { get; set;}
       
        [Required(ErrorMessage = "El precio unitario es obligatorio")]

        public decimal PrecioUnitario { get; set;}
        [NotMapped]
        public CompraProveedor CompraProveedor { get;set;}
        
        [NotMapped]
        public Categoria Categoria { get; set; }
        [NotMapped]
        public DetalleFactura DetalleFactura { get; set; }
       
        [NotMapped]
        public int Top_Aux { get; set;}

        
        
    }
}
