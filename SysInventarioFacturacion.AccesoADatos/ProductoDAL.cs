using Microsoft.EntityFrameworkCore;
using SysInventarioFacturacion.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacion.AccesoADatos
{
    public class ProductoDAL
    {
        public static async Task<int> CrearAsync(Producto pProducto)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pProducto);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModificarAsync(Producto pProducto)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var Producto = await bdContexto.Producto.FirstOrDefaultAsync(s => s.IdProducto == pProducto.IdProducto);
                Producto.Nombre = pProducto.Nombre;
                bdContexto.Update(Producto);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(Producto pProducto)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var Producto = await bdContexto.Producto.FirstOrDefaultAsync(s => s.IdProducto == pProducto.IdProducto);
                bdContexto.Producto.Remove(Producto);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<Producto> ObtenerPorIdProductoAsync(Producto pProducto)
        {
            var Producto = new Producto();
            using (var bdContexto = new BDContexto())
            {
                Producto = await bdContexto.Producto.FirstOrDefaultAsync(s => s.IdProducto == pProducto.IdProducto);
            }
            return Producto;
        }
        public static async Task<List<Producto>> ObtenerTodosAsync()
        {
            var Productoes = new List<Producto>();
            using (var bdContexto = new BDContexto())
            {
                Productoes = await bdContexto.Producto.ToListAsync();
            }
            return Productoes;
        }
        internal static IQueryable<Producto> QuerySelect(IQueryable<Producto> pQuery, Producto pProducto)
        {
            if (pProducto.IdProducto > 0)
                pQuery = pQuery.Where(s => s.IdProducto == pProducto.IdProducto);
            if (!string.IsNullOrWhiteSpace(pProducto.Nombre))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pProducto.Nombre));
            pQuery = pQuery.OrderByDescending(s => s.IdProducto).AsQueryable();
            if (pProducto.Top_Aux > 0)
                pQuery = pQuery.Take(pProducto.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<Producto>> BuscarAsync(Producto pProducto)
        {
            var Productoes = new List<Producto>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Producto.AsQueryable();
                select = QuerySelect(select, pProducto);
                Productoes = await select.ToListAsync();
            }
            return Productoes;
        }
    }
}

