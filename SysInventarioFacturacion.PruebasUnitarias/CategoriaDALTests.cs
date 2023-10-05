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
    // Agregar un id existente en la base de datos  
    [TestClass()]
    public class CategoriaDALTests
    {
        private static Categoria categoriaInicial = new Categoria { IdCategoria = 21 };
        [TestMethod()]
        public async Task T1CrearAsync()
        {
            var categoria = new Categoria();
            categoria.Codigo = 34;
            categoria.Nombre = "zapatos de balec";
            categoria.Descripcion = "zapatos para bailiar";
            int result = await CategoriaDAL.CrearAsync(categoria);
            Assert.AreNotEqual(0, result);
            categoriaInicial.IdCategoria = categoria.IdCategoria;
        }

        [TestMethod()]
        public async Task T2ModificarAsyncTest()
        {
            var categoria = new Categoria();
            categoria.IdCategoria = categoriaInicial.IdCategoria;
            categoria.Codigo = 34;
            categoria.Nombre = "zapatos de balec";
            categoria.Descripcion = "zapatos para bailiar";
            int result = await CategoriaDAL.ModificarAsync(categoria);
            Assert.AreNotEqual(0, result);
        }

        [TestMethod()]
        public async Task T3ObtenerPorIdCategoriaAsyncTest()
        {

            var categoria = new Categoria();
            categoria.IdCategoria = categoriaInicial.IdCategoria;
            var resultCategoria = await CategoriaDAL.ObtenerPorIdCategoriaAsync(categoria);
            Assert.AreEqual(categoria.IdCategoria, resultCategoria.IdCategoria);
        }

        [TestMethod()]
        public async Task T4ObtenerTodosAsyncTest()
        {
            var resultCategorias = await CategoriaDAL.ObtenerTodosAsync();
            Assert.AreNotEqual(0, resultCategorias.Count);
        }

        [TestMethod()]
        public async Task T5BuscarAsyncTest()
        {
            var categoria = new Categoria();
            categoria.Codigo = 34;
            categoria.Nombre = "zapatos de balec";
            categoria.Descripcion = "zapatos para bailiar";
            var resultCategorias = await CategoriaDAL.BuscarAsync(categoria);
            Assert.AreNotEqual(0, resultCategorias.Count);
        }
        [TestMethod()]
        public async Task T6EliminarAsyncTest()
        {
            var categoria = new Categoria();
            categoria.IdCategoria = categoriaInicial.IdCategoria;
            int result = await CategoriaDAL.EliminarAsync(categoria);
            Assert.AreNotEqual(0, result);
        }
    }
}