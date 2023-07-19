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
    public class RolDALTests
    {
        private static Rol rolInicial = new Rol { Id = 2 };  // Agregar un id existente en la base de datos     
        [TestMethod()]
        public async Task T1CrearAsyncTest()
        {
            var rol = new Rol();
            rol.Nombre = "Administrador";
            int result = await RolDAL.CrearAsync(rol);
            Assert.AreNotEqual(0, result);
            rolInicial.Id = rol.Id;
        }

        [TestMethod()]
        public async Task T2ModificarAsyncTest()
        {
            var rol = new Rol();
            rol.Id = rolInicial.Id;
            rol.Nombre = "Admin";
            int result = await RolDAL.ModificarAsync(rol);
            Assert.AreNotEqual(0, result);
        }

        [TestMethod()]
        public async Task T3ObtenerPorIdAsyncTest()
        {
            var rol = new Rol();
            rol.Id = rolInicial.Id;
            var resultRol = await RolDAL.ObtenerPorIdAsync(rol);
            Assert.AreEqual(rol.Id, resultRol.Id);
        }

        [TestMethod()]
        public async Task T4ObtenerTodosAsyncTest()
        {
            var resultRoles = await RolDAL.ObtenerTodosAsync();
            Assert.AreNotEqual(0, resultRoles.Count);
        }

        [TestMethod()]
        public async Task T5BuscarAsyncTest()
        {
            var rol = new Rol();
            rol.Nombre = "a";
            rol.Top_Aux = 10;
            var resultRoles = await RolDAL.BuscarAsync(rol);
            Assert.AreNotEqual(0, resultRoles.Count);
        }
        [TestMethod()]
        public async Task T6EliminarAsyncTest()
        {
            var rol = new Rol();
            rol.Id = rolInicial.Id;
            int result = await RolDAL.EliminarAsync(rol);
            Assert.AreNotEqual(0, result);
        }
    }
}