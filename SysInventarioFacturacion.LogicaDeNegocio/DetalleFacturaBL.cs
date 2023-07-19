using SysInventarioFacturacion.AccesoADatos;
using SysInventarioFacturacion.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacion.LogicaDeNegocio
{
    public class DetalleFacturaBL
    {
        public async Task<int> CrearAsync(DetalleFactura pDetalleFactura)
        {
            return await DetalleFacturaDAL.CrearAsync(pDetalleFactura);
        }
        public async Task<int> ModificarAsync(DetalleFactura pDetalleFactura)
        {
            return await DetalleFacturaDAL.ModificarAsync(pDetalleFactura);
        }
        public async Task<int> EliminarAsync(DetalleFactura pDetalleFactura)
        {
            return await DetalleFacturaDAL.EliminarAsync(pDetalleFactura);
        }
        public async Task<DetalleFactura> ObtenerPorIdAsync(DetalleFactura pDetalleFactura)
        {
            return await DetalleFacturaDAL.ObtenerPorIdAsync(pDetalleFactura);
        }
        public async Task<List<DetalleFactura>> ObtenerTodosAsync()
        {
            return await DetalleFacturaDAL.ObtenerTodosAsync();
        }
        public async Task<List<DetalleFactura>> BuscarAsync(DetalleFactura pDetalleFactura)
        {
            return await DetalleFacturaDAL.BuscarAsync(pDetalleFactura);
        }
    }
}