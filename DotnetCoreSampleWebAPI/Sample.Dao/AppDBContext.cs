using Microsoft.EntityFrameworkCore;
using Sample.Dto;

namespace Sample.Dao
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Person> person { get; set; }
        public DbSet<Address> address { get; set; }
    }
}
