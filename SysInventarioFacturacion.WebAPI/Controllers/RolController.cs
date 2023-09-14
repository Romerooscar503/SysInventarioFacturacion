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
    public class RolController : ControllerBase
    {
        private RolBL rolBL = new RolBL();

        // GET: api/<RolController>
        [HttpGet]
        public async Task<IEnumerable<Rol>> Get()
        {
            return await rolBL.ObtenerTodosAsync();
        }

        // GET api/<RolController>/5
        [HttpGet("{id}")]
        public async Task<Rol> Get(int id)
        {
            Rol rol = new Rol();
            rol.Id = id;
            return await rolBL.ObtenerPorIdAsync(rol);
        }

        // POST api/<RolController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Rol rol)
        {
            try
            {
                await rolBL.CrearAsync(rol);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        // PUT api/<RolController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Rol rol)
        {

            if (rol.Id == id)
            {
                await rolBL.ModificarAsync(rol);
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        // DELETE api/<RolController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Rol rol = new Rol();
                rol.Id = id;
                await rolBL.EliminarAsync(rol);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Buscar")]
        public async Task<List<Rol>> Buscar([FromBody] object pRol)
        {

            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strRol = JsonSerializer.Serialize(pRol);
            Rol rol = JsonSerializer.Deserialize<Rol>(strRol, option);
            return await rolBL.BuscarAsync(rol);

        }
    }
}
