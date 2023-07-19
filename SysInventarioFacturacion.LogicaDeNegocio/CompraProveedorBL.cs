using SysInventarioFacturacion.AccesoADatos;
using SysInventarioFacturacion.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacion.LogicaDeNegocio
{


    public class CompraProveedorBL
    {
        public async Task<int> CrearAsync(CompraProveedor pCompraProveedor)
        {
            return await CompraProveedorDAL.CrearAsync(pCompraProveedor);
        }
        public async Task<int> ModificarAsync(CompraProveedor pCompraProveedor)
        {
            return await CompraProveedorDAL.ModificarAsync(pCompraProveedor);
        }
        public async Task<int> EliminarAsync(CompraProveedor pCompraProveedor)
        {
            return await CompraProveedorDAL.EliminarAsync(pCompraProveedor);
        }
        public async Task<CompraProveedor> ObtenerPorIdAsync(CompraProveedor pCompraProveedor)
        {
            return await CompraProveedorDAL.ObtenerPorIdAsync(pCompraProveedor);
        }
        public async Task<List<CompraProveedor>> ObtenerTodosAsync()
        {
            return await CompraProveedorDAL.ObtenerTodosAsync();
        }
        public async Task<List<CompraProveedor>> BuscarAsync(CompraProveedor pCompraProveedor)
        {
            return await CompraProveedorDAL.BuscarAsync(pCompraProveedor);
        }


    }
}
