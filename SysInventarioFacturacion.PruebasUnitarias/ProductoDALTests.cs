using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SysInventarioFacturacion.AccesoADatos;
using SysInventarioFacturacion.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SysInventarioFacturacion.AccesoADatos.Tests
{
    [TestClass()]
    public class ProductoDALTests
    {
        // Agregar un Id,IdRol,Password existente en la base de datos 
        private static Producto productoInicial = new Producto { IdProducto = 2, IdCategoria = 5, IdProveedor = 4 };
        [TestMethod()]
        public async Task T1CrearAsyncTest()
        {
            var producto = new Producto();
            producto.IdCategoria = productoInicial.IdCategoria;
            producto.IdProveedor = productoInicial.IdProveedor;
            producto.Codigo = 123;
            producto.Nombre = "Nayib Air Force";
            producto.Descripcion = "Zapatos Nayib";
            producto.Talla = "9";
            producto.Marca = "Nayib Shoes";
            producto.Cantidad = 88;
            producto.Color = "Blancos";
            producto.PrecioUnitario = 49; // "M" Es usado para indicar que agregare un valor de tipo decimal - Nestor comment
            int result = await ProductoDAL.CrearAsync(producto);
            Assert.AreNotEqual(0, result);
            productoInicial.IdProducto = producto.IdProducto;
        }

        [TestMethod()]
        public async Task T2ModificarAsyncTest()
        {
            var producto = new Producto();
            producto.IdProducto = productoInicial.IdProducto;
            producto.IdCategoria = productoInicial.IdCategoria;
            producto.IdProveedor = productoInicial.IdProveedor;
            producto.Codigo = 37;
            producto.Nombre = "Nayib F1";
            producto.Descripcion = "Colaboración Nayib";
            producto.Talla = "9";
            producto.Marca = "Nayib Shoes";
            producto.Cantidad = 46;
            producto.Color = "Blancos y Negro";
            producto.PrecioUnitario = 50;
            int result = await ProductoDAL.ModificarAsync(producto);
            Assert.AreNotEqual(0, result);
        }


        [TestMethod()]
        public async Task T3ObtenerPorIdAsyncTest()
        {
            var producto = new Producto();
            producto.IdProducto = productoInicial.IdProducto;
            var resultProducto = await ProductoDAL.ObtenerPorIdProductoAsync(producto);
            Assert.AreEqual(producto.IdProducto, resultProducto.IdProducto);
        }

        [TestMethod()]
        public async Task T4ObtenerTodosAsyncTest()
        {
            var resultProductos = await ProductoDAL.ObtenerTodosAsync();
            Assert.AreNotEqual(0, resultProductos.Count);
        }

        [TestMethod()]
        public async Task T5BuscarAsyncTest()
        {
            var producto = new Producto();
            producto.IdCategoria = productoInicial.IdCategoria;
            producto.IdProveedor = productoInicial.IdProveedor;
            producto.Codigo = 34;
            producto.Nombre = "Adidas";
            producto.Descripcion = "Adidas Concha Blancos para niños";
            producto.Talla = "8";
            producto.Marca = "Adidas";
            producto.Cantidad = 20;
            producto.Color = "Blanco";
            producto.PrecioUnitario = 89;
            producto.Top_Aux = 10;
            var resultProductos = await ProductoDAL.BuscarAsync(producto);
            Assert.AreNotEqual(0, resultProductos.Count);
        }

        [TestMethod()]
        public async Task T6BuscarIncluirCategoriayProveedorAsyncTest()
        {
            var producto = new Producto();
            producto.IdCategoria = productoInicial.IdCategoria;
            producto.IdProveedor = productoInicial.IdProveedor;
            producto.Codigo = 37;
            producto.Nombre = "Zapatillas";
            producto.Descripcion = "Zapatillas lindas para bebe";
            producto.Talla = "6";
            producto.Marca = "ZapaBaby";
            producto.Cantidad = 3;
            producto.Color = "Rosado";
            producto.PrecioUnitario = 23; // Usa un valor decimal
            producto.Top_Aux = 10;
            var resultProductos = await ProductoDAL.BuscarIncluirCategoriayProveedorAsync(producto);
            Assert.AreNotEqual(0, resultProductos.Count);
            var ultimoProducto = resultProductos.FirstOrDefault();
            Assert.IsTrue(ultimoProducto.Categoria != null && producto.IdCategoria == ultimoProducto.Categoria.IdCategoria);
            Assert.IsTrue(ultimoProducto.Proveedor != null && producto.IdProveedor == ultimoProducto.Proveedor.IdProveedor);
        }

        [TestMethod()]
        public async Task T7EliminarAsyncTest()
        {
            var producto = new Producto();
            producto.IdProducto = productoInicial.IdProducto;
            int result = await ProductoDAL.EliminarAsync(producto);
            Assert.AreNotEqual(0, result);
        }

    }

}