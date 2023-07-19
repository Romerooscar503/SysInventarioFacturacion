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

namespace SysInventarioFacturacion.UI.AppWebAspNetCore.Controllers
{
   //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class RolController : Controller
    {
        RolBL rolBL = new RolBL();
        // GET: RolController
        public async Task<IActionResult> Index(Rol pRol = null)
        {
            if (pRol == null)
                pRol = new Rol();
            if (pRol.Top_Aux == 0)
                pRol.Top_Aux = 10;
            else if (pRol.Top_Aux == -1)
                pRol.Top_Aux = 0;
            var roles = await rolBL.BuscarAsync(pRol);
            ViewBag.Top = pRol.Top_Aux;
            return View(roles);
        }

        // GET: RolController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var rol = await rolBL.ObtenerPorIdAsync(new Rol { Id = id });
            return View(rol);
        }

        // GET: RolController/Create
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // POST: RolController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Rol pRol)
        {
            try
            {
                int result = await rolBL.CrearAsync(pRol);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pRol);
            }
        }

        // GET: RolController/Edit/5
        public async Task<IActionResult> Edit(Rol pRol)
        {
            var rol = await rolBL.ObtenerPorIdAsync(pRol);
            ViewBag.Error = "";
            return View(rol);
        }

        // POST: RolController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Rol pRol)
        {
            try
            {
                int result = await rolBL.ModificarAsync(pRol);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pRol);
            }
        }

        // GET: RolController/Delete/5
        public async Task<IActionResult> Delete(Rol pRol)
        {
            ViewBag.Error = "";
            var rol = await rolBL.ObtenerPorIdAsync(pRol);
            return View(rol);
        }

        // POST: RolController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Rol pRol)
        {
            try
            {
                int result = await rolBL.EliminarAsync(pRol);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pRol);
            }
        }
    }
}
