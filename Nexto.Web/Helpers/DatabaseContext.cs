using Commom.Dto.Core;
using Commom.Dto.Solicitacao;
using Microsoft.EntityFrameworkCore;

namespace Nexto.Web.Helpers
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=liquid_code.Storage;Integrated Security=True;");
        }

        public DbSet<SolicitacaoDto> Solicitacaos { get; set; }
        public DbSet<UserDto> Users { get; set; }
        public DbSet<ArquivoDto> Arquivos { get; set; }
        public DbSet<FormularioDto> Formularios { get; set; }
    }
}

