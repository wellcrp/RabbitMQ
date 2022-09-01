using Microsoft.EntityFrameworkCore;
using Project.RabbitMQ.Models;

namespace Project.RabbitMQ.Infrastructure
{
    public class DbContextData : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbContextData(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder opt)
        {
            opt.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<ProductModel> Products { get; set; }
    }
}
