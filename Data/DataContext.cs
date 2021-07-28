using calendar_api.Models;
using Microsoft.EntityFrameworkCore;

namespace calendar_api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}

        public DbSet<Task> task_data { get; set; }
    }
}