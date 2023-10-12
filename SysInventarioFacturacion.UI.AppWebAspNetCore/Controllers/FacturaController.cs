using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/********************************/
using SysInventarioFacturacion.EntidadesDeNegocio;
using SysInventarioFacturacion.LogicaDeNegocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using SysInventarioFacturacion.AccesoADatos;

namespace SysInventarioFacturacion.UI.AppWebAspNetCore.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Authorize(Roles = "Cajero,SuperAdmin")]
    
    public class FacturaController : Controller
    {
        FacturaBL FacturaBL = new FacturaBL();
        UsuarioBL UsuarioBL = new UsuarioBL();
        // GET: FacturaController
        public async Task<IActionResult> Index(Factura pFactura = null)
        {
            if (pFactura == null)
                pFactura = new Factura();
            if (pFactura.Top_Aux == 0)
                pFactura.Top_Aux = 10;
            else if (pFactura.Top_Aux == -1)
                pFactura.Top_Aux = 0;
            var taskBuscar = FacturaBL.BuscarIncluirUsuarioAsync(pFactura);
            var taskObtenerTodosUsuarios = UsuarioBL.ObtenerTodosAsync();
            var Facturas = await taskBuscar;
            ViewBag.Top = pFactura.Top_Aux;
            ViewBag.Usuarios = await taskObtenerTodosUsuarios;
            return View(Facturas);
        }

        // GET: FacturaController/Details/5
        public async Task<IActionResult> Details(int IdFactura)
        {
            var factura = await FacturaBL.ObtenerPorIdAsync(new Factura { IdFactura = IdFactura });
            factura.Usuario = await UsuarioBL.ObtenerPorIdAsync(new Usuario { Id = factura.IdUsuario });
            return View(factura);
        }

        // GET: FacturaController/Create
        
        public async Task<IActionResult> Create()
        {
            ViewBag.Usuarios = await UsuarioBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View();
        }

        // POST: FacturaController/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Factura pFactura)
        {
            try
            {
                int result = await FacturaBL.CrearAsync(pFactura);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Usuarios = await UsuarioBL.ObtenerTodosAsync();
                return View(pFactura);
            }
        }

        // GET: FacturaController/Edit/5
        public async Task<IActionResult> Edit(Factura pFactura)
        {
            var taskObtenerPorId = FacturaBL.ObtenerPorIdAsync(pFactura);
            var taskObtenerTodosUsuarios = UsuarioBL.ObtenerTodosAsync();
            var factura = await taskObtenerPorId;
            ViewBag.Usuarios = await taskObtenerTodosUsuarios;
            ViewBag.Error = "";
            return View(factura);
        }

        // POST: FacturaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdFactura, Factura pFactura)
        {
            try
            {
                int result = await FacturaBL.ModificarAsync(pFactura);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Usuarios = await UsuarioBL.ObtenerTodosAsync();
                return View(pFactura);
            }
        }

        // GET: FacturaController/Delete/5
        public async Task<IActionResult> Delete(Factura pFactura)
        {
            var Factura = await FacturaBL.ObtenerPorIdAsync(pFactura);
            Factura.Usuario = await UsuarioBL.ObtenerPorIdAsync(new Usuario { Id = Factura.IdUsuario });
            ViewBag.Error = "";

            return View(Factura);
        }

        // POST: FacturaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int IdFactura, Factura pFactura)
        {
            try
            {
                int result = await FacturaBL.EliminarAsync(pFactura);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var factura = await FacturaBL.ObtenerPorIdAsync(pFactura);
                if (factura == null)
                    factura = new Factura();
                if (factura.IdFactura > 0)
                    factura.Usuario = await UsuarioBL.ObtenerPorIdAsync(new Usuario { Id = factura.IdUsuario });
                return View(factura);
            }
        }
    }
}
