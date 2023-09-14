using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Agregar la siguiente librerias
using SysInventarioFacturacion.EntidadesDeNegocio;
using SysInventarioFacturacion.LogicaDeNegocio;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using SysInventarioFacturacion.AccesoADatos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SysInventarioFacturacion.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] // agregar el siguiente metadato para autorizar JWT la Web API
    public class ProveedorController : ControllerBase
    {
        private ProveedorBL proveedorBL = new ProveedorBL();

        // GET: api/<ProveedorController>
        [HttpGet]
        public async Task<IEnumerable<Proveedor>> Get()
        {
            return await proveedorBL.ObtenerTodosAsync();
        }

        // GET api/<ProveedorController>/5
        [HttpGet("{id}")]
        public async Task<Proveedor> Get(int IdProveedor)
        {
            Proveedor proveedor = new Proveedor();
            proveedor.IdProveedor = IdProveedor;
            return await proveedorBL.ObtenerPorIdAsync(proveedor);
        }

        // POST api/<ProveedorController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Proveedor proveedor)
        {
            try
            {
                await proveedorBL.CrearAsync(proveedor);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        // PUT api/<RolController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int IdProveedor, [FromBody] Proveedor proveedor)
        {

            if (proveedor.IdProveedor == IdProveedor)
            {
                await proveedorBL.ModificarAsync(proveedor);
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        // DELETE api/<ProveedorController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int IdProveedor)
        {
            try
            {
                Proveedor proveedor = new Proveedor();
                proveedor.IdProveedor = IdProveedor;
                await proveedorBL.EliminarAsync(proveedor);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Buscar")]
        public async Task<List<Proveedor>> Buscar([FromBody] object pProveedor)
        {

            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strProveedor = JsonSerializer.Serialize(pProveedor);
            Proveedor proveedor = JsonSerializer.Deserialize<Proveedor>(strProveedor, option);
            return await proveedorBL.BuscarAsync(proveedor);

        }
    }
}
