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
    public class PedidoDALTests
    {
        // Agregar un Id,IdRol,Password existente en la base de datos 
        private static Pedido pedidoInicial = new Pedido { IdPedido = 1,  };
        [TestMethod()]
        public async Task T1CrearAsyncTest()
        {
            var pedido = new Pedido();
            pedido.IdUsuario = pedidoInicial.IdUsuario;
            pedido.IdUsuario = 20;
            pedido.Telefono = "17171818";
            int result = await PedidoDAL.CrearAsync(pedido);
            Assert.AreNotEqual(0, result);
            pedidoInicial.IdPedido = pedido.IdPedido;
            pedidoInicial.DetallePedido = pedido.DetallePedido;
            
        }

        [TestMethod()]
        public async Task T2ModificarAsyncTest()
        {
            var pedido = new Pedido();
            pedido.IdPedido = pedidoInicial.IdPedido;
            pedido.IdUsuario = pedidoInicial.IdUsuario;
            pedido.IdUsuario = 20;
            pedido.Telefono = "17171919";
            int result = await PedidoDAL.ModificarAsync(pedido);
            Assert.AreNotEqual(0, result);
            pedidoInicial.IdPedido = pedido.IdPedido;
            pedidoInicial.IdUsuario = pedido.IdUsuario;
            pedidoInicial.DetallePedido = pedido.DetallePedido;

        }

        [TestMethod()]
        public async Task T3ObtenerPorIdAsyncTest()
        {
            var pedido = new Pedido();
            pedido.IdPedido = pedidoInicial.IdPedido;
            var resultPedido = await PedidoDAL.ObtenerPorIdAsync(pedido);
            Assert.AreEqual(pedido.IdPedido, resultPedido.IdPedido);
        }
        [TestMethod()]
        public async Task T4ObtenerTodosAsyncTest()
        {
            var resultPedidos = await PedidoDAL.ObtenerTodosAsync();
            Assert.AreNotEqual(0, resultPedidos.Count);
        }
        [TestMethod()]
        public async Task T5BuscarAsyncTest()
        {
            var pedido = new Pedido();
            pedido.IdPedido = pedidoInicial.IdPedido;
            pedido.IdUsuario = pedidoInicial.IdUsuario;
            pedido.IdUsuario = 20;
            pedido.Telefono = "17171818";
            var resultPedidos = await PedidoDAL.BuscarAsync(pedido);
            Assert.AreNotEqual(0, resultPedidos.Count);
        }
       
        [TestMethod()]
        public async Task T6EliminarAsyncTest()
        {
            var pedido = new Pedido();
            pedido.IdPedido = pedidoInicial.IdPedido;
            int result = await PedidoDAL.EliminarAsync(pedido);
            Assert.AreNotEqual(0, result);
        }
    }
}