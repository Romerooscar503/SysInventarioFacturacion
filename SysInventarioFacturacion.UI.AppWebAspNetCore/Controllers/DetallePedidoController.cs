using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysInventarioFacturacion.EntidadesDeNegocio;
using SysInventarioFacturacion.LogicaDeNegocio;

namespace SysInventarioFacturacion.UI.AppWebAspNetCore.Controllers
{
    public class DetallePedidoController : Controller
    {
        DetallePedidoBL DetallePedidoBL = new DetallePedidoBL();
        ProductoBL ProductoBL = new ProductoBL();
        ProveedorBL ProveedorBL = new ProveedorBL();
        PedidoBL PedidoBL = new PedidoBL();
        // GET: DetallePedidoController
        public async Task<IActionResult> Index(DetallePedido pDetallePedido = null)
        {
            if (pDetallePedido == null)
                pDetallePedido = new DetallePedido();
            if (pDetallePedido.Top_Aux == 0)
                pDetallePedido.Top_Aux = 10;
            else if (pDetallePedido.Top_Aux == -1)
                pDetallePedido.Top_Aux = 0;
            var taskBuscar = DetallePedidoBL.BuscarIncluirPedidoProductoProveedorAsync(pDetallePedido);
            var taskObtenerTodosPedido = PedidoBL.ObtenerTodosAsync();
            var taskObtenerTodosProducto = ProductoBL.ObtenerTodosAsync();
            var taskObtenerTodosProveedor = ProveedorBL.ObtenerTodosAsync();
            var DetallePedido = await taskBuscar;
            ViewBag.Top = pDetallePedido.Top_Aux;
            ViewBag.Facturas = await taskObtenerTodosPedido;
            ViewBag.Producto = await taskObtenerTodosProducto;
            return View();
        }

        // GET: DetallePedidoController/Details/5
        public async Task<IActionResult> Details(int IdDetallePedido)
        {
            var DetallePedido = await DetallePedidoBL.ObtenerPorIdAsync(new DetallePedido { IdDetallePedido = IdDetallePedido });
            DetallePedido.Pedido = await PedidoBL.ObtenerPorIdAsync(new Pedido { IdPedido = DetallePedido.IdPedido });
            DetallePedido.Proveedor = await ProveedorBL.ObtenerPorIdAsync(new Proveedor { IdProveedor = DetallePedido.IdProveedor });
            DetallePedido.Producto = await ProductoBL.ObtenerPorIdProductoAsync(new Producto { IdProducto = DetallePedido.IdProducto });
            
            return View(DetallePedido);
        }

        // GET: DetallePedidoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DetallePedidoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DetallePedidoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DetallePedidoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DetallePedidoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DetallePedidoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
