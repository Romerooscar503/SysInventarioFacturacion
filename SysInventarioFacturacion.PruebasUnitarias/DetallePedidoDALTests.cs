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
    public class DetallePedidoDALTests
    {
        private static DetallePedido detallepedidoInicial = new DetallePedido { IdDetallePedido = 12, IdProveedor = 38, IdProducto = 30, IdPedido = 1 };
        [TestMethod()]
        public async Task T1CrearAsyncTest()
        {
            var detallePedido = new DetallePedido();
            detallePedido.IdProveedor = detallepedidoInicial.IdProveedor;
            detallePedido.IdProducto = detallepedidoInicial.IdProducto;
            detallePedido.IdPedido = detallepedidoInicial.IdPedido;
            detallePedido.Cantidad = 234;
            int result = await DetallePedidoDAL.CrearAsync(detallePedido);
            Assert.AreNotEqual(0, result);
            detallepedidoInicial.IdDetallePedido = detallePedido.IdDetallePedido;
        }

        [TestMethod()]
        public async Task T2ModificarAsyncTest()
        {
            var detallePedido = new DetallePedido();
            detallePedido.IdDetallePedido = detallepedidoInicial.IdDetallePedido;
            detallePedido.IdPedido = detallepedidoInicial.IdPedido;
            detallePedido.IdProducto = detallepedidoInicial.IdProducto;
            detallePedido.IdPedido = detallepedidoInicial.IdPedido;
            detallePedido.Cantidad = 45; ;
            int result = await DetallePedidoDAL.ModificarAsync(detallePedido);
            Assert.AreNotEqual(0, result);
        }


        [TestMethod()]
        public async Task T3ObtenerPorIdAsyncTest()
        {
            var detallePedido = new DetallePedido();
            detallePedido.IdDetallePedido = detallepedidoInicial.IdDetallePedido;
            var resultDetallePedido = await DetallePedidoDAL.ObtenerPorIdAsync(detallePedido);
            Assert.AreEqual(detallePedido.IdDetallePedido, resultDetallePedido.IdDetallePedido);
        }

        [TestMethod()]
        public async Task T4ObtenerTodosAsyncTest()
        {
            var resultDetallePedidos = await DetallePedidoDAL.ObtenerTodosAsync();
            Assert.AreNotEqual(0, resultDetallePedidos.Count);
        }

        [TestMethod()]
        public async Task T5BuscarAsyncTest()
        {
            var detallePedido = new DetallePedido();
            detallePedido.IdPedido = detallepedidoInicial.IdPedido;
            detallePedido.IdProducto = detallepedidoInicial.IdProducto;
            detallePedido.IdPedido = detallepedidoInicial.IdPedido;
            detallePedido.Cantidad = 4;
            var resultDetallePedidos = await DetallePedidoDAL.BuscarAsync(detallePedido);
            Assert.AreNotEqual(0, resultDetallePedidos.Count);
        }

        [TestMethod()]
        public async Task T6BuscarIncluirPedidoProductoProveedorAsyncTest()
        {
            var detallePedido = new DetallePedido();
            detallePedido.IdPedido = detallepedidoInicial.IdPedido;
            detallePedido.IdProducto = detallepedidoInicial.IdProducto;
            detallePedido.Cantidad = 4;
            var resultDetallePedidos = await DetallePedidoDAL.BuscarIncluirPedidoProductoProveedorAsync(detallePedido);
            Assert.AreNotEqual(0, resultDetallePedidos.Count);
            var ultimoDetallePedido = resultDetallePedidos.FirstOrDefault();
            Assert.IsTrue(ultimoDetallePedido.Pedido != null && detallePedido.IdPedido == ultimoDetallePedido.Pedido.IdPedido);
            Assert.IsTrue(ultimoDetallePedido.Producto != null && detallePedido.IdProducto == ultimoDetallePedido.Producto.IdProducto);
            Assert.IsTrue(ultimoDetallePedido.Proveedor != null && detallePedido.IdProveedor == ultimoDetallePedido.Producto.IdProveedor);

        }

        [TestMethod()]
        public async Task T7EliminarAsyncTest()
        {
            var detallePedido = new DetallePedido();
            detallePedido.IdDetallePedido = detallepedidoInicial.IdDetallePedido;
            int result = await DetallePedidoDAL.EliminarAsync(detallePedido);
            Assert.AreNotEqual(0, result);
        }

    }

}