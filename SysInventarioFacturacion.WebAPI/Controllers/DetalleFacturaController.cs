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
using SysInventarioFacturacion.AccesoADatos;
// ***************************************************

namespace SeguridadWeb.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // agregar el siguiente metadato para autorizar JWT la Web API
    public class DetalleFacturaController : ControllerBase
    {
        private DetalleFacturaBL detalle_facturaBL = new DetalleFacturaBL();
        // Codigo para agregar la seguridad JWT
        private readonly IJwtAuthenticationService authService;
        public DetalleFacturaController(IJwtAuthenticationService pAuthService)
        {
            authService = pAuthService;
        }
        //************************************************
        // GET: api/<DetalleFacturaController>
        [HttpGet]
        public async Task<IEnumerable<DetalleFactura>> Get()
        {
            return await detalle_facturaBL.ObtenerTodosAsync();
        }

        // GET api/<DetalleFacturaController>/5
        [HttpGet("{IdDetalleFactura}")]
        public async Task<DetalleFactura> Get(int IdDetalleFactura)
        {
            DetalleFactura detallefactura = new DetalleFactura();
            detallefactura.IdDetalleFactura = IdDetalleFactura;
            return await detalle_facturaBL.ObtenerPorIdAsync(detallefactura);
        }

        // POST api/<DetalleFacturaController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DetalleFactura detallefactura)
        {
            try
            {
                await detalle_facturaBL.CrearAsync(detallefactura);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        // PUT api/<DetalleFacturaController>/5
        [HttpPut("{IdDetalleFactura}")]
        public async Task<ActionResult> Put(int IdDetalleFactura, [FromBody] object pDetalleFactura)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strDetalleFactura = JsonSerializer.Serialize(pDetalleFactura);
            DetalleFactura detallefactura = JsonSerializer.Deserialize<DetalleFactura>(strDetalleFactura, option);
            if (detallefactura.IdDetalleFactura == IdDetalleFactura)
            {
                await detalle_facturaBL.ModificarAsync(detallefactura);
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        // DELETE api/<DetalleFacturaController>/5
        [HttpDelete("{IdDetalleFactura}")]
        public async Task<ActionResult> Delete(int IdDetalleFactura)
        {
            try
            {
                DetalleFactura detallefactura = new DetalleFactura();
                detallefactura.IdDetalleFactura = IdDetalleFactura;
                await detalle_facturaBL.EliminarAsync(detallefactura);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Buscar")]
        public async Task<List<DetalleFactura>> Buscar([FromBody] object pDetalleFactura)
        {

            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strDetalleFactura = JsonSerializer.Serialize(pDetalleFactura);
            DetalleFactura detallefactura = JsonSerializer.Deserialize<DetalleFactura>(strDetalleFactura, option);
            var detallefacturas = await detalle_facturaBL.BuscarIncluirFacturasYProductoAsync(detallefactura);
            detallefacturas.ForEach(s => s.Factura.DetalleFactura = null); // Evitar la redundacia de datos
            detallefacturas.ForEach(s => s.Producto.DetalleFactura = null);
            return detallefacturas;

        }
    }
}