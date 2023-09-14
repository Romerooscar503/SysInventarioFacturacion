using SysInventarioFacturacion.AccesoADatos;
using SysInventarioFacturacion.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacion.LogicaDeNegocio
{
    public class DetallePedidoBL
    {

        public async Task<int> CrearAsync(DetallePedido pDetallePedido)
        {
            return await DetallePedidoDAL.CrearAsync(pDetallePedido);
        }
        public async Task<int> ModificarAsync(DetallePedido pDetallePedido)
        {
            return await DetallePedidoDAL.ModificarAsync(pDetallePedido);
        }
        public async Task<int> EliminarAsync(DetallePedido pDetallePedido)
        {
            return await DetallePedidoDAL.EliminarAsync(pDetallePedido);
        }
        public async Task<DetallePedido> ObtenerPorIdAsync(DetallePedido pDetallePedido)
        {
            return await DetallePedidoDAL.ObtenerPorIdAsync(pDetallePedido);
        }
        public async Task<List<DetallePedido>> ObtenerTodosAsync()
        {
            return await DetallePedidoDAL.ObtenerTodosAsync();
        }
        public async Task<List<DetallePedido>> BuscarAsync(DetallePedido pDetallePedido)
        {
            return await DetallePedidoDAL.BuscarAsync(pDetallePedido);
        }

        public async Task<List<DetallePedido>> BuscarIncluirPedidoProductoProveedorAsync(DetallePedido pDetallePedido)
        {
            return await DetallePedidoDAL.BuscarIncluirPedidoProductoProveedorAsync(pDetallePedido);
        }
    }
}
