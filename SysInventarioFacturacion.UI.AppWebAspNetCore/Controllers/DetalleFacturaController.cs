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
using SysInventarioFacturacion.UI.AppWebAspNetCore.Models;


namespace SysInventarioFacturacion.UI.AppWebAspNetCore.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Authorize(Roles = "Cajero,SuperAdmin,Admin")]
    public class DetalleFacturaController : Controller
    {
        DetalleFacturaBL detalle_facturaBL = new DetalleFacturaBL();
        FacturaBL FacturaBL = new FacturaBL();
        ProductoBL ProductoBL = new ProductoBL();
        public static int idFac;

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

            if (campo > 0)
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



        //[HttpPost("ProcesarFactura")]
        //public JsonResult ProcesarFactura([Bind("NumeroFactura, Descripcion, Direccion, Telefono, Correo, total, descuento, impuesto, totalpagado, FechaFacturacion, detalleFacturas")] int NumeroFactura, string Descripcion, string Direccion, string Correo, string Telefono, decimal total, decimal descuento, decimal impuesto, decimal totalpagado, List<DetalleFactura> detalleFacturas)
        //{
        //    Random random = new Random();


        //    Factura objFactura = new Factura();
        //    objFactura.IdUsuario = global.idu;
        //    objFactura.NumeroFactura = random.Next(100000, 999999);
        //    objFactura.Descripcion = Descripcion;
        //    objFactura.Direccion = Direccion;
        //    objFactura.Correo = Correo;
        //    objFactura.Total = total;
        //    objFactura.Descuento = descuento;
        //    objFactura.Impuesto = impuesto;
        //    objFactura.TotalPagado = totalpagado;

        //    objFactura.Telefono = Telefono;



        //    objFactura.FechaFacturacion = DateTime.Now;

        //    if (Direccion == null)
        //    {
        //        objFactura.Direccion = "N/A";
        //    }

        //    if (Telefono == null)
        //    {
        //        objFactura.Telefono = "N/A";
        //    }
        //    if (Correo == null)
        //    {
        //        objFactura.Correo = "N/A";
        //    }
        //    if (Descripcion == null)
        //    {
        //        objFactura.Descripcion = "N/A";
        //    }
        //    if (descuento == null)
        //    {
        //        objFactura.Descuento = 111;
        //    }
        //    if (NumeroFactura == null)
        //    {
        //        objFactura.NumeroFactura = 777;
        //    }

        //    FacturaBL.CrearAsync(objFactura);

        //    foreach (var detalle in detalleFacturas)
        //    {
        //        // Asignar el ID de la factura a los detalles de factura
        //        detalle.IdFactura = objFactura.IdFactura;

        //        // Llamar al método que crea el detalle de factura en la base de datos
        //        detalle_facturaBL.CrearAsync(detalle);
        //    }


        //    return Json(objFactura);
        //}
        [HttpPost("ProcesarFactura")]
        public async Task<IActionResult> ProcesarFactura(string Descripcion, string Direccion, string Correo, string Telefono, decimal total, decimal descuento, decimal impuesto, decimal totalpagado, int cantidad, int codigo, byte FormadaDePago, DateTime FechaEmision, decimal ValorTotal, List<DetalleFactura> detalleFacturas)
        {
            Random random = new Random();

            Factura objFactura = new();
            objFactura.IdUsuario = global.idu;
            objFactura.NumeroFactura = random.Next(100000, 999999);
            objFactura.Descripcion = Descripcion ?? "N/A";
            objFactura.Direccion = Direccion ?? "N/A";
            objFactura.Correo = Correo ?? "N/A";
            objFactura.Total = total;
            objFactura.Descuento = descuento;
            objFactura.Impuesto = impuesto;
            objFactura.TotalPagado = totalpagado;
            objFactura.Telefono = Telefono ?? "N/A";
            objFactura.FechaFacturacion = DateTime.Now;

            //FacturaBL.CrearAsync(objFactura);
            await FacturaBL.CrearAsync(objFactura);


            DetalleFactura DetalleFacturas = new();
            DetalleFacturas.Codigo = random.Next(100000, 999999);
            DetalleFacturas.FechaEmision = DateTime.Now;
            DetalleFacturas.FormaDePago = 1;
            
            foreach (var detalle in detalleFacturas)
            {
                Producto objProducto = new Producto();
                objProducto.IdProducto = detalle.IdProducto;
                objProducto = await ProductoBL.ObtenerPorIdProductoAsync(objProducto); 

               
                objProducto.Cantidad = objProducto.Cantidad - detalle.Cantidad;
                await ProductoBL.ModificarAsync(objProducto);


                idFac = objFactura.IdFactura;
                detalle.IdFactura = objFactura.IdFactura;
                await detalle_facturaBL.CrearAsync(detalle);
            }

            var venta = new Venta
            {
                ObjFactura = objFactura,
                DetalleFacturas = detalleFacturas
            };

            return RedirectToAction("ObtenerFactura");
        }

        [HttpGet("ObtenerFactura")]

        public async Task<IActionResult> ObtenerFactura()
        {
           
            DetalleFactura objdetalle = new DetalleFactura();
            objdetalle.IdFactura = idFac;
            List<DetalleFactura> ListaDetalle = await detalle_facturaBL.BuscarIncluirFacturasYProductoAsync(objdetalle);
            ViewBag.Facturas = ListaDetalle.FirstOrDefault().Factura;
            ViewBag.ListaDetalle = ListaDetalle;
            return View();
        }

        public async Task<IActionResult> Reportes(DetalleFactura pDetalleFactura)
        {
            List<Factura> taskObtenerTodosFacturas = await FacturaBL.ObtenerTodosAsync();
            var taskObtenerTodosProducto = ProductoBL.ObtenerTodosAsync();

            ViewBag.Facturas = taskObtenerTodosFacturas;
           

            return View();
        }

        //public async Task<IActionResult> Reportes(DetalleFactura pDetalleFactura)
        //{
        //    var taskObtenerTodosFacturas = FacturaBL.ObtenerTodosAsync();
        //    var taskObtenerTodosProducto = ProductoBL.ObtenerTodosAsync();
        //    var taskObtenerTodosDeallesFacturas = detalle_facturaBL.ObtenerTodosAsync();
        //    var facturas = await taskObtenerTodosFacturas;

        //    // Cargar detalles de facturas para cada factura
        //    foreach (var factura in facturas)
        //    {
        //        DetalleFactura objdetalle = new DetalleFactura();
        //        objdetalle.IdFactura = idFac;
        //        List<DetalleFactura> ListaDetalles = await detalle_facturaBL.BuscarIncluirFacturasYProductoAsync(objdetalle);


        //        ViewBag.ListaDetalles = ListaDetalles;

        //    }

        //    ViewBag.Facturas = await taskObtenerTodosFacturas;
        //    ViewBag.Producto = await taskObtenerTodosProducto;
        //    ViewBag.DetalleFactura = await taskObtenerTodosDeallesFacturas;

        //    return View();
        //}


    }


}