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
				var producto = await bdContexto.Producto.FirstOrDefaultAsync(s => s.IdProducto == pProducto.IdProducto);
                producto.IdCategoria = pProducto.IdCategoria;
                producto.IdProveedor = pProducto.IdProveedor;
                producto.Codigo = pProducto.Codigo;
                producto.Nombre = pProducto.Nombre;
				producto.Descripcion = pProducto.Descripcion;
				producto.Talla = pProducto.Talla;
				producto.Marca = pProducto.Marca;
				producto.Cantidad = pProducto.Cantidad;
				producto.Color = pProducto.Color;
				producto.PrecioUnitario = pProducto.PrecioUnitario;
				bdContexto.Update(producto);
				result = await bdContexto.SaveChangesAsync();
			}
			return result;
		}
		public static async Task<int> EliminarAsync(Producto pProducto)
		{
			int result = 0;
			using (var bdContexto = new BDContexto())
			{
				var producto = await bdContexto.Producto.FirstOrDefaultAsync(s => s.IdProducto == pProducto.IdProducto);
				bdContexto.Producto.Remove(producto);
				result = await bdContexto.SaveChangesAsync();
			}
			return result;
		}
		public static async Task<Producto> ObtenerPorIdProductoAsync(Producto pProducto)
		{
			var producto = new Producto();
			using (var bdContexto = new BDContexto())
			{
				producto = await bdContexto.Producto.FirstOrDefaultAsync(s => s.IdProducto == pProducto.IdProducto);
			}
			return producto;
		}
		public static async Task<List<Producto>> ObtenerTodosAsync()
		{
			var Productos = new List<Producto>();
			using (var bdContexto = new BDContexto())
			{
				Productos = await bdContexto.Producto.ToListAsync();
			}
			return Productos;
		}

		//SIRVE PARA BUSCAR POR FILTRO
		internal static IQueryable<Producto> QuerySelect(IQueryable<Producto> pQuery, Producto pProducto)
		{

            if (pProducto.IdProducto > 0)
                pQuery = pQuery.Where(s => s.IdProducto == pProducto.IdProducto);
            if (pProducto.IdProducto > 0)
                pQuery = pQuery.Where(s => s.IdCategoria == pProducto.IdCategoria);
            if (pProducto.IdProveedor > 0)
                pQuery = pQuery.Where(s => s.IdProveedor == pProducto.IdProveedor);
            if (pProducto.Codigo > 0)
                pQuery = pQuery.Where(s => s.Codigo == pProducto.Codigo);

            //if (pProducto.IdProducto > 0)
            //	pQuery = pQuery.Where(s => s.IdProducto == pProducto.IdProducto);
            if (!string.IsNullOrWhiteSpace(pProducto.Nombre))
				pQuery = pQuery.Where(s => s.Nombre.Contains(pProducto.Nombre));
			//if (!string.IsNullOrWhiteSpace(pProducto.Nombre))

				pQuery = pQuery.OrderByDescending(s => s.IdProducto).AsQueryable();
			if (pProducto.Top_Aux > 0)
				pQuery = pQuery.Take(pProducto.Top_Aux).AsQueryable();
			return pQuery;
		}
		public static async Task<List<Producto>> BuscarAsync(Producto pProducto)
		{
			var Productos = new List<Producto>();
			using (var bdContexto = new BDContexto())
			{
				var select = bdContexto.Producto.AsQueryable();
				select = QuerySelect(select, pProducto);
				Productos = await select.ToListAsync();
			}
			return Productos;
		}
		public static async Task<List<Producto>> BuscarIncluirCategoriayProveedorAsync(Producto pProducto)
		{
			var Productos = new List<Producto>();
			using (var bdContexto = new BDContexto())
			{
				var select = bdContexto.Producto.AsQueryable();
				select = QuerySelect(select, pProducto).Include(s => s.Categoria).Include(s => s.Proveedor).AsQueryable();
                Productos = await select.ToListAsync();
			}
			return Productos;
		}


        public static async Task<int> ModificarExistenciasAsync(Producto pProducto)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var producto = await bdContexto.Producto.FirstOrDefaultAsync(s => s.IdProducto == pProducto.IdProducto);
                producto.Cantidad = pProducto.Cantidad;
                bdContexto.Update(producto);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
    }
}

