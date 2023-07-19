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
    public class CompraProveedorController : Controller
    {
        CompraProveedorBL compraProveedorBL = new CompraProveedorBL();
        // GET: CompraProveedorController
        public async Task<IActionResult> Index(CompraProveedor pCompraProveedor = null)
        {
            if (pCompraProveedor == null)
                pCompraProveedor = new CompraProveedor();
            if (pCompraProveedor.Top_Aux == 0)
                pCompraProveedor.Top_Aux = 10;
            else if (pCompraProveedor.Top_Aux == -1)
                pCompraProveedor.Top_Aux = 0;
            var CompraProveedor = await compraProveedorBL.BuscarAsync(pCompraProveedor);
            ViewBag.Top = pCompraProveedor.Top_Aux;
            return View(CompraProveedor);
        }

        // GET: CompraProveedorController/Details/5
        public async Task<IActionResult> Details(int id, CompraProveedorBL compraProveedorBL)
        {
            var CompraProveedor = await compraProveedorBL.ObtenerPorIdAsync(new CompraProveedor { Id = id });
            return View(CompraProveedor);
        }

        // GET: CompraProveedorController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CompraProveedorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompraProveedor pCompraProveedor)
        {
            try
            {
                int result = await compraProveedorBL.CrearAsync(pCompraProveedor);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(pCompraProveedor);
            }
        }

        // GET: CompraProveedorController/Edit/5
        public async Task<IActionResult> Edit(CompraProveedor pCompraProveedor)
        {
            var CompraProveedor = await compraProveedorBL.ObtenerPorIdAsync(pCompraProveedor);
            ViewBag.Error = "";
            return View(CompraProveedor);
        }

        // POST: CompraProveedorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CompraProveedor pCompraProveedor)
        {
            try
            {
                int result = await compraProveedorBL.ModificarAsync(pCompraProveedor);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(pCompraProveedor);
            }
        }

        // GET: CompraProveedorController/Delete/5
        public async Task<IActionResult> Delete(CompraProveedor pCompraProveedor)
        {
            var CompraProveedor = await compraProveedorBL.ObtenerPorIdAsync(pCompraProveedor);
            return View(CompraProveedor);
        }

        // POST: CompraProveedorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, CompraProveedor pCompraProveedor)
        { 
            try
            {
                int result = await compraProveedorBL.EliminarAsync(pCompraProveedor);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(pCompraProveedor);
            }
        }
    }
}
