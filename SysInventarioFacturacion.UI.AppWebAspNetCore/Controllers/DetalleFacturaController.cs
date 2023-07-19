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
    public class DetalleFacturaController : Controller
    {
        DetalleFacturaBL detalle_facturaBL = new DetalleFacturaBL();
        // GET: RolController
        public async Task<IActionResult> Index(DetalleFactura pDetalleFactura = null)
        {
            if (pDetalleFactura == null)
                pDetalleFactura = new DetalleFactura();
            if (pDetalleFactura.Top_Aux == 0)
                pDetalleFactura.Top_Aux = 10;
            else if (pDetalleFactura.Top_Aux == -1)
                pDetalleFactura.Top_Aux = 0;
            var DetalleFactura = await detalle_facturaBL.BuscarAsync(pDetalleFactura);
            ViewBag.Top = pDetalleFactura.Top_Aux;
            return View(DetalleFactura);
        }

        // GET: DetalleFacturaController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var DetalleFactura = await detalle_facturaBL.ObtenerPorIdAsync(new DetalleFactura { Id = id });
            return View(DetalleFactura);
        }

        // GET: DetalleFacturaController/Create
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // POST: DeatlleFacturaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DetalleFactura pDetalleFactura)
        {
            try
            {
                int result = await detalle_facturaBL.CrearAsync(pDetalleFactura);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pDetalleFactura);
            }
        }

        // GET: DeatlleFacturaController/Edit/5
        public async Task<IActionResult> Edit(DetalleFactura pDetalleFactura)
        {
            var DetalleFactura = await detalle_facturaBL.ObtenerPorIdAsync(pDetalleFactura);
            ViewBag.Error = "";
            return View(DetalleFactura);
        }

        // POST: DetalleFacturaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DetalleFactura pDetalleFactura)
        {
            try
            {
                int result = await detalle_facturaBL.ModificarAsync(pDetalleFactura);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pDetalleFactura);
            }
        }

        // GET: DetalleFacturaController/Delete/5
        public async Task<IActionResult> Delete(DetalleFactura pDetalleFactura)
        {
            ViewBag.Error = "";
            var DetalleFactura = await detalle_facturaBL.ObtenerPorIdAsync(pDetalleFactura);
            return View(DetalleFactura);
        }

        // POST: DetalleFacturaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DetalleFactura pDetalleFactura)
        {
            try
            {
                int result = await detalle_facturaBL.EliminarAsync(pDetalleFactura);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pDetalleFactura);
            }
        }
    }
}
