using Microsoft.EntityFrameworkCore;
using SysInventarioFacturacion.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacion.AccesoADatos
{
    public class ProveedorDAL
    {
        public static async Task<int> CrearAsync(Proveedor pProveedor)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pProveedor);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModificarAsync(Proveedor pProveedor)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var Proveedor = await bdContexto.Proveedor.FirstOrDefaultAsync(s => s.Id == pProveedor.Id);
                Proveedor.Nombre = pProveedor.Nombre;
                bdContexto.Update(Proveedor);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(Proveedor pProveedor)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var Proveedor = await bdContexto.Proveedor.FirstOrDefaultAsync(s => s.Id == pProveedor.Id);
                bdContexto.Proveedor.Remove(Proveedor);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<Proveedor> ObtenerPorIdProveedorAsync(Proveedor pProveedor)
        {
            var Proveedor = new Proveedor();
            using (var bdContexto = new BDContexto())
            {
                Proveedor = await bdContexto.Proveedor.FirstOrDefaultAsync(s => s.Id == pProveedor.Id);
            }
            return Proveedor;
        }
        public static async Task<List<Proveedor>> ObtenerTodosAsync()
        {
            var Proveedores = new List<Proveedor>();
            using (var bdContexto = new BDContexto())
            {
                Proveedores = await bdContexto.Proveedor.ToListAsync();
            }
            return Proveedores;
        }
        internal static IQueryable<Proveedor> QuerySelect(IQueryable<Proveedor> pQuery, Proveedor pProveedor)
        {
            if (pProveedor.Id > 0)
                pQuery = pQuery.Where(s => s.Id == pProveedor.Id);
            if (!string.IsNullOrWhiteSpace(pProveedor.Nombre))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pProveedor.Nombre));
            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
            if (pProveedor.Top_Aux > 0)
                pQuery = pQuery.Take(pProveedor.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<Proveedor>> BuscarAsync(Proveedor pProveedor)
        {
            var Proveedores = new List<Proveedor>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Proveedor.AsQueryable();
                select = QuerySelect(select, pProveedor);
                Proveedores = await select.ToListAsync();
            }
            return Proveedores;
        }
    }
}
