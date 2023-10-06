using Microsoft.VisualStudio.TestTools.UnitTesting;
using SysInventarioFacturacion.AccesoADatos;
using SysInventarioFacturacion.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SysInventarioFacturacion.AccesoADatos.Tests
{
    [TestClass()]
    public class ProveedorDALTests
    {
        private static Proveedor proveedorInicial = new Proveedor { IdProveedor = 13 };
        [TestMethod()]
        public async Task T1CrearAsync()
        {
            var proveedor = new Proveedor();
            proveedor.Codigo = 266;
            proveedor.Nombre = "ALFREDO";
            proveedor.Direccion = "Soyapango";
            proveedor.Telefono = "9705868";
            proveedor.Correo = "geje@gmail.com";
            int result = await ProveedorDAL.CrearAsync(proveedor);
            Assert.AreNotEqual(0, result);
            proveedorInicial.IdProveedor = proveedor.IdProveedor;
        }

        [TestMethod()]
        public async Task  T2ModificarAsyncTest()
        {
            var proveedor = new Proveedor();
            proveedor.IdProveedor = proveedorInicial.IdProveedor;
            proveedor.Codigo = 3;
            proveedor.Nombre = "CARLOSsss";
            proveedor.Direccion = "Soyapango";
            proveedor.Telefono = "77669889";
            proveedor.Correo = "charly@gmail.com";
            int result = await ProveedorDAL.ModificarAsync(proveedor);
            Assert.AreNotEqual(0, result);
        }

        [TestMethod()]
        public async Task T3ObtenerPorIdProveedorAsyncTest()
        {

            var proveedor = new Proveedor();
            proveedor.IdProveedor = proveedorInicial.IdProveedor;
            var resultProveedor = await ProveedorDAL.ObtenerPorIdProveedorAsync(proveedor);
            Assert.AreEqual(proveedor.IdProveedor, proveedor.IdProveedor);
        }

        [TestMethod()]
        public async Task T4ObtenerTodosAsyncTest()
        {
            var resultProveedores = await ProveedorDAL.ObtenerTodosAsync();
            Assert.AreNotEqual(0, resultProveedores.Count);
        }

        [TestMethod()]
        public async Task T5BuscarAsyncTest()
        {
            var proveedor = new  Proveedor();
            proveedor.Codigo = 3;
            proveedor.Nombre = "a";         
            proveedor.Direccion = "s";
            proveedor.Telefono = "7";
            proveedor.Correo = "c";

            var resultProveedores = await ProveedorDAL.BuscarAsync(proveedor);
            Assert.AreNotEqual(0, resultProveedores.Count);
        }
        [TestMethod()]
        public async Task T6EliminarAsyncTest()
        {
            var proveedor = new Proveedor();
            proveedor.IdProveedor = proveedorInicial.IdProveedor;
            int result = await ProveedorDAL.EliminarAsync(proveedor);
            Assert.AreNotEqual(0, result);
        }
    }
}