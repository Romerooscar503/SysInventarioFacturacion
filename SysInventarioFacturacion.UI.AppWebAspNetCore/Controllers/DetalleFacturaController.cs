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
using SysInventarioFacturacion.AccesoADatos;


namespace SysInventarioFacturacion.UI.AppWebAspNetCore.Controllers
{
    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class DetalleFacturaController : Controller
    {
        DetalleFacturaBL detalle_facturaBL = new DetalleFacturaBL();
        FacturaBL FacturaBL = new FacturaBL();
        ProductoBL ProductoBL = new ProductoBL();

       
        // GET: RolController
        public async Task<IActionResult> Index(DetalleFactura pDetalleFactura = null)
        {
            if (pDetalleFactura == null)
                pDetalleFactura = new DetalleFactura();
            if (pDetalleFactura.Top_Aux == 0)
                pDetalleFactura.Top_Aux = 10;
            else if (pDetalleFactura.Top_Aux == -1)
                pDetalleFactura.Top_Aux = 0;
            var taskBuscar = detalle_facturaBL.BuscarIncluirFacturasYProductoAsync(pDetalleFactura);
            var taskObtenerTodosFacturas = FacturaBL.ObtenerTodosAsync();
            var taskObtenerTodosProducto = ProductoBL.ObtenerTodosAsync();
            var DetalleFacturas = await taskBuscar;
            ViewBag.Top = pDetalleFactura.Top_Aux;
            ViewBag.Facturas = await taskObtenerTodosFacturas;
            ViewBag.Producto = await taskObtenerTodosProducto;
            return View(DetalleFacturas);
        }

        // GET: DetalleFacturaController/Details/5
        public async Task<IActionResult> Details(int IdDetalleFactura)
        {
            var detalleFactura = await detalle_facturaBL.ObtenerPorIdAsync(new DetalleFactura { IdDetalleFactura = IdDetalleFactura });
            detalleFactura.Factura = await FacturaBL.ObtenerPorIdAsync(new Factura { IdFactura = detalleFactura.IdFactura });
            detalleFactura.Producto = await ProductoBL.ObtenerPorIdProductoAsync(new Producto { IdProducto = detalleFactura.IdProducto });
            return View(detalleFactura);
        }

      

        // GET: DetalleFacturaController/Create


        public async Task<IActionResult> Create()
        {
            ViewBag.Facturas = await FacturaBL.ObtenerTodosAsync();
            ViewBag.Producto = await ProductoBL.ObtenerTodosAsync();
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
                ViewBag.Facturas = await FacturaBL.ObtenerTodosAsync();
                ViewBag.Producto = await ProductoBL.ObtenerTodosAsync();
                return View(pDetalleFactura);
            }
        }

        // GET: DeatlleFacturaController/Edit/5
        public async Task<IActionResult> Edit(DetalleFactura pDetalleFactura)
        {
            var taskObtenerPorId = detalle_facturaBL.ObtenerPorIdAsync(pDetalleFactura);
            var taskObtenerTodosFacturas = FacturaBL.ObtenerTodosAsync();
            var taskObtenerTodosProducto = ProductoBL.ObtenerTodosAsync();
            var detalleFactura = await taskObtenerPorId;
            ViewBag.Facturas = await taskObtenerTodosFacturas;
            ViewBag.Producto = await taskObtenerTodosProducto;
            ViewBag.Error = "";
            return View(detalleFactura);
        }

        // POST: DetalleFacturaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdDetalleFactura, DetalleFactura pDetalleFactura)
        {
            try
            {
                int result = await detalle_facturaBL.ModificarAsync(pDetalleFactura);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Facturas = await FacturaBL.ObtenerTodosAsync();
                ViewBag.Producto = await ProductoBL.ObtenerTodosAsync();
                return View(pDetalleFactura);
            }
        }

        // GET: DetalleFacturaController/Delete/5
        public async Task<IActionResult> Delete(DetalleFactura pDetalleFactura)
        {
            var DetalleFactura = await detalle_facturaBL.ObtenerPorIdAsync(pDetalleFactura);
            DetalleFactura.Factura = await FacturaBL.ObtenerPorIdAsync(new Factura { IdFactura = DetalleFactura.IdFactura });
            DetalleFactura.Producto = await ProductoBL.ObtenerPorIdProductoAsync(new Producto { IdProducto = DetalleFactura.IdProducto });
            ViewBag.Error = "";

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
                var detalleFactura = await detalle_facturaBL.ObtenerPorIdAsync(pDetalleFactura);
                if (detalleFactura == null)
                    detalleFactura = new DetalleFactura();
                if (detalleFactura.IdDetalleFactura > 0)
                    detalleFactura.Factura = await FacturaBL.ObtenerPorIdAsync(new Factura { IdFactura = detalleFactura.IdFactura });
                if (detalleFactura.IdDetalleFactura > 0)
                    detalleFactura.Producto = await ProductoBL.ObtenerPorIdProductoAsync(new Producto { IdProducto = detalleFactura.IdProducto });
                return View(detalleFactura);
            }
        }
        List<DetalleFactura>? DetalleFacturas;
        public async Task<IActionResult> Venta(int? campo, DetalleFactura pDetalleFactura = null)
        {
            if (pDetalleFactura == null)
                pDetalleFactura = new DetalleFactura();
            if (pDetalleFactura.Top_Aux == 0)
                pDetalleFactura.Top_Aux = 10;
            else if (pDetalleFactura.Top_Aux == -1)
                pDetalleFactura.Top_Aux = 0;
            var taskBuscar = detalle_facturaBL.BuscarIncluirFacturasYProductoAsync(pDetalleFactura);
            var taskObtenerTodosFacturas = FacturaBL.ObtenerTodosAsync();
            var taskObtenerTodosProducto = ProductoBL.ObtenerTodosAsync();
            DetalleFacturas = await taskBuscar;
            ViewBag.Top = pDetalleFactura.Top_Aux;
            ViewBag.Facturas = await taskObtenerTodosFacturas;
          
            if (campo>0)
            {
                Producto producto = new Producto();
                producto.Codigo = campo;
                List<Producto> ProductoBuscado = await ProductoBL.BuscarAsync(producto);
                ViewBag.Producto = ProductoBuscado;
            }
            else
            {
                ViewBag.Producto = await taskObtenerTodosProducto;
            }
            return View(DetalleFacturas);
        }

        

            [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetalleVenta(DetalleFactura pDetalleFactura)
        {
            try
            {
                int result = await detalle_facturaBL.CrearAsync(pDetalleFactura);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Facturas = await FacturaBL.ObtenerTodosAsync();
                ViewBag.Producto = await ProductoBL.ObtenerTodosAsync();
                return View(pDetalleFactura);
            }
        }
        // GET: DetalleFacturaController/Create

        public async Task<IActionResult> DetalleVenta()
        {
            ViewBag.Facturas = await FacturaBL.ObtenerTodosAsync();
            ViewBag.Producto = await ProductoBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View();
        }

    }

}
