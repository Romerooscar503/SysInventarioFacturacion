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
    public class UsuarioDALTests
    {
        // Agregar un Id,IdRol,Password existente en la base de datos 
        private static Usuario usuarioInicial = new Usuario { Id = 7, IdRol = 1, Login = "JuanUser", Password = "12345" };
        [TestMethod()]
        public async Task T1CrearAsyncTest()
        {
            var usuario = new Usuario();
            usuario.IdRol = usuarioInicial.IdRol;
            usuario.Nombre = "juan";
            usuario.Apellido = "lopez";
            usuario.Login = "juanUser";
            string password = "12345";
            usuario.Password = password;
            usuario.Estatus = (byte)Estatus_Usuario.INACTIVO;
            int result = await UsuarioDAL.CrearAsync(usuario);
            Assert.AreNotEqual(0, result);
            usuarioInicial.Id = usuario.Id;
            usuarioInicial.Password = password;
            usuarioInicial.Login = usuario.Login;
        }

        [TestMethod()]
        public async Task T2ModificarAsyncTest()
        {
            var usuario = new Usuario();
            usuario.Id = usuarioInicial.Id;
            usuario.IdRol = usuarioInicial.IdRol;
            usuario.Nombre = "Juan";
            usuario.Apellido = "Lopez";
            usuario.Login = "JuanUser01";
            usuario.Estatus = (byte)Estatus_Usuario.ACTIVO;
            int result = await UsuarioDAL.ModificarAsync(usuario);
            Assert.AreNotEqual(0, result);
            usuarioInicial.Login = usuario.Login;
        }

        [TestMethod()]
        public async Task T3ObtenerPorIdAsyncTest()
        {
            var usuario = new Usuario();
            usuario.Id = usuarioInicial.Id;
            var resultUsuario = await UsuarioDAL.ObtenerPorIdAsync(usuario);
            Assert.AreEqual(usuario.Id, resultUsuario.Id);
        }
        [TestMethod()]
        public async Task T4ObtenerTodosAsyncTest()
        {
            var resultUsuarios = await UsuarioDAL.ObtenerTodosAsync();
            Assert.AreNotEqual(0, resultUsuarios.Count);
        }
        [TestMethod()]
        public async Task T5BuscarAsyncTest()
        {
            var usuario = new Usuario();
            usuario.IdRol = usuarioInicial.IdRol;
            usuario.Nombre = "A";
            usuario.Apellido = "a";
            usuario.Login = "A";
            usuario.Estatus = (byte)Estatus_Usuario.ACTIVO;
            usuario.Top_Aux = 10;
            var resultUsuarios = await UsuarioDAL.BuscarAsync(usuario);
            Assert.AreNotEqual(0, resultUsuarios.Count);
        }
        [TestMethod()]
        public async Task T6BuscarIncluirRolesAsync()
        {
            var usuario = new Usuario();
            usuario.IdRol = usuarioInicial.IdRol;
            usuario.Nombre = "A";
            usuario.Apellido = "a";
            usuario.Login = "A";
            usuario.Estatus = (byte)Estatus_Usuario.ACTIVO;
            usuario.Top_Aux = 10;
            var resultUsuarios = await UsuarioDAL.BuscarIncluirRolesAsync(usuario);
            Assert.AreNotEqual(0, resultUsuarios.Count);
            var ultimoUsuario = resultUsuarios.FirstOrDefault();
            Assert.IsTrue(ultimoUsuario.Rol != null && usuario.IdRol == ultimoUsuario.Rol.Id);
        }
        [TestMethod()]
        public async Task T7CambiarPasswordAsyncTest()
        {
            var usuario = new Usuario();
            usuario.Id = usuarioInicial.Id;
            string passwordNuevo = "123456";
            usuario.Password = passwordNuevo;
            var result = await UsuarioDAL.CambiarPasswordAsync(usuario, usuarioInicial.Password);
            Assert.AreNotEqual(0, result);
            usuarioInicial.Password = passwordNuevo;
        }
        [TestMethod()]
        public async Task T8LoginAsyncTest()
        {
            var usuario = new Usuario();
            usuario.Login = usuarioInicial.Login;
            usuario.Password = usuarioInicial.Password;
            var resultUsuario = await UsuarioDAL.LoginAsync(usuario);
            Assert.AreEqual(usuario.Login, resultUsuario.Login);
        }
        [TestMethod()]
        public async Task T9EliminarAsyncTest()
        {
            var usuario = new Usuario();
            usuario.Id = usuarioInicial.Id;
            int result = await UsuarioDAL.EliminarAsync(usuario);
            Assert.AreNotEqual(0, result);
        }
    }
}