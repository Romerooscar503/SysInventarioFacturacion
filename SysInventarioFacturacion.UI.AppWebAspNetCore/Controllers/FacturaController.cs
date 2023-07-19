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
    public class FacturaController : Controller
    {
        FacturaBL FacturaBL = new FacturaBL();
        // GET: FacturaController
        public async Task<IActionResult> Index(Factura? pFactura = null)
        {
            if (pFactura == null)
                pFactura = new Factura();
            if (pFactura.Top_Aux == 0)
                pFactura.Top_Aux = 10;
            else if (pFactura.Top_Aux == -1)
                pFactura.Top_Aux = 0;
            var Factura = await FacturaBL.BuscarAsync(pFactura);
            ViewBag.Top = pFactura.Top_Aux;
            return View(Factura);
        }

        // GET: FacturaController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var Factura = await FacturaBL.ObtenerPorIdAsync(new Factura { IdFactura = id });
            return View(Factura);
        }

        // GET: FacturaController/Create
        public IActionResult Create()
        {
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
                int result = await FacturaBL.AgregarAsync(pFactura);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pFactura);
            }
        }

        // GET: FacturaController/Edit/5
        public async Task<IActionResult> Edit(Factura pFactura)
        {
            var Factura = await FacturaBL.ObtenerPorIdAsync(pFactura);
            ViewBag.Error = "";
            return View(Factura);
        }

        // POST: FacturaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Factura pFactura)
        {
            try
            {
                int result = await FacturaBL.EditarAsync(pFactura);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pFactura);
            }
        }

        // GET: FacturaController/Delete/5
        public async Task<IActionResult> Delete(Factura pFactura)
        {
            var Factura = await FacturaBL.ObtenerPorIdAsync(pFactura);
            return View(Factura);
        }

        // POST: FacturaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Factura pFactura)
        {
            try
            {
                int result = await FacturaBL.EliminarAsync(pFactura);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pFactura);
            }
        }
    }
}
