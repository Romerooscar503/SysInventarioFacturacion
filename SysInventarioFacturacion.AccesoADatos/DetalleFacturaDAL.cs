using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SysInventarioFacturacion.EntidadesDeNegocio;

namespace SysInventarioFacturacion.AccesoADatos
{
    public class DetalleFacturaDAL
    {
        public static async Task<int> CrearAsync(DetalleFactura pDetalleFactura)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pDetalleFactura);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModificarAsync(DetalleFactura pDetalleFactura)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var detalleFactura = await bdContexto.DetalleFactura.FirstOrDefaultAsync(s => s.IdDetalleFactura == pDetalleFactura.IdDetalleFactura);               
                detalleFactura.IdFactura = pDetalleFactura.IdFactura;
                detalleFactura.IdProducto = pDetalleFactura.IdProducto;
                detalleFactura.Codigo = pDetalleFactura.Codigo;
                detalleFactura.Cantidad = pDetalleFactura.Cantidad;
                detalleFactura.FormaDePago = pDetalleFactura.FormaDePago;
                detalleFactura.FechaEmision = pDetalleFactura.FechaEmision;
                detalleFactura.ValorTotal = pDetalleFactura.ValorTotal;
                bdContexto.Update(detalleFactura);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(DetalleFactura pDetalleFactura)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var detalleFactura = await bdContexto.DetalleFactura.FirstOrDefaultAsync(s => s.IdDetalleFactura == pDetalleFactura.IdDetalleFactura);
                bdContexto.DetalleFactura.Remove(detalleFactura);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<DetalleFactura> ObtenerPorIdAsync(DetalleFactura pDetalleFactura)
        {
            var detalleFactura = new DetalleFactura();
            using (var bdContexto = new BDContexto())
            {
                detalleFactura = await bdContexto.DetalleFactura.FirstOrDefaultAsync(s => s.IdDetalleFactura == pDetalleFactura.IdDetalleFactura);
            }
            return detalleFactura;
        }
        public static async Task<List<DetalleFactura>> ObtenerTodosAsync()
        {
            var DetalleFacturas = new List<DetalleFactura>();
            using (var bdContexto = new BDContexto())
            {
                DetalleFacturas = await bdContexto.DetalleFactura.ToListAsync();
            }
            return DetalleFacturas;
        }
        internal static IQueryable<DetalleFactura> QuerySelect(IQueryable<DetalleFactura> pQuery, DetalleFactura pDetalleFactura)
        {
            if (pDetalleFactura.IdDetalleFactura > 0)
                pQuery = pQuery.Where(s => s.IdDetalleFactura == pDetalleFactura.IdDetalleFactura);
            if (pDetalleFactura.IdFactura > 0)
                pQuery = pQuery.Where(s => s.IdFactura == pDetalleFactura.IdFactura);
            if (pDetalleFactura.IdProducto > 0)
                pQuery = pQuery.Where(s => s.IdProducto == pDetalleFactura.IdProducto);
            //if (pDetalleFactura.Codigo > 0)
            //    pQuery = pQuery.Where(s => s.Codigo == pDetalleFactura.Codigo);
            //if (pDetalleFactura.Cantidad > 0)
            //    pQuery = pQuery.Where(s => s.Cantidad == pDetalleFactura.Cantidad);          
            //if (!string.IsNullOrWhiteSpace(pDetalleFactura.FormaDePago))
            //    pQuery = pQuery.Where(s => s.FormaDePago.Contains(pDetalleFactura.FormaDePago));

            //if (!string.IsNullOrWhiteSpace(pDetalleFactura.FechaEmision.ToString()))
            //    pQuery = pQuery.Where(s => s.FechaEmision.ToString().Contains(pDetalleFactura.FechaEmision.ToString()));
            //if (pDetalleFactura.ValorTotal > 0)
            //    pQuery = pQuery.Where(s => s.ValorTotal == pDetalleFactura.ValorTotal);



            pQuery = pQuery.OrderByDescending(s => s.IdDetalleFactura).AsQueryable();
            if (pDetalleFactura.Top_Aux > 0)
                pQuery = pQuery.Take(pDetalleFactura.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<DetalleFactura>> BuscarAsync(DetalleFactura pDetalleFactura)
        {
            var DetalleFacturas = new List<DetalleFactura>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.DetalleFactura.AsQueryable();
                select = QuerySelect(select, pDetalleFactura);
                DetalleFacturas = await select.ToListAsync();
            }
            return DetalleFacturas;
        }

         public static async Task<List<DetalleFactura>> BuscarIncluirFacturasYProductoAsync(DetalleFactura pDetalleFactura)
        {
            var DetalleFacturas = new List<DetalleFactura>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.DetalleFactura.AsQueryable();
                select = QuerySelect(select, pDetalleFactura).Include(s => s.Factura).Include(s => s.Producto).AsQueryable();
                DetalleFacturas = await select.ToListAsync();
            }
            return DetalleFacturas;
        }
    }
}