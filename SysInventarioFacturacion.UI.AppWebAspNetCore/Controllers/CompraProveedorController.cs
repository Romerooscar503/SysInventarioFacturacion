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
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;



namespace SysInventarioFacturacion.UI.AppWebAspNetCore.Controllers
{
   // [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class CompraProveedorController : Controller
    {
        CompraProveedorBL compraProveedorBL = new CompraProveedorBL();
        ProveedorBL ProveedorBL = new ProveedorBL();
        // GET: CompraProveedorController
        public async Task<IActionResult> Index(CompraProveedor pCompraProveedor = null)
        {
            if (pCompraProveedor == null)
                pCompraProveedor = new CompraProveedor();
            if (pCompraProveedor.Top_Aux == 0)
                pCompraProveedor.Top_Aux = 10;
            else if (pCompraProveedor.Top_Aux == -1)
                pCompraProveedor.Top_Aux = 0;
            var taskBuscar = compraProveedorBL.BuscarIncluirProveedorAsync(pCompraProveedor);
            var taskObtenerTodosProveedores = ProveedorBL.ObtenerTodosAsync();
            var CompraProveedores = await taskBuscar;
            ViewBag.Top = pCompraProveedor.Top_Aux;
            ViewBag.Proveedores = await taskObtenerTodosProveedores;
            return View(CompraProveedores);
        }

        // GET: CompraProveedorController/Details/5
        public async Task<IActionResult> Details(int IdCompraProveedor, CompraProveedorBL compraProveedorBL)
        {
            var CompraProveedor = await compraProveedorBL.ObtenerPorIdAsync(new CompraProveedor { IdCompraProveedor = IdCompraProveedor });
            CompraProveedor.Proveedor = await ProveedorBL.ObtenerPorIdAsync(new Proveedor { IdProveedor = CompraProveedor.IdProveedor });
            return View(CompraProveedor);
        }

        // GET: CompraProveedorController/Create
       
        public async Task<IActionResult> Create()
        {
            ViewBag.Proveedores = await ProveedorBL.ObtenerTodosAsync();
            ViewBag.Error = "";
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
                ViewBag.Proveedores = await ProveedorBL.ObtenerTodosAsync();
                return View(pCompraProveedor);
            }
        }

        // GET: CompraProveedorController/Edit/5
        public async Task<IActionResult> Edit(CompraProveedor pCompraProveedor)
        {
            var taskObtenerPorIdCompraProveedor = compraProveedorBL.ObtenerPorIdAsync(pCompraProveedor);
            var taskObtenerTodosProveedores = ProveedorBL.ObtenerTodosAsync();
            var CompraProveedor = await taskObtenerPorIdCompraProveedor;
            ViewBag.Proveedores = await taskObtenerTodosProveedores;
            ViewBag.Error = "";
            return View(CompraProveedor);
        }

        // POST: CompraProveedorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdCompraProveedor, CompraProveedor pCompraProveedor)
        {
            try
            {
                int result = await compraProveedorBL.ModificarAsync(pCompraProveedor);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                ViewBag.Proveedores = await ProveedorBL.ObtenerTodosAsync();
                return View(pCompraProveedor);
            }
        }

        // GET: CompraProveedorController/Delete/5
        public async Task<IActionResult> Delete(CompraProveedor pCompraProveedor)
        {
            var CompraProveedor = await compraProveedorBL.ObtenerPorIdAsync(pCompraProveedor);
            CompraProveedor.Proveedor = await ProveedorBL.ObtenerPorIdAsync(new Proveedor { IdProveedor = CompraProveedor.IdProveedor });
            return View(CompraProveedor);
        }

        // POST: CompraProveedorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int IdCompraProveedor, CompraProveedor pCompraProveedor)
        { 
            try
            {
                int result = await compraProveedorBL.EliminarAsync(pCompraProveedor);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                var CompraProveedor = await compraProveedorBL.ObtenerPorIdAsync(pCompraProveedor);
                if (CompraProveedor == null)
                    CompraProveedor = new CompraProveedor();
                if (CompraProveedor.IdCompraProveedor > 0)
                    CompraProveedor.Proveedor = await ProveedorBL.ObtenerPorIdAsync(new Proveedor { IdProveedor = CompraProveedor.IdProveedor });
                return View(pCompraProveedor);
            }
        }
    }
}
