using Microsoft.EntityFrameworkCore;
using SysInventarioFacturacion.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacion.AccesoADatos
{
	public class PedidoDAL
	{
		public static async Task<int> CrearAsync(Pedido pPedido)
		{
			int result = 0;
			using (var bdContexto = new BDContexto())
			{
				bdContexto.Add(pPedido);
				result = await bdContexto.SaveChangesAsync();
			}
			return result;
		}
		public static async Task<int> ModificarAsync(Pedido pPedido)
		{
			int result = 0;
			using (var bdContexto = new BDContexto())
			{
				var Pedido = await bdContexto.Pedido.FirstOrDefaultAsync(s => s.IdPedido == pPedido.IdPedido);
				Pedido.IdUsuario = pPedido.IdUsuario;
				Pedido.Telefono = pPedido.Telefono;
				bdContexto.Update(Pedido);
				result = await bdContexto.SaveChangesAsync();
			}
			return result;
		}
		public static async Task<int> EliminarAsync(Pedido pPedido)
		{
			int result = 0;
			using (var bdContexto = new BDContexto())
			{
				var Pedido = await bdContexto.Pedido.FirstOrDefaultAsync(s => s.IdPedido == pPedido.IdPedido);
				bdContexto.Pedido.Remove(Pedido);
				result = await bdContexto.SaveChangesAsync();
			}
			return result;
		}
		public static async Task<Pedido> ObtenerPorIdAsync(Pedido pPedido)
		{
			var Pedido = new Pedido();
			using (var bdContexto = new BDContexto())
			{
				Pedido = await bdContexto.Pedido.FirstOrDefaultAsync(s => s.IdPedido == pPedido.IdPedido);
			}
			return Pedido;
		}

		public static async Task<List<Pedido>> ObtenerTodosAsync()
		{
			var Pedidos = new List<Pedido>();
			using (var bdContexto = new BDContexto())
			{
				Pedidos = await bdContexto.Pedido.ToListAsync();
			}
			return Pedidos;
		}
		internal static IQueryable<Pedido> QuerySelect(IQueryable<Pedido> pQuery, Pedido pPedido)
		{
			//Para enteros y decimales
			if (pPedido.IdPedido > 0)
				pQuery = pQuery.Where(s => s.IdPedido == pPedido.IdPedido);
			if (pPedido.IdPedido > 0)
				pQuery = pQuery.Where(s => s.IdUsuario == pPedido.IdUsuario);

			pQuery = pQuery.OrderByDescending(s => s.IdPedido).AsQueryable();
			if (pPedido.Top_Aux > 0)
				pQuery = pQuery.Take(pPedido.Top_Aux).AsQueryable();
			return pQuery;
		}
		public static async Task<List<Pedido>> BuscarAsync(Pedido pPedido)
		{
			var Pedidos = new List<Pedido>();
			using (var bdContexto = new BDContexto())
			{
				var select = bdContexto.Pedido.AsQueryable();
				select = QuerySelect(select, pPedido);
				Pedidos = await select.ToListAsync();
			}
			return Pedidos;
		}

		public static async Task<List<Pedido>> BuscarIncluirUsuarioAsync(Pedido pPedido)
		{
			var Pedidos = new List<Pedido>();
			using (var bdContexto = new BDContexto())
			{
				var select = bdContexto.Pedido.AsQueryable();
				select = QuerySelect(select, pPedido).Include(s => s.Usuario).AsQueryable();
				Pedidos = await select.ToListAsync();
			}
			return Pedidos;
		}
	}
}


