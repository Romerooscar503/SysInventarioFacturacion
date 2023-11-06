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
using Microsoft.EntityFrameworkCore;
using SysInventarioFacturacion.AccesoADatos;

namespace SysInventarioFacturacion.UI.AppWebAspNetCore.Controllers
{
	[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ProductoController : Controller
	{
		ProductoBL ProductoBL = new ProductoBL();
		CategoriaBL CategoriaBL = new CategoriaBL();
		ProveedorBL ProveedorBL = new ProveedorBL();

		// GET: ProductoController
		public async Task<IActionResult> Index(Producto? pProducto = null)
		{
			if (pProducto == null)
				pProducto = new Producto();
			if (pProducto.Top_Aux == 0)
				pProducto.Top_Aux = 10;
			else if (pProducto.Top_Aux == -1)
				pProducto.Top_Aux = 0;
			var taskBuscar = ProductoBL.BuscarIncluirCategoriayProveedorAsync(pProducto);
			var taskObtenerTodosCategoria = CategoriaBL.ObtenerTodosAsync();
			var taskObtenerTodosProveedor = ProveedorBL.ObtenerTodosAsync();
			var Productos = await taskBuscar;
			ViewBag.Categoria = await taskObtenerTodosCategoria;
			ViewBag.Proveedor = await taskObtenerTodosProveedor;
			ViewBag.Top = pProducto.Top_Aux;
			return View(Productos);
		}

		// GET: ProductoController/Details/5
		public async Task<IActionResult> Details(int IdProducto)
		{
			var producto = await ProductoBL.ObtenerPorIdProductoAsync(new Producto { IdProducto = IdProducto });
			producto.Categoria = await CategoriaBL.ObtenerPorIdCategoriaAsync(new Categoria { IdCategoria = producto.IdCategoria });
			producto.Proveedor = await ProveedorBL.ObtenerPorIdAsync(new Proveedor { IdProveedor = producto.IdProveedor });
			return View(producto);
		}

		// GET: ProductoController/Create

		public async Task<IActionResult> Create()
		{
			ViewBag.Categoria = await CategoriaBL.ObtenerTodosAsync();
			ViewBag.Proveedor = await ProveedorBL.ObtenerTodosAsync();
			ViewBag.Error = "";
			return View();
		}

		// POST: ProductoController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Producto pProducto)
		{
			try
			{
				int result = await ProductoBL.CrearAsync(pProducto);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				ViewBag.Categoria = await CategoriaBL.ObtenerTodosAsync();
				ViewBag.Proveedor = await ProveedorBL.ObtenerTodosAsync();
				ViewBag.Error = ex.Message;
				return View(pProducto);
			}
		}

		// GET: ProductoController/Edit/5
		public async Task<IActionResult> Edit(Producto pProducto)
		{
			var taskObtenerPorIdProducto = ProductoBL.ObtenerPorIdProductoAsync(pProducto);
			var taskObtenerTodosCategoria = CategoriaBL.ObtenerTodosAsync();
			var taskObtenerTodosProveedor = ProveedorBL.ObtenerTodosAsync();
			var producto = await taskObtenerPorIdProducto;
			ViewBag.Categoria = await taskObtenerTodosCategoria;
			ViewBag.Proveedor = await taskObtenerTodosProveedor;
			ViewBag.Error = "";
			return View(producto);
		}

		// POST: ProductoController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int IdProducto, Producto pProducto)
		{
			try
			{
				int result = await ProductoBL.ModificarAsync(pProducto);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				ViewBag.Error = ex.Message;
				ViewBag.Categoria = await CategoriaBL.ObtenerTodosAsync();
				ViewBag.Proveedor = await ProveedorBL.ObtenerTodosAsync();
				return View(pProducto);
			}
		}

		// GET: ProductoController/Delete/5
		public async Task<IActionResult> Delete(Producto pProducto)
		{
			var Producto = await ProductoBL.ObtenerPorIdProductoAsync(pProducto);
            Producto.Categoria = await CategoriaBL.ObtenerPorIdCategoriaAsync(new Categoria { IdCategoria = Producto.IdCategoria });
            Producto.Proveedor = await ProveedorBL.ObtenerPorIdAsync(new Proveedor { IdProveedor = Producto.IdProveedor });
			ViewBag.Error = "";
			return View(Producto);
		}

		// POST: ProductoController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int IdProducto, Producto pProducto)
		{
			try
			{
				int result = await ProductoBL.EliminarAsync(pProducto);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				ViewBag.Error = ex.Message;
				var producto = await ProductoBL.ObtenerPorIdProductoAsync(pProducto);
				if (producto == null)
                    producto = new Producto();
				if (producto.IdProducto > 0)
                    producto.Categoria = await CategoriaBL.ObtenerPorIdCategoriaAsync(new Categoria { IdCategoria = producto.IdCategoria });
				if (producto.IdProducto > 0)
                    producto.Proveedor = await ProveedorBL.ObtenerPorIdAsync(new Proveedor { IdProveedor = producto.IdProveedor });
				return View(producto);
			}
		}

        public async Task<IActionResult> Inventario(Producto? pProducto = null)
        {
            if (pProducto == null)
                pProducto = new Producto();
            if (pProducto.Top_Aux == 0)
                pProducto.Top_Aux = 10;
            else if (pProducto.Top_Aux == -1)
                pProducto.Top_Aux = 0;
            var taskBuscar = ProductoBL.BuscarIncluirCategoriayProveedorAsync(pProducto);
            var taskObtenerTodosCategoria = CategoriaBL.ObtenerTodosAsync();
            var taskObtenerTodosProveedor = ProveedorBL.ObtenerTodosAsync();
            var Productos = await taskBuscar;
            ViewBag.Categoria = await taskObtenerTodosCategoria;
            ViewBag.Proveedor = await taskObtenerTodosProveedor;
            ViewBag.Top = pProducto.Top_Aux;
            return View(Productos);
        }


        // POST: ProductoController/Edit/5
        public async Task<IActionResult> EditExistencia(Producto pProducto)
        {
            var taskObtenerPorIdProducto = ProductoBL.ObtenerPorIdProductoAsync(pProducto);
            var taskObtenerTodosCategoria = CategoriaBL.ObtenerTodosAsync();
            var taskObtenerTodosProveedor = ProveedorBL.ObtenerTodosAsync();
            var producto = await taskObtenerPorIdProducto;
            ViewBag.Categoria = await taskObtenerTodosCategoria;
            ViewBag.Proveedor = await taskObtenerTodosProveedor;
            ViewBag.Error = "";
            return View(producto);
        }

        // POST: ProductoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditExistencia(int IdProducto, Producto pProducto)
        {
            try
            {
                int result = await ProductoBL.ModificarExistenciasAsync(pProducto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Categoria = await CategoriaBL.ObtenerTodosAsync();
                ViewBag.Proveedor = await ProveedorBL.ObtenerTodosAsync();
                return View(pProducto);
            }
        }
       
        
         
        


    }
}