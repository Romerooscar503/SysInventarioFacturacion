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
                var DetalleFactura = await bdContexto.DetalleFactura.FirstOrDefaultAsync(s => s.Id == pDetalleFactura.Id);
                DetalleFactura.Id = pDetalleFactura.Id;
                bdContexto.Update(DetalleFactura);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(DetalleFactura pDetalleFactura)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var DetalleFactura = await bdContexto.DetalleFactura.FirstOrDefaultAsync(s => s.Id == pDetalleFactura.Id);
                bdContexto.DetalleFactura.Remove(DetalleFactura);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<DetalleFactura> ObtenerPorIdAsync(DetalleFactura pDetalleFactura)
        {
            var DetalleFactura = new DetalleFactura();
            using (var bdContexto = new BDContexto())
            {
                DetalleFactura = await bdContexto.DetalleFactura.FirstOrDefaultAsync(s => s.Id == pDetalleFactura.Id);
            }
            return DetalleFactura;
        }
        public static async Task<List<DetalleFactura>> ObtenerTodosAsync()
        {
            var DetalleFactura = new List<DetalleFactura>();
            using (var bdContexto = new BDContexto())
            {
                DetalleFactura = await bdContexto.DetalleFactura.ToListAsync();
            }
            return DetalleFactura;
        }
        internal static IQueryable<DetalleFactura> QuerySelect(IQueryable<DetalleFactura> pQuery, DetalleFactura pDetalleFactura)
        {
            if (pDetalleFactura.Cantidad > 0)
                pQuery = pQuery.Where(s => s.Cantidad == pDetalleFactura.Cantidad);


            if (pDetalleFactura.Codigo > 0)
                pQuery = pQuery.Where(s => s.Codigo == pDetalleFactura.Codigo);


            if (!string.IsNullOrWhiteSpace(pDetalleFactura.FormaDePago))
                pQuery = pQuery.Where(s => s.FormaDePago.Contains(pDetalleFactura.FormaDePago));

            if (!string.IsNullOrWhiteSpace(pDetalleFactura.FechaEmision.ToString()))
                pQuery = pQuery.Where(s => s.FechaEmision.ToString().Contains(pDetalleFactura.FechaEmision.ToString()));


            if (pDetalleFactura.ValorTotal > 0)
                pQuery = pQuery.Where(s => s.ValorTotal== pDetalleFactura.ValorTotal);



            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
            if (pDetalleFactura.Top_Aux > 0)
                pQuery = pQuery.Take(pDetalleFactura.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<DetalleFactura>> BuscarAsync(DetalleFactura pDetalleFactura)
        {
            var DetalleFactura = new List<DetalleFactura>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.DetalleFactura.AsQueryable();
                select = QuerySelect(select, pDetalleFactura);
                DetalleFactura = await select.ToListAsync();
            }
            return DetalleFactura;
        }
    }
}