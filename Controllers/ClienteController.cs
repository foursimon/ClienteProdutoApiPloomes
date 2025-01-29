using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClienteProdutoApiPloomes.BaseDeDados;
using ClienteProdutoApiPloomes.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ClienteProdutoApiPloomes.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClienteController : ControllerBase
	{
		private readonly ClienteProduto _contexto;
		public ClienteController(ClienteProduto contexto)
		{
			this._contexto = contexto;
		}

		[HttpGet]
		public async Task<ActionResult> BuscarTodosClientes()
		{
			try
			{
				var bancoDados = await _contexto.Clientes.ToListAsync();
				if (bancoDados.Count() == 0) return NotFound("Não há clientes cadastrados no sistema");
				List<Cliente> clientes = bancoDados;
				return Ok(clientes);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}
		[HttpGet("{id}")]
		public async Task<ActionResult> BuscarClientePorId(int id)
		{
			try
			{
				var bancoDados = await _contexto.Clientes.FindAsync(id);
				if (bancoDados == null) return NotFound($"Cliente de id: {id} não foi encontrado no sistema.");
				return Ok(bancoDados);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("cpf/{cpf}")]
		public async Task<ActionResult> BuscarClientePorCpf(string cpf)
		{
			if (cpf.Length < 11 || cpf.Length > 11) return BadRequest("O cpf deve ter 11 dígitos");
			try
			{
				var bancoDados = await _contexto.Clientes.FirstOrDefaultAsync(cliente => cliente.Cpf == cpf);
				if (bancoDados == null) return NotFound($"Cliente com CPF: {cpf} não foi encontrado no sistema.");
				return Ok(bancoDados);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("cadastrar_cliente")]
		public async Task<ActionResult> CadastrarNovoCliente([FromBody] Cliente cliente)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(cliente);
			}
			try
			{
				Cliente novoCliente = cliente;
				await _contexto.Clientes.AddAsync(novoCliente);
				await _contexto.SaveChangesAsync();
				return Ok(novoCliente + "\nNovo cliente registrado com sucesso!");

			} catch (Exception ex) {
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("remover_cliente/{id}")]
		public async Task<ActionResult> RemoverCliente(int id)
		{
			try
			{
				var bancoDados = await _contexto.Clientes.FindAsync(id);
				_contexto.Remove(bancoDados);
				await _contexto.SaveChangesAsync();
				return Ok(bancoDados + "\nFoi removido do sistema");

			}
			catch (ArgumentNullException)
			{
				return NotFound("Cliente com o ID informado não foi encontrado no sistema");
			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("atualizar_cliente/{id}")]
		public async Task<ActionResult> AtualizarCliente([FromBody]Cliente cliente, [FromRoute]int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(cliente);
			}
			try
			{
				var bancoDados = await _contexto.Clientes.FindAsync(id);
				bancoDados.Nome = cliente.Nome;
				bancoDados.Telefone = cliente.Telefone;
				bancoDados.Cpf = cliente.Cpf;
				bancoDados.Email = cliente.Email;
				_contexto.Clientes.Update(bancoDados);
				await _contexto.SaveChangesAsync();
				return Ok(bancoDados + "\nDados do cliente foram atualizados.");
			}catch(NullReferenceException)
			{
				return NotFound("Cliente do ID informado não foi encontrado");
			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
