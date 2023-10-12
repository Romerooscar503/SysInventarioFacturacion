using SysInventarioFacturacion.AccesoADatos;
using SysInventarioFacturacion.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacion.LogicaDeNegocio
{
	public class ProductoBL
	{
		public async Task<int> CrearAsync(Producto pProducto)
		{
			return await ProductoDAL.CrearAsync(pProducto);
		}
		public async Task<int> ModificarAsync(Producto pProducto)
		{
			return await ProductoDAL.ModificarAsync(pProducto);
		}
        public async Task<int> ModificarExistenciasAsync(Producto pProducto)
        {
            return await ProductoDAL.ModificarExistenciasAsync(pProducto);
        }
        public async Task<int> EliminarAsync(Producto pProducto)
		{
			return await ProductoDAL.EliminarAsync(pProducto);
		}
		public async Task<Producto> ObtenerPorIdProductoAsync(Producto pProducto)
		{
			return await ProductoDAL.ObtenerPorIdProductoAsync(pProducto);
		}
		public async Task<List<Producto>> ObtenerTodosAsync()
		{
			return await ProductoDAL.ObtenerTodosAsync();
		}
		public async Task<List<Producto>> BuscarAsync(Producto pProducto)
		{
			return await ProductoDAL.BuscarAsync(pProducto);
		}
		public async Task<List<Producto>> BuscarIncluirCategoriayProveedorAsync(Producto pProducto)
		{
			return await ProductoDAL.BuscarIncluirCategoriayProveedorAsync(pProducto);
		}
	}
}
