using Microsoft.EntityFrameworkCore;
using SysInventarioFacturacion.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacion.AccesoADatos
{
    public class DetallePedidoDAL
    {
        public static async Task<int> CrearAsync(DetallePedido pDetallePedido)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                pDetallePedido.FechaPedido = DateTime.Now;
                bdContexto.Add(pDetallePedido);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModificarAsync(DetallePedido pDetallePedido)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var detallePedido = await bdContexto.DetallePedido.FirstOrDefaultAsync(s => s.IdDetallePedido == pDetallePedido.IdDetallePedido);
                detallePedido.IdProducto = pDetallePedido.IdProducto;
                detallePedido.IdProveedor = pDetallePedido.IdProveedor;
                detallePedido.IdPedido = pDetallePedido.IdPedido;
                detallePedido.Cantidad = pDetallePedido.Cantidad;
                bdContexto.Update(detallePedido);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(DetallePedido pDetallePedido)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var detallePedido = await bdContexto.DetallePedido.FirstOrDefaultAsync(s => s.IdDetallePedido == pDetallePedido.IdDetallePedido);
                bdContexto.DetallePedido.Remove(detallePedido);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<DetallePedido> ObtenerPorIdAsync(DetallePedido pDetallePedido)
        {
            var detallePedido = new DetallePedido();
            using (var bdContexto = new BDContexto())
            {
                detallePedido = await bdContexto.DetallePedido.FirstOrDefaultAsync(s => s.IdDetallePedido == pDetallePedido.IdDetallePedido);
            }
            return detallePedido;
        }
        public static async Task<List<DetallePedido>> ObtenerTodosAsync()
        {
            var DetallePedido = new List<DetallePedido>();
            using (var bdContexto = new BDContexto())
            {
                DetallePedido = await bdContexto.DetallePedido.ToListAsync();
            }
            return DetallePedido;
        }
        internal static IQueryable<DetallePedido> QuerySelect(IQueryable<DetallePedido> pQuery, DetallePedido pDetallePedido)
        {
            if (pDetallePedido.IdDetallePedido > 0)
                pQuery = pQuery.Where(s => s.IdDetallePedido == pDetallePedido.IdDetallePedido);
            if (pDetallePedido.IdPedido > 0)
                pQuery = pQuery.Where(s => s.IdPedido == pDetallePedido.IdPedido);
            if (pDetallePedido.IdProducto > 0)
                pQuery = pQuery.Where(s => s.IdProducto == pDetallePedido.IdProducto);
            if (pDetallePedido.IdProveedor > 0)
                pQuery = pQuery.Where(s => s.IdProveedor == pDetallePedido.IdProveedor);
            if (pDetallePedido.Cantidad > 0)
             pQuery = pQuery.Where(s => s.Cantidad == pDetallePedido.Cantidad);
            if (pDetallePedido.FechaPedido.Year > 1000)
            {
                DateTime fechaInicial = new DateTime(pDetallePedido.FechaPedido.Year, pDetallePedido.FechaPedido.Month, pDetallePedido.FechaPedido.Day, 0, 0, 0);
                DateTime fechaFinal = fechaInicial.AddDays(1).AddMilliseconds(-1);
                pQuery = pQuery.Where(s => s.FechaPedido >= fechaInicial && s.FechaPedido <= fechaFinal);
            }

            pQuery = pQuery.OrderByDescending(s => s.IdDetallePedido).AsQueryable();
            if (pDetallePedido.Top_Aux > 0)
                pQuery = pQuery.Take(pDetallePedido.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<DetallePedido>> BuscarAsync(DetallePedido pDetallePedido)
        {
            var DetallePedido = new List<DetallePedido>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.DetallePedido.AsQueryable();
                select = QuerySelect(select, pDetallePedido);
                DetallePedido = await select.ToListAsync();
            }
            return DetallePedido;
        }

        public static async Task<List<DetallePedido>> BuscarIncluirPedidoProductoProveedorAsync(DetallePedido pDetallePedido)
        {
            var DetallePedido = new List<DetallePedido>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.DetallePedido.AsQueryable();
                select = QuerySelect(select, pDetallePedido).Include(s => s.Pedido).Include(s => s.Producto).Include(s => s.Proveedor).AsQueryable();
                DetallePedido = await select.ToListAsync();
            }
            return DetallePedido;
        }
    }
}
