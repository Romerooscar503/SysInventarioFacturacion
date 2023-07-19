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

{    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ProveedorController : Controller
    {    
        ProveedorBL ProveedorBL = new ProveedorBL();
        // GET: ProveedorController
        public async Task<IActionResult> Index(Proveedor? pProveedor = null)
        {
            if (pProveedor == null)
               pProveedor = new Proveedor();
            if (pProveedor.Top_Aux == 0)
                pProveedor.Top_Aux = 10;
            else if (pProveedor.Top_Aux == -1)
                pProveedor.Top_Aux = 0;
            var Proveedor = await ProveedorBL.BuscarAsync(pProveedor);
            ViewBag.Top = pProveedor.Top_Aux;
            return View(Proveedor);
        }

        // GET: ProveedorController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var Proveedor = await ProveedorBL.ObtenerPorIdAsync(new Proveedor { Id = id });
            return View(Proveedor);
        }

        // GET: ProveedorController/Create
        public  IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // POST: ProveedorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Proveedor pProveedor)
        {
            try
            {   
                int result = await ProveedorBL.CrearAsync(pProveedor);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pProveedor);
            }
        }

        // GET: ProveedorController/Edit/5
        public async Task<IActionResult> Edit(Proveedor pProveedor)
        {
            var Proveedor = await ProveedorBL.ObtenerPorIdAsync(pProveedor);
            ViewBag.Error = "";
            return View(Proveedor);
        }

        // POST: ProveedorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Proveedor pProveedor)
        {
            try
            {
                int result = await ProveedorBL.ModificarAsync(pProveedor);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {   
                ViewBag.Error = ex.Message;
                return View(pProveedor);
            }
        }

        // GET: ProveedorController/Delete/5
        public async Task<IActionResult> Delete(Proveedor pProveedor)
        {
            var Proveedor = await ProveedorBL.ObtenerPorIdAsync(pProveedor);
            return View(Proveedor);
        }

        // POST: ProveedorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Proveedor pProveedor)
        {
            try
            {   
                int result = await ProveedorBL.EliminarAsync(pProveedor);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {   
                ViewBag.Error = ex.Message;
                return View(pProveedor);
            }
        }
    }
}
