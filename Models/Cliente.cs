using System.ComponentModel.DataAnnotations;

namespace ClienteProdutoApiPloomes.Models
{
	public class Cliente
	{
		private int id;
		private string? nome;
		private string? telefone;
		private string? email;
		private string? cpf;

		[Key]
		public int? Id { get { return id; } }

		[Required (ErrorMessage ="O nome do cliente é obrigatório")]
		[StringLength(100, ErrorMessage = "Limite de 100 caracteres alcançado")]
		public string? Nome { get { return nome; } set { nome = value; } }

		[Required (ErrorMessage ="O telefone do cliente é obrigatório")]
		[RegularExpression(@"^[0-9]*$", ErrorMessage = "Insira apenas números para o Telefone")]
		[StringLength(11, MinimumLength =10, ErrorMessage = "O número de telefone deve conter de 10 a 11 dígitos")]
		public string? Telefone { get { return telefone; } set { telefone = value; } }

		[Required (ErrorMessage ="O e-mail do cliente é obrigatório")]
		[StringLength(100, ErrorMessage = "O e-mail só pode conter até 100 caracteres")]
		public string? Email { get { return email; } set { email = value; } }

		[StringLength(11, ErrorMessage = "O cpf deve ter 11 dígitos")]
		[RegularExpression(@"^[0-9]*$", ErrorMessage = "Insira apenas números para o CPF")]
		public string? Cpf { get { return cpf; } set { cpf = value; } }

		public override string ToString()
		{
			string res = "";
			res += $"ID do cliente: {this.id};\nNome do cliente: {this.nome};";
			res += $"\nTelefone para contato: {this.telefone};\nE-mail para contato: {this.email};";
			res += $"\nCPF do cliente: {this.cpf}";
			return res;
		}

	}
}
