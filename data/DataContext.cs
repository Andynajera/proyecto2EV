using Microsoft.EntityFrameworkCore;
using Classes;

namespace Data
{

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
       
        public DbSet<User>? User { get; set; }
        public DbSet<TipoGasoleo>? TipoGasoleos { get; set; }
        public DbSet<Promocion>? Promociones { get; set; }
        
    }
}
