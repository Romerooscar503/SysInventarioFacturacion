using Microsoft.EntityFrameworkCore;
using SysInventarioFacturacion.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacion.AccesoADatos
{
    public class FacturaDAL
    {
        public static async Task<int> AgregarAsync(Factura pFactura)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pFactura);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<List<Factura>> BuscarAsync(Factura pFactura)
        {
            var Factura = new List<Factura>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Factura.AsQueryable();
                select = QuerySelect(select, pFactura);
                Factura = await select.ToListAsync();
            }
            return Factura;
        }
        public static async Task<int> EditarAsync(Factura pFactura)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var Factura = await bdContexto.Factura.FirstOrDefaultAsync(s => s.IdFactura == pFactura.IdFactura);
                Factura.FechaFacturacion = pFactura.FechaFacturacion;
                bdContexto.Update(Factura);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<Factura> ObtenerPorIdFacturaAsync(Factura pFactura)
        {
            var Factura = new Factura();
            using (var bdContexto = new BDContexto())
            {
                Factura = await bdContexto.Factura.FirstOrDefaultAsync(s => s.IdFactura == pFactura.IdFactura);
            }
            return Factura;
        }
        public static async Task<List<Factura>> ObtenerTodosAsync()
        {
            var Factura = new List<Factura>();
            using (var bdContexto = new BDContexto())
            {
                Factura = await bdContexto.Factura.ToListAsync();
            }
            return Factura;
        }
        internal static IQueryable<Factura> QuerySelect(IQueryable<Factura> pQuery, Factura pFactura)
        {
            if (pFactura.IdFactura > 0)
                pQuery = pQuery.Where(s => s.IdFactura == pFactura.IdFactura);
            if (!string.IsNullOrWhiteSpace(pFactura.FechaFacturacion.ToString()))
                pQuery = pQuery.Where(s => s.FechaFacturacion.ToString().Contains(pFactura.FechaFacturacion.ToString()));
            pQuery = pQuery.OrderByDescending(s => s.IdFactura).AsQueryable();
            if (pFactura.Top_Aux > 0)
                pQuery = pQuery.Take(pFactura.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<int> EliminarAsync(Factura pFactura)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var Factura = await bdContexto.Factura.FirstOrDefaultAsync(s => s.IdFactura == pFactura.IdFactura);
                bdContexto.Factura.Remove(Factura);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
    }
}