using Microsoft.AspNetCore.Mvc;
using SysInventarioFacturacion.EntidadesDeNegocio;
using SysInventarioFacturacion.LogicaDeNegocio;
using System.Text.Json;

// Agregar la siguiente librerias
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using SysInventarioFacturacion.AccesoADatos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SysInventarioFacturacion.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] // agregar el siguiente metadato para autorizar JWT la Web API
    public class CategoriaController : ControllerBase
    {
        private CategoriaBL categoriaBL = new CategoriaBL();

        // GET: api/<CategoriaController>
        [HttpGet]
        public async Task<IEnumerable<Categoria>> Get()
        {
            return await categoriaBL.ObtenerTodosAsync();
        }

        // GET api/<CategoriaController>/5
        [HttpGet("{IdCategoria}")]
        public async Task<Categoria> Get(int IdCategoria)
        {
            Categoria categoria = new Categoria();
            categoria.IdCategoria = IdCategoria;
            return await categoriaBL.ObtenerPorIdCategoriaAsync(categoria);
        }

        // POST api/<CategoriaController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Categoria categoria)
        {
            try
            {
                await categoriaBL.CrearAsync(categoria);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        // PUT api/<CategoriaController>/5
        [HttpPut("{IdCategoria}")]
        public async Task<ActionResult> Put(int IdCategoria, [FromBody] Categoria categoria)
        {

            if (categoria.IdCategoria == IdCategoria)
            {
                await categoriaBL.ModificarAsync(categoria);
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        // DELETE api/<CategoriaController>/5
        [HttpDelete("{IdCategoria}")]
        public async Task<ActionResult> Delete(int IdCategoria)
        {
            try
            {
                Categoria categoria = new Categoria();
                categoria.IdCategoria = IdCategoria;
                await categoriaBL.EliminarAsync(categoria);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Buscar")]
        public async Task<List<Categoria>> Buscar([FromBody] object pCategoria)
        {

            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strCategoria = JsonSerializer.Serialize(pCategoria);
            Categoria categoria = JsonSerializer.Deserialize<Categoria>(strCategoria, option);
            return await categoriaBL.BuscarAsync(categoria);

        }
    }
}
