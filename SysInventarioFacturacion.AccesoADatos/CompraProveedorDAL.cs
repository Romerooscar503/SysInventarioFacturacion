using Microsoft.EntityFrameworkCore;
using SysInventarioFacturacion.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace SysInventarioFacturacion.AccesoADatos
{
    public class CompraProveedorDAL
    {
        public static async Task<int> CrearAsync(CompraProveedor pCompraProveedor)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pCompraProveedor);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModificarAsync(CompraProveedor pCompraProveedor)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var CompraProveedor = await bdContexto.CompraProveedor.FirstOrDefaultAsync(s => s.IdCompraProveedor == pCompraProveedor.IdCompraProveedor);
                CompraProveedor.IdProveedor = pCompraProveedor.IdProveedor;
                CompraProveedor.Codigo = pCompraProveedor.Codigo;
                CompraProveedor.Fecha = pCompraProveedor.Fecha;
                CompraProveedor.TotalCompras = pCompraProveedor.TotalCompras;
                bdContexto.Update(CompraProveedor);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(CompraProveedor pCompraProveedor)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var CompraProveedor = await bdContexto.CompraProveedor.FirstOrDefaultAsync(s => s.IdCompraProveedor == pCompraProveedor.IdCompraProveedor);
                bdContexto.CompraProveedor.Remove(CompraProveedor);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<CompraProveedor> ObtenerPorIdAsync(CompraProveedor pCompraProveedor)
        {
            var CompraProveedor = new CompraProveedor();
            using (var bdContexto = new BDContexto())
            {
                CompraProveedor = await bdContexto.CompraProveedor.FirstOrDefaultAsync(s => s.IdCompraProveedor == pCompraProveedor.IdCompraProveedor);
            }
            return CompraProveedor;
        }
        public static async Task<List<CompraProveedor>> ObtenerTodosAsync()
        {
            var CompraProveedores = new List<CompraProveedor>();
            using (var bdContexto = new BDContexto())
            {
                CompraProveedores = await bdContexto.CompraProveedor.ToListAsync();
            }
            return CompraProveedores;
        }
        internal static IQueryable<CompraProveedor> QuerySelect(IQueryable<CompraProveedor> pQuery, CompraProveedor pCompraProveedor)
        {
            //Para enteros y decimales
            if (pCompraProveedor.IdCompraProveedor > 0)
                pQuery = pQuery.Where(s => s.IdCompraProveedor == pCompraProveedor.IdCompraProveedor);
            if (pCompraProveedor.IdProveedor > 0)
                pQuery = pQuery.Where(s => s.IdProveedor == pCompraProveedor.IdProveedor);
            if (pCompraProveedor.Codigo > 0)
                pQuery = pQuery.Where(s => s.Codigo == pCompraProveedor.Codigo);
            //Para fecha
            if (!string.IsNullOrWhiteSpace(pCompraProveedor.Fecha.ToString()))
                pQuery = pQuery.Where(s => s.Fecha.ToString().Contains(pCompraProveedor.Fecha.ToString()));
             
            if (pCompraProveedor.TotalCompras > 0)
                pQuery = pQuery.Where(s => s.TotalCompras == pCompraProveedor.TotalCompras);

            pQuery = pQuery.OrderByDescending(s => s.IdCompraProveedor).AsQueryable();
            if (pCompraProveedor.Top_Aux > 0)
                pQuery = pQuery.Take(pCompraProveedor.Top_Aux).AsQueryable();
            return pQuery; 
        }
        public static async Task<List<CompraProveedor>> BuscarAsync(CompraProveedor pCompraProveedor)
        {
            var CompraProveedores = new List<CompraProveedor>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.CompraProveedor.AsQueryable();
                select = QuerySelect(select, pCompraProveedor);
                CompraProveedores = await select.ToListAsync();
            }
            return CompraProveedores;
        }

        public static async Task<List<CompraProveedor>> BuscarIncluirProveedorAsync(CompraProveedor pCompraProveedor)
        {
            var CompraProveedores = new List<CompraProveedor>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.CompraProveedor.AsQueryable();
                select = QuerySelect(select, pCompraProveedor).Include(s => s.Proveedor).AsQueryable();
                CompraProveedores = await select.ToListAsync();
            }
            return CompraProveedores;
        }
    }
}

