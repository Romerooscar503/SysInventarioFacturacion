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
        public static async Task<int> CrearAsync(Factura pFactura)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                pFactura.FechaFacturacion = DateTime.Now;
                bdContexto.Add(pFactura);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModificarAsync(Factura pFactura)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var Factura = await bdContexto.Factura.FirstOrDefaultAsync(s => s.IdFactura == pFactura.IdFactura);
                Factura.IdUsuario = pFactura.IdUsuario;
                Factura.NumeroFactura = pFactura.NumeroFactura;            
                Factura.Descripcion = pFactura.Descripcion;
                Factura.Direccion = pFactura.Direccion;
                Factura.Telefono = pFactura.Telefono;
                Factura.Correo = pFactura.Correo;
                Factura.Total = pFactura.Total;
                Factura.Descuento = pFactura.Descuento;
                Factura.Impuesto = pFactura.Impuesto;
                Factura.TotalPagado = pFactura.TotalPagado;
                bdContexto.Update(Factura);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
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
        public static async Task<Factura> ObtenerPorIdAsync(Factura pFactura)
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
            var Facturas = new List<Factura>();
            using (var bdContexto = new BDContexto())
            {
                Facturas = await bdContexto.Factura.Include(f => f.DetalleFactura).ThenInclude(p => p.Producto).ToListAsync();
            }
            return Facturas;
        }
        internal static IQueryable<Factura> QuerySelect(IQueryable<Factura> pQuery, Factura pFactura)
        {
            //Para enteros y decimales
            if (pFactura.IdFactura > 0)
                pQuery = pQuery.Where(s => s.IdFactura == pFactura.IdFactura);
            //if (pFactura.IdUsuario > 0)
            //    pQuery = pQuery.Where(s => s.IdUsuario == pFactura.IdUsuario);
            //if (pFactura.NumeroFactura > 0)
            //    pQuery = pQuery.Where(s => s.NumeroFactura == pFactura.NumeroFactura);
            //Para fecha
            if (pFactura.FechaFacturacion.Year > 1000)
            {
                DateTime fechaInicial = new DateTime(pFactura.FechaFacturacion.Year, pFactura.FechaFacturacion.Month, pFactura.FechaFacturacion.Day, 0, 0, 0);
                DateTime fechaFinal = fechaInicial.AddDays(1).AddMilliseconds(-1);
                pQuery = pQuery.Where(s => s.FechaFacturacion >= fechaInicial && s.FechaFacturacion <= fechaFinal);
            }
            //if (!string.IsNullOrWhiteSpace(pFactura.FechaFacturacion.ToString()))
            //    pQuery = pQuery.Where(s => s.FechaFacturacion.ToString().Contains(pFactura.FechaFacturacion.ToString()));

            //if (pFactura.Cantidad > 0)
            //    pQuery = pQuery.Where(s => s.Cantidad == pFactura.Cantidad);
            //if (!string.IsNullOrWhiteSpace(pFactura.Descripcion))
            //    pQuery = pQuery.Where(s => s.Descripcion.Contains(pFactura.Descripcion));
            //if (!string.IsNullOrWhiteSpace(pFactura.Direccion))
            //    pQuery = pQuery.Where(s => s.Direccion.Contains(pFactura.Direccion));
            //if (!string.IsNullOrWhiteSpace(pFactura.Telefono))
            //    pQuery = pQuery.Where(s => s.Telefono.Contains(pFactura.Telefono));
            //if (!string.IsNullOrWhiteSpace(pFactura.Correo))
            //    pQuery = pQuery.Where(s => s.Correo.Contains(pFactura.Correo));

            //if (pFactura.Total > 0)
            //    pQuery = pQuery.Where(s => s.Total == pFactura.Total);
            //if (pFactura.Descuento > 0)
            //    pQuery = pQuery.Where(s => s.Descuento == pFactura.Descuento);
            //if (pFactura.Impuesto > 0)
            //    pQuery = pQuery.Where(s => s.Impuesto == pFactura.Impuesto);
            //if (pFactura.TotalPagado > 0)
            //    pQuery = pQuery.Where(s => s.TotalPagado == pFactura.TotalPagado);

            pQuery = pQuery.OrderByDescending(s => s.IdFactura).AsQueryable();
            if (pFactura.Top_Aux > 0)
                pQuery = pQuery.Take(pFactura.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<Factura>> BuscarAsync(Factura pFactura)
        {
            var Facturas = new List<Factura>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Factura.AsQueryable();
                select = QuerySelect(select, pFactura);
                Facturas = await select.ToListAsync();
            }
            return Facturas;
        }

        public static async Task<List<Factura>> BuscarIncluirUsuarioAsync(Factura pFactura)
        {
            var Facturas = new List<Factura>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Factura.AsQueryable();
                select = QuerySelect(select, pFactura).Include(s => s.Usuario).AsQueryable();
                Facturas= await select.ToListAsync();
            }
            return Facturas;
        }
    }
}

//