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
            ViewBag.Pedido = await taskObtenerTodosPedido;
            ViewBag.Proveedor = await taskObtenerTodosProveedor;
            ViewBag.Producto = await taskObtenerTodosProducto;
            return View(DetallePedido);
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

        public async Task<IActionResult> Create()
        {
            ViewBag.Pedido = await PedidoBL.ObtenerTodosAsync();
            ViewBag.Producto = await ProductoBL.ObtenerTodosAsync();
            ViewBag.Proveedor = await ProveedorBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View();
        }

        // POST: DetallePedidoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DetallePedido pDetallePedido)
        {
            try
            {
                int result = await DetallePedidoBL.CrearAsync(pDetallePedido);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Pedido = await PedidoBL.ObtenerTodosAsync();
                ViewBag.Producto = await ProductoBL.ObtenerTodosAsync();
                ViewBag.Proveedor = await ProveedorBL.ObtenerTodosAsync();
                return View(pDetallePedido);
            }
        }

        // GET: DetallePedidoController/Edit/5
        public async Task<IActionResult> Edit(DetallePedido pDetallePedido)
        {
            var taskObtenerPorId = DetallePedidoBL.ObtenerPorIdAsync(pDetallePedido);
            var taskObtenerTodosPedido = PedidoBL.ObtenerTodosAsync();
            var taskObtenerTodosProveedor = ProveedorBL.ObtenerTodosAsync();
            var taskObtenerTodosProducto = ProductoBL.ObtenerTodosAsync();
            var DetallePedido = await taskObtenerPorId;
            ViewBag.Pedido = await taskObtenerTodosPedido;
            ViewBag.Producto = await taskObtenerTodosProducto;
            ViewBag.Proveedor = await taskObtenerTodosProveedor;
            ViewBag.Error = "";
            return View(DetallePedido);
        }

        // POST: DetallePedidoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdDetallePedido, DetallePedido pDetallePedido)
        {
            try
            {
                int result = await DetallePedidoBL.ModificarAsync(pDetallePedido);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Pedido = await PedidoBL.ObtenerTodosAsync();
                ViewBag.Producto = await ProductoBL.ObtenerTodosAsync();
                ViewBag.Proveedor = await ProveedorBL.ObtenerTodosAsync();
                return View(pDetallePedido);
            }
        }

        // GET: DetallePedidoController/Delete/5
        public async Task<IActionResult> Delete(DetallePedido pDetallePedido)
        {
            var DetallePedido = await DetallePedidoBL.ObtenerPorIdAsync(pDetallePedido);
            DetallePedido.Pedido = await PedidoBL.ObtenerPorIdAsync(new Pedido { IdPedido = DetallePedido.IdPedido });
            DetallePedido.Producto = await ProductoBL.ObtenerPorIdProductoAsync(new Producto { IdProducto = DetallePedido.IdProducto });
            DetallePedido.Proveedor = await ProveedorBL.ObtenerPorIdAsync(new Proveedor { IdProveedor = DetallePedido.IdProveedor });
            ViewBag.Error = "";

            return View(DetallePedido);
        }

        // POST: DetallePedidoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DetallePedido pDetallePedido)
        {
            try
            {
                int result = await DetallePedidoBL.EliminarAsync(pDetallePedido);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var DetallePedido = await DetallePedidoBL.ObtenerPorIdAsync(pDetallePedido);
                if (DetallePedido == null)
                    DetallePedido = new DetallePedido();
                if (DetallePedido.IdDetallePedido > 0)
                    DetallePedido.Pedido = await PedidoBL.ObtenerPorIdAsync(new Pedido { IdPedido = DetallePedido.IdPedido });
                if (DetallePedido.IdDetallePedido > 0)
                    DetallePedido.Producto = await ProductoBL.ObtenerPorIdProductoAsync(new Producto { IdProducto = DetallePedido.IdProducto });
                if (DetallePedido.IdDetallePedido > 0)
                    DetallePedido.Proveedor = await ProveedorBL.ObtenerPorIdAsync(new Proveedor { IdProveedor = DetallePedido.IdProveedor });
                return View(DetallePedido);
            }
        }
    }
}
