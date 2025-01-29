using Microsoft.AspNetCore.Mvc;
using ClienteProdutoApiPloomes.Models;
using ClienteProdutoApiPloomes.BaseDeDados;
using Microsoft.EntityFrameworkCore;

namespace ClienteProdutoApiPloomes.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EletronicoController : ControllerBase
	{
		private readonly ClienteProduto _contexto;
		public EletronicoController(ClienteProduto contexto)
		{
			_contexto = contexto;
		}

		[HttpGet]
		public async Task<ActionResult> BuscarTodosEletronicos()
		{
			try
			{
				var bancoDados = await _contexto.Eletronicos.ToListAsync();
				if (bancoDados.Count() == 0) return NotFound("Não há eletrônicos registrados no sistema");
				List<Eletronico> eletronicos = bancoDados;
				return Ok(eletronicos);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<ActionResult> BuscarEletronicoPorId(int id)
		{
			try
			{
				var bancoDados = await _contexto.Eletronicos.FindAsync(id);
				if (bancoDados == null) return NotFound($"Eletrônico de id: {id} não foi encontrado.");
				return Ok(bancoDados);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("tipo/{tipo}")]
		public async Task<ActionResult> BuscarEletronicosPorTipo(string tipo)
		{
			try
			{
				var bancoDados = await _contexto.Eletronicos.Where(e => e.Tipo == tipo).ToListAsync();
				if (bancoDados.Count() <= 0) return NotFound($"Não há eletrônicos do tipo: {tipo} registrados no sistema");
				return Ok(bancoDados);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("marca/{marca}")]
		public async Task<ActionResult> BuscarEletronicosPorMarca(string marca)
		{
			try
			{
				var bancoDados = await _contexto.Eletronicos.Where(e => e.Marca == marca).ToListAsync();
				if (bancoDados.Count() <= 0) return NotFound($"Não há eletrônicos da marca: {marca} registrados no sistema");
				return Ok(bancoDados);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("registrar_eletronico")]
		public async Task<ActionResult> RegistrarNovoEletronico([FromBody] Eletronico eletronico)
		{
			if (!ModelState.IsValid) return BadRequest(eletronico);
			try
			{
				await _contexto.AddAsync(eletronico);
				await _contexto.SaveChangesAsync();
				return Ok(eletronico + "\nEletrônico adicionado com sucesso!");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("remover_eletronico/{id}")]
		public async Task<ActionResult> RemoverEletronico(int id)
		{
			try
			{
				var bancoDados = await _contexto.Eletronicos.FindAsync(id);
				_contexto.Remove(bancoDados);
				await _contexto.SaveChangesAsync();
				return Ok(bancoDados + "\nFoi removido do sistema");

			}
			catch (ArgumentNullException)
			{
				return NotFound("Eletronico com o ID informado não foi encontrado no sistema");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("atualizar_eletronico/{id}")]
		public async Task<ActionResult> AtualizarEletronico([FromBody] Eletronico eletronico, [FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(eletronico);
			}
			try
			{
				var bancoDados = await _contexto.Eletronicos.FindAsync(id);
				bancoDados.Nome = eletronico.Nome;
				bancoDados.Tipo = eletronico.Tipo;
				bancoDados.Marca = eletronico.Marca;
				bancoDados.Descricao = eletronico.Descricao;
				bancoDados.QuantidadeEstoque = eletronico.QuantidadeEstoque;
				_contexto.Eletronicos.Update(bancoDados);
				await _contexto.SaveChangesAsync();
				return Ok(bancoDados + "\nDados do eletrônico foram atualizados.");
			}
			catch (NullReferenceException)
			{
				return NotFound("O eletrônico de id informado não foi encontrado no sistema.");
			}
			catch (Exception ex) 
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
