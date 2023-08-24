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
                var proveedor = await bdContexto.Proveedor.FirstOrDefaultAsync(s => s.IdProveedor == pProveedor.IdProveedor);
                proveedor.Codigo = pProveedor.Codigo;
                proveedor.Nombre = pProveedor.Nombre;
                proveedor.Direccion = pProveedor.Direccion;
                proveedor.Telefono = pProveedor.Telefono;
                bdContexto.Update(proveedor);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(Proveedor pProveedor)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var proveedor = await bdContexto.Proveedor.FirstOrDefaultAsync(s => s.IdProveedor == pProveedor.IdProveedor);
                bdContexto.Proveedor.Remove(proveedor);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<Proveedor> ObtenerPorIdProveedorAsync(Proveedor pProveedor)
        {
            var proveedor = new Proveedor();
            using (var bdContexto = new BDContexto())
            {
                proveedor = await bdContexto.Proveedor.FirstOrDefaultAsync(s => s.IdProveedor == pProveedor.IdProveedor);
            }
            return proveedor;
        }
        public static async Task<List<Proveedor>> ObtenerTodosAsync()
        {
            var proveedores = new List<Proveedor>();
            using (var bdContexto = new BDContexto())
            {
                proveedores = await bdContexto.Proveedor.ToListAsync();
            }
            return proveedores;
        }
        internal static IQueryable<Proveedor> QuerySelect(IQueryable<Proveedor> pQuery, Proveedor pProveedor)
        {
            if (pProveedor.IdProveedor > 0)
                pQuery = pQuery.Where(s => s.IdProveedor == pProveedor.IdProveedor);
            if (!string.IsNullOrWhiteSpace(pProveedor.Nombre))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pProveedor.Nombre));
            if (!string.IsNullOrWhiteSpace(pProveedor.Direccion))
                pQuery = pQuery.Where(s => s.Direccion.Contains(pProveedor.Direccion));
            if (!string.IsNullOrWhiteSpace(pProveedor.Telefono))
                pQuery = pQuery.Where(s => s.Telefono.Contains(pProveedor.Telefono));

            pQuery = pQuery.OrderByDescending(s => s.IdProveedor).AsQueryable();
            if (pProveedor.Top_Aux > 0)
                pQuery = pQuery.Take(pProveedor.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<Proveedor>> BuscarAsync(Proveedor pProveedor)
        {
            var proveedores = new List<Proveedor>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Proveedor.AsQueryable();
                select = QuerySelect(select, pProveedor);
                proveedores = await select.ToListAsync();
            }
            return proveedores;
        }
    }
}
