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

namespace SysInventarioFacturacion.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // agregar el siguiente metadato para autorizar JWT la Web API
    public class ProductoController : ControllerBase
    {
        private ProductoBL productoBL = new ProductoBL();
        // Codigo para agregar la seguridad JWT
        private readonly IJwtAuthenticationService authService;
        public ProductoController(IJwtAuthenticationService pAuthService)
        {
            authService = pAuthService;
        }
        //************************************************
        // GET: api/<UsuarioController>
        [HttpGet]
        public async Task<IEnumerable<Producto>> Get()
        {
            return await productoBL.ObtenerTodosAsync();
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{IdProducto}")]
        public async Task<Producto> Get(int IdProducto)
        {
            Producto producto = new Producto();
            producto.IdProducto = IdProducto;
            return await productoBL.ObtenerPorIdProductoAsync(producto);
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Producto producto)
        {
            try
            {
                await productoBL.CrearAsync(producto);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{IdProducto}")]
        public async Task<ActionResult> Put(int IdProducto, [FromBody] object pProducto)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strProducto = JsonSerializer.Serialize(pProducto);
            Producto producto = JsonSerializer.Deserialize<Producto>(strProducto, option);
            if (producto.IdProducto == IdProducto)
            {
                await productoBL.ModificarAsync(producto);
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }


        // DELETE api/<UsuarioController>/5
        [HttpDelete("{IdProducto}")]
        public async Task<ActionResult> Delete(int IdProducto)
        {
            try
            {
                Producto producto = new Producto();
                producto.IdProducto = IdProducto;
                await productoBL.EliminarAsync(producto);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Buscar")]
        public async Task<List<Producto>> Buscar([FromBody] object pProducto)
        {

            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strProducto = JsonSerializer.Serialize(pProducto);
            Producto producto = JsonSerializer.Deserialize<Producto>(strProducto, option);
            var productos = await productoBL.BuscarIncluirCategoriayProveedorAsync(producto);
            productos.ForEach(s => s.Categoria.Producto = null); // Evitar la redundacia de datos
            productos.ForEach(s => s.Proveedor.Producto = null); // Evitar la redundacia de datos

            return productos;

        }
    }
}
