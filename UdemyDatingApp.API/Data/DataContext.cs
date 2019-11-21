using Microsoft.EntityFrameworkCore;
using UdemyDatingApp.API.Models;

namespace UdemyDatingApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Value> Values { get; set; }
    }
}