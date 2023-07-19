using SysInventarioFacturacion.AccesoADatos;
using SysInventarioFacturacion.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacion.LogicaDeNegocio
{
    public class ProveedorBL
    {
        public async Task<int> CrearAsync(Proveedor pProveedor)
        {
            return await ProveedorDAL.CrearAsync(pProveedor);
        }
        public async Task<int> ModificarAsync(Proveedor pProveedor)
        {
            return await ProveedorDAL.ModificarAsync(pProveedor);
        }
        public async Task<int> EliminarAsync(Proveedor pProveedor)
        {
            return await ProveedorDAL.EliminarAsync(pProveedor);
        }
        public async Task<Proveedor> ObtenerPorIdAsync(Proveedor pProveedor)
        {
            return await ProveedorDAL.ObtenerPorIdProveedorAsync(pProveedor);
        }
        public async Task<List<Proveedor>> ObtenerTodosAsync()
        {
            return await ProveedorDAL.ObtenerTodosAsync();
        }
        public async Task<List<Proveedor>> BuscarAsync(Proveedor pProveedor)
        {
            return await ProveedorDAL.BuscarAsync(pProveedor);
        }
    }
}
