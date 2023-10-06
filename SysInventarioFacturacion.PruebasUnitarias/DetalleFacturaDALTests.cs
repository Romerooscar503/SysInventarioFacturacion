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
    public class DetalleFacturaDALTests
    {
        // Agregar un Id,IdRol,Password existente en la base de datos 
        private static DetalleFactura detallefacturaInicial = new DetalleFactura { IdDetalleFactura = 7, IdFactura = 6, IdProducto = 5};
        [TestMethod()]
        public async Task T1CrearAsyncTest()
        {
            var detalleFactura = new DetalleFactura();
            detalleFactura.IdFactura = detallefacturaInicial.IdFactura;
            detalleFactura.IdProducto = detallefacturaInicial.IdProducto;
            detalleFactura.Codigo = 234;
            detalleFactura.Cantidad = 59;
            detalleFactura.FormaDePago = "efectivo";
            detalleFactura.ValorTotal = 56;
            int result = await DetalleFacturaDAL.CrearAsync(detalleFactura);
            Assert.AreNotEqual(0, result);
            detallefacturaInicial.IdDetalleFactura = detalleFactura.IdDetalleFactura;
        }

        [TestMethod()]
        public async Task T2ModificarAsyncTest()
        {
            var detalleFactura = new DetalleFactura();
            detalleFactura.IdDetalleFactura = detallefacturaInicial.IdDetalleFactura;
            detalleFactura.IdFactura = detallefacturaInicial.IdFactura;
            detalleFactura.IdProducto = detallefacturaInicial.IdProducto;
            detalleFactura.Codigo = 4;
            detalleFactura.Cantidad = 5;
            detalleFactura.FormaDePago = "JuanUser01";
            detalleFactura.ValorTotal = 36;
            int result = await DetalleFacturaDAL.ModificarAsync(detalleFactura);
            Assert.AreNotEqual(0, result);
        }

       
        [TestMethod()]
        public async Task T3ObtenerPorIdAsyncTest()
        {
            var detalleFactura = new DetalleFactura();
            detalleFactura.IdDetalleFactura = detallefacturaInicial.IdDetalleFactura;
            var resultDetalleFactura = await DetalleFacturaDAL.ObtenerPorIdAsync(detalleFactura);
            Assert.AreEqual(detalleFactura.IdDetalleFactura, resultDetalleFactura.IdDetalleFactura);
        }

        [TestMethod()]
        public async Task T4ObtenerTodosAsyncTest()
        {
            var resultDetalleFacturas = await DetalleFacturaDAL.ObtenerTodosAsync();
            Assert.AreNotEqual(0, resultDetalleFacturas.Count);
        }

        [TestMethod()]
        public async Task T5BuscarAsyncTest()
        {
            var detalleFactura = new DetalleFactura();
            detalleFactura.IdFactura = detallefacturaInicial.IdFactura;
            detalleFactura.IdProducto = detallefacturaInicial.IdProducto;
            detalleFactura.Codigo = 4;
            detalleFactura.Cantidad = 5;
            detalleFactura.FormaDePago = "use";
            detalleFactura.ValorTotal = 56;
            detalleFactura.Top_Aux = 10;
            var resultDetalleFacturas = await DetalleFacturaDAL.BuscarAsync(detalleFactura);
            Assert.AreNotEqual(0, resultDetalleFacturas.Count);
        }

        [TestMethod()]
        public async Task T6BuscarIncluirFacturasYProductoAsyncTest()
        {
            var detalleFactura = new DetalleFactura();
            detalleFactura.IdFactura = detallefacturaInicial.IdFactura;
            detalleFactura.IdProducto = detallefacturaInicial.IdProducto;
            detalleFactura.Codigo = 4;
            detalleFactura.Cantidad = 5;
            detalleFactura.FormaDePago = "use";
            detalleFactura.ValorTotal = 56;
            detalleFactura.Top_Aux = 10;
            var resultDetalleFacturas = await DetalleFacturaDAL.BuscarIncluirFacturasYProductoAsync(detalleFactura);
            Assert.AreNotEqual(0, resultDetalleFacturas.Count);
            var ultimoDetalleFactura = resultDetalleFacturas.FirstOrDefault();
            Assert.IsTrue(ultimoDetalleFactura.Factura != null && detalleFactura.IdFactura == ultimoDetalleFactura.Factura.IdFactura);
            Assert.IsTrue(ultimoDetalleFactura.Producto != null && detalleFactura.IdProducto == ultimoDetalleFactura.Producto.IdProducto);
        }
         
        [TestMethod()]
        public async Task T7EliminarAsyncTest()
        {
            var detalleFactura = new DetalleFactura();
            detalleFactura.IdDetalleFactura = detallefacturaInicial.IdDetalleFactura;
            int result = await DetalleFacturaDAL.EliminarAsync(detalleFactura);
            Assert.AreNotEqual(0, result);
        }

    }

}