using Microsoft.EntityFrameworkCore;
using ModOff6Api.Model;

namespace ModOff6Api.Context
{
    public class Contexto : DbContext
    {
        public Contexto()
        {

        }

        public Contexto(DbContextOptions<Contexto> options) :base(options) { }

        public DbSet<Cadastro> Cadastros { get; set; }
    }
}
