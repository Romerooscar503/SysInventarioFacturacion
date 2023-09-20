using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Agregar la siguiente libreria para la seguridad JWT
using SysInventarioFacturacion.WebAPI.Auth;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using SysInventarioFacturacion.EntidadesDeNegocio;
using SysInventarioFacturacion.LogicaDeNegocio;
using SysInventarioFacturacion.WebAPI.Auth;

namespace SysInventarioFacturacion.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PedidoController : ControllerBase
    {
        private PedidoBL pedidoBL = new PedidoBL();
        // Codigo para agregar la seguridad JWT
        private readonly IJwtAuthenticationService authService;
        public PedidoController(IJwtAuthenticationService pAuthService)
        {
            authService = pAuthService;
        }
        //************************************************
        // GET: api/<UsuarioController>
        [HttpGet]
        public async Task<IEnumerable<Pedido>> Get()
        {
            return await pedidoBL.ObtenerTodosAsync();
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{IdPedido}")]
        public async Task<Pedido> Get(int IdPedido)
        {
            Pedido pedido = new Pedido();
            pedido.IdPedido = IdPedido;
            return await pedidoBL.ObtenerPorIdAsync(pedido);
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Pedido pedido)
        {
            try
            {
                await pedidoBL.CrearAsync(pedido);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{IdPedido}")]
        public async Task<ActionResult> Put(int IdPedido, [FromBody] object pPedido)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strPedido = JsonSerializer.Serialize(pPedido);
            Pedido pedido = JsonSerializer.Deserialize<Pedido>(strPedido, option);
            if (pedido.IdPedido == IdPedido)
            {
                await pedidoBL.ModificarAsync(pedido);
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }


        // DELETE api/<UsuarioController>/5
        [HttpDelete("{IdPedido}")]
        public async Task<ActionResult> Delete(int IdPedido)
        {
            try
            {
                Pedido pedido = new Pedido();
                pedido.IdPedido = IdPedido;
                await pedidoBL.EliminarAsync(pedido);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Buscar")]
        public async Task<List<Pedido>> Buscar([FromBody] object pPedido)
        {

            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strPedido = JsonSerializer.Serialize(pPedido);
            Pedido pedido = JsonSerializer.Deserialize<Pedido>(strPedido, option);
            var pedidos = await pedidoBL.BuscarIncluirUsuarioAsync(pedido);
            pedidos.ForEach(s => s.Usuario.Pedido = null); // Evitar la redundacia de datos
            return pedidos;

        }



    }
}
