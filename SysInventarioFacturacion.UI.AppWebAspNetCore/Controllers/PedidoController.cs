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
	//[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
	public class PedidoController : Controller
	{
		PedidoBL PedidoBL = new PedidoBL();
		UsuarioBL UsuarioBL = new UsuarioBL();
		// GET: FacturaController
		public async Task<IActionResult> Index(Pedido pPedido = null)
		{
			if (pPedido == null)
				pPedido = new Pedido();
			if (pPedido.Top_Aux == 0)
				pPedido.Top_Aux = 10;
			else if (pPedido.Top_Aux == -1)
				pPedido.Top_Aux = 0;
			var taskBuscar = PedidoBL.BuscarIncluirUsuarioAsync(pPedido);
			var taskObtenerTodosUsuarios = UsuarioBL.ObtenerTodosAsync();
			var Pedidos = await taskBuscar;
			ViewBag.Top = pPedido.Top_Aux;
			ViewBag.Usuarios = await taskObtenerTodosUsuarios;
			return View(Pedidos);
		}

		// GET: FacturaController/Details/5
		public async Task<IActionResult> Details(int IdPedido)
		{
			var pedido = await PedidoBL.ObtenerPorIdAsync(new Pedido { IdPedido = IdPedido });
			pedido.Usuario = await UsuarioBL.ObtenerPorIdAsync(new Usuario { Id = pedido.IdUsuario });
			return View(pedido);
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
		public async Task<IActionResult> Create(Pedido pPedido)
		{
			try
			{
				int result = await PedidoBL.CrearAsync(pPedido);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
                ViewBag.Usuarios = await UsuarioBL.ObtenerTodosAsync();
                ViewBag.Error = ex.Message;
				return View(pPedido);
			}
		}

		// GET: FacturaController/Edit/5
		public async Task<IActionResult> Edit(Pedido pPedido)
		{
			var taskObtenerPorId = PedidoBL.ObtenerPorIdAsync(pPedido);
			var taskObtenerTodosUsuarios = UsuarioBL.ObtenerTodosAsync();
			var pedido = await taskObtenerPorId;
			ViewBag.Usuarios = await taskObtenerTodosUsuarios;
			ViewBag.Error = "";
			return View(pedido);
		}

		// POST: FacturaController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int IdPedido, Pedido pPedido)
		{
			try
			{
				int result = await PedidoBL.ModificarAsync(pPedido);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				ViewBag.Error = ex.Message;
				ViewBag.Usuarios = await UsuarioBL.ObtenerTodosAsync();
				return View(pPedido);
			}
		}

		// GET: FacturaController/Delete/5
		public async Task<IActionResult> Delete(Pedido pPedido)
		{
			var Pedido = await PedidoBL.ObtenerPorIdAsync(pPedido);
			Pedido.Usuario = await UsuarioBL.ObtenerPorIdAsync(new Usuario { Id = Pedido.IdUsuario });
			ViewBag.Error = "";

			return View(Pedido);
		}

		// POST: FacturaController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int IdPedido, Pedido pPedido)
		{
			try
			{
				int result = await PedidoBL.EliminarAsync(pPedido);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				ViewBag.Error = ex.Message;
				var pedido = await PedidoBL.ObtenerPorIdAsync(pPedido);
				if (pedido == null)
					pedido = new Pedido();
				if (pedido.IdPedido > 0)
					pedido.Usuario = await UsuarioBL.ObtenerPorIdAsync(new Usuario { Id = pedido.IdUsuario });
				return View(pedido);
			}
		}
	}
}
