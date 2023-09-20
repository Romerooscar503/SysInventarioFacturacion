using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysInventarioFacturacion.EntidadesDeNegocio;
using SysInventarioFacturacion.LogicaDeNegocio;
using SysInventarioFacturacion.WebAPI.Auth;
using System.Text.Json;

namespace SysInventarioFacturacion.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallePedidoController : ControllerBase
    {

        private DetallePedidoBL detallepedidoBL = new DetallePedidoBL();
        // Codigo para agregar la seguridad JWT
        private readonly IJwtAuthenticationService authService;
        public DetallePedidoController(IJwtAuthenticationService pAuthService)
        {
            authService = pAuthService;
        }
        //************************************************
        // GET: api/<UsuarioController>
        [HttpGet]
        public async Task<IEnumerable<DetallePedido>> Get()
        {
            return await detallepedidoBL.ObtenerTodosAsync();
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{IdDetallePedido}")]
        public async Task<DetallePedido> Get(int IdDetallePedido)
        {
            DetallePedido detallepedido = new DetallePedido();
            detallepedido.IdDetallePedido = IdDetallePedido;
            return await detallepedidoBL.ObtenerPorIdAsync(detallepedido);
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DetallePedido detallepedido)
        {
            try
            {
                await detallepedidoBL.CrearAsync(detallepedido);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{IdDetallePedido}")]
        public async Task<ActionResult> Put(int IdDetallePedido, [FromBody] object pDetallePedido)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strDetallePedido = JsonSerializer.Serialize(pDetallePedido);
            DetallePedido detallepedido = JsonSerializer.Deserialize<DetallePedido>(strDetallePedido, option);
            if (detallepedido.IdDetallePedido == IdDetallePedido)
            {
                await detallepedidoBL.ModificarAsync(detallepedido);
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }


        // DELETE api/<UsuarioController>/5
        [HttpDelete("{IdDetallePedido}")]
        public async Task<ActionResult> Delete(int IdDetallePedido)
        {
            try
            {
                DetallePedido detallepedido = new DetallePedido();
                detallepedido.IdDetallePedido = IdDetallePedido;
                await detallepedidoBL.EliminarAsync(detallepedido);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Buscar")]
        public async Task<List<DetallePedido>> Buscar([FromBody] object pDetallePedido)
        {

            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strDetallePedido = JsonSerializer.Serialize(pDetallePedido);
            DetallePedido detallepedido = JsonSerializer.Deserialize<DetallePedido>(strDetallePedido, option);
            var detallepedidos = await detallepedidoBL.BuscarIncluirPedidoProductoProveedorAsync(detallepedido);
            detallepedidos.ForEach(s => s.Pedido.DetallePedido = null);
            detallepedidos.ForEach(s => s.Producto.DetallePedido = null);
            detallepedidos.ForEach(s => s.Proveedor.DetallePedido = null);// Evitar la redundacia de datos
            return detallepedidos;

        }

    }
}
