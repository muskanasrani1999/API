using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class SignUpAPIDbContext : DbContext
    {
        public SignUpAPIDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Signup> Sign { get; set; }
    }
}
