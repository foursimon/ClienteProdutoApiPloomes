using System.ComponentModel.DataAnnotations;
namespace ClienteProdutoApiPloomes.Models
{
	public class Eletronico
	{
		private int id;
		private string? nome;
		private string? tipo;
		private string? marca;
		private string? descricao;
		private int? quantidadeEstoque;

		[Key]
		public int Id { get { return id; } }
		[Required (ErrorMessage = "Insira o nome do produto.")]
		[StringLength(100, ErrorMessage = "O nome do produto não pode passar de 100 caracteres.")]
		public string? Nome { get { return nome; } set { nome = value; } }
		[Required (ErrorMessage = "Insira o tipo do produto.")]
		[StringLength(100, ErrorMessage = "O tipo do produto não pode passar de 100 caracteres.")]
		public string? Tipo { get { return tipo; } set { tipo = value; } }
		[Required (ErrorMessage = "Insira a marca do produto.")]
		[StringLength(100, ErrorMessage = "A marca do produto não pode passar de 100 caracteres.")]
		public string ?Marca { get { return marca; } set { marca = value; } }
		[Required (ErrorMessage = "Insira a descrição do produto.")]
		[StringLength(1500, ErrorMessage = "A descrição só pode conter 500 caracteres no máximo.")]
		public string? Descricao { get { return descricao; } set { descricao = value; } }
		[Range(0, 1000, ErrorMessage = "A quantidade no estoque não pode exceder 1000 unidades")]
		public int? QuantidadeEstoque { get { return quantidadeEstoque; } set { quantidadeEstoque= value; } }

		public override string ToString()
		{
			string res = "";
			res += $"Id: {this.id};\nNome: {this.nome};\nTipo: {this.tipo};\nMarca: {this.marca};";
			res += $"\nDescrição: {this.descricao};\nQuantidade no estoque: {this.quantidadeEstoque}";
			return res;
		}
	}
}
