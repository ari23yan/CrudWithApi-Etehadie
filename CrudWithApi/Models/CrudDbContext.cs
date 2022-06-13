using Microsoft.EntityFrameworkCore;

namespace CrudWithApi.Models
{
    public class CrudDbContext:DbContext
    {
        public CrudDbContext(DbContextOptions<CrudDbContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
