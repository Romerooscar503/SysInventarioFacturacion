using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacion.EntidadesDeNegocio
{
    public class Venta
    {


        public Factura ObjFactura { get; set; }
        public List<DetalleFactura> DetalleFacturas { get; set; }
    }


}
