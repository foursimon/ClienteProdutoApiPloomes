
using Microsoft.EntityFrameworkCore;
using ClienteProdutoApiPloomes.Models;
namespace ClienteProdutoApiPloomes.BaseDeDados
{
	public class ClienteProduto : DbContext
	{
		private readonly IConfiguration configuracao;
		public ClienteProduto(IConfiguration configuracao)
		{
			this.configuracao = configuracao;
		}

		public ClienteProduto(DbContextOptions<ClienteProduto> options, IConfiguration configuration)
			:base(options)
		{
			configuracao = configuration;
		}

		public virtual DbSet<Cliente> Clientes { get; set; }
		public virtual DbSet<Eletronico> Eletronicos { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
			optionsBuilder.UseSqlServer(configuracao.GetConnectionString("DefaultConnection"));

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Cliente>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.ToTable("Cliente");

				entity.Property(e => e.Id).HasMaxLength(4).HasColumnName("id");
				entity.Property(e => e.Nome).HasMaxLength(100).HasColumnName("nome");
				entity.Property(e => e.Telefone).HasMaxLength(11).HasColumnName("telefone");
				entity.Property(e => e.Email).HasMaxLength(100).HasColumnName("email");
				entity.Property(e => e.Cpf).HasMaxLength(11).HasColumnName("cpf");
			});

			modelBuilder.Entity<Eletronico>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.ToTable("Eletronicos");

				entity.Property(e => e.Id).HasMaxLength(4).HasColumnName("id");
				entity.Property(e => e.Nome).HasMaxLength(100).HasColumnName("nome");
				entity.Property(e => e.Tipo).HasMaxLength(11).HasColumnName("tipo");
				entity.Property(e => e.Marca).HasMaxLength(100).HasColumnName("marca");
				entity.Property(e => e.Descricao).HasMaxLength(11).HasColumnName("descricao");
				entity.Property(e => e.QuantidadeEstoque).HasMaxLength(4).HasColumnName("quantidade_estoque");
			});
		}
	}
}
