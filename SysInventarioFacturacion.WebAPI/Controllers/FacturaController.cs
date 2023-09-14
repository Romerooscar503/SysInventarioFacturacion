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
// ***************************************************

namespace SeguridadWeb.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] // agregar el siguiente metadato para autorizar JWT la Web API
    public class FacturaController : ControllerBase
    {
        private FacturaBL facturaBL = new FacturaBL();
        // Codigo para agregar la seguridad JWT
        private readonly IJwtAuthenticationService authService;
        public FacturaController(IJwtAuthenticationService pAuthService)
        {
            authService = pAuthService;
        }
        //************************************************
        // GET: api/<FacturaController>
        [HttpGet]
        public async Task<IEnumerable<Factura>> Get()
        {
            return await facturaBL.ObtenerTodosAsync();
        }

        // GET api/<FacturaController>/5
        [HttpGet("{id}")]
        public async Task<Factura> Get(int id)
        {
            Factura factura = new Factura();
            factura.IdFactura = id;
            return await facturaBL.ObtenerPorIdAsync(factura);
        }

        // POST api/<FacturaController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Factura factura)
        {
            try
            {
                await facturaBL.CrearAsync(factura);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        // PUT api/<FacturaController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] object pFactura)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strFactura = JsonSerializer.Serialize(pFactura);
            Factura factura = JsonSerializer.Deserialize<Factura>(strFactura, option);
            if (factura.IdFactura == id)
            {
                await facturaBL.ModificarAsync(factura);
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }


        // DELETE api/<FacturaController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Factura factura = new Factura();
                factura.IdFactura = id;
                await facturaBL.EliminarAsync(factura);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Buscar")]
        public async Task<List<Factura>> Buscar([FromBody] object pFactura)
        {

            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strFactura = JsonSerializer.Serialize(pFactura);
            Factura factura = JsonSerializer.Deserialize<Factura>(strFactura, option);
            var facturas = await facturaBL.BuscarIncluirUsuarioAsync(factura);
            facturas.ForEach(s => s.Usuario.Factura = null); // Evitar la redundacia de datos
            return facturas;

        }
    }
}
