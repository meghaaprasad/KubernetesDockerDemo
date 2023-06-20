using Microsoft.EntityFrameworkCore;

namespace NAGPKubernetesDockerDemo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<books> books { get; set; }
    }
}
