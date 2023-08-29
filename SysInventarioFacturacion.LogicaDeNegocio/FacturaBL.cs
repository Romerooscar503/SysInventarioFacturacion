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

        public async Task<int> CrearAsync(Factura pFactura)
        {
            return await FacturaDAL.CrearAsync(pFactura);
        }
        public async Task<int> ModificarAsync(Factura pFactura)
        {
            return await FacturaDAL.ModificarAsync(pFactura);
        }
        public async Task<int> EliminarAsync(Factura pFactura)
        {
            return await FacturaDAL.EliminarAsync(pFactura);
        }
        public async Task<Factura> ObtenerPorIdAsync(Factura pFactura)
        {
            return await FacturaDAL.ObtenerPorIdAsync(pFactura);
        }
        public async Task<List<Factura>> ObtenerTodosAsync()
        {
            return await FacturaDAL.ObtenerTodosAsync();
        }
        public async Task<List<Factura>> BuscarAsync(Factura pFactura)
        {
            return await FacturaDAL.BuscarAsync(pFactura);
        }

        public async Task<List<Factura>> BuscarIncluirUsuarioAsync(Factura pFactura)
        {
            return await FacturaDAL.BuscarIncluirUsuarioAsync(pFactura);
        }

    }
}
