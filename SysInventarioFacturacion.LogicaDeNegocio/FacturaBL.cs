using SysInventarioFacturacion.AccesoADatos;
using SysInventarioFacturacion.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacion.LogicaDeNegocio
{
    public class FacturaBL
    {
        public async Task<int> AgregarAsync(Factura pFactura)
        {
            return await FacturaDAL.AgregarAsync(pFactura);
        }
        public async Task<int> EditarAsync(Factura pFactura)
        {
            return await FacturaDAL.EditarAsync(pFactura);
        }
        public async Task<int> EliminarAsync(Factura pFactura)
        {
            return await FacturaDAL.EliminarAsync(pFactura);
        }
        public async Task<Factura> ObtenerPorIdAsync(Factura pFactura)
        {
            return await FacturaDAL.ObtenerPorIdFacturaAsync(pFactura);
        }
        public async Task<List<Factura>> ObtenerTodosAsync()
        {
            return await FacturaDAL.ObtenerTodosAsync();
        }
        public async Task<List<Factura>> BuscarAsync(Factura pFactura)
        {
            return await FacturaDAL.BuscarAsync(pFactura);
        }
    }
}
