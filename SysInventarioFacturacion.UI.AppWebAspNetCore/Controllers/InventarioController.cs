﻿using Microsoft.AspNetCore.Http;
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
    public class InventarioController : Controller
    {
        InventarioBL InventarioBL = new InventarioBL();
        // GET: InventarioController
        public async Task<IActionResult> Index(Inventario pInventario = null)
        {
            if (pInventario == null)
                pInventario = new Inventario();
            if (pInventario.Top_Aux == 0)
                pInventario.Top_Aux = 10;
            else if (pInventario.Top_Aux == -1)
                pInventario.Top_Aux = 0;
            var Inventarios = await InventarioBL.BuscarAsync(pInventario);
            ViewBag.Top = pInventario.Top_Aux;
            return View(Inventarios);
        }

        // GET: InventarioController/Details/5
        public async Task<IActionResult> Details(int IdInventario)
        {
            var inventario = await InventarioBL.ObtenerPorIdAsync(new Inventario { IdInventario = IdInventario });
            return View(inventario);
        }

        // GET: InventarioController/Create
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // POST: InventarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Inventario pInventario)
        {
            try
            {
                int result = await InventarioBL.CrearAsync(pInventario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pInventario);
            }
        }

        // GET: InventarioController/Edit/5
        public async Task<IActionResult> Edit(Inventario pInventario)
        {
            var inventario = await InventarioBL.ObtenerPorIdAsync(pInventario);
            ViewBag.Error = "";
            return View(inventario);
        }

        // POST: InventarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdInventario, Inventario pInventario)
        {
            try
            {
                int result = await InventarioBL.ModificarAsync(pInventario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pInventario);
            }
        }

        // GET: InventarioController/Delete/5
        public async Task<IActionResult> Delete(Inventario pInventario)
        {
            var inventario = await InventarioBL.ObtenerPorIdAsync(pInventario);
            return View(inventario);
        }

        // POST: InventarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int IdInventario, Inventario pInventario)
        {
            try
            {
                int result = await InventarioBL.EliminarAsync(pInventario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pInventario);
            }
        }
    }
}
