using AdyenAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AdyenAPI.Data
{
    public class AdyenAPIContext : DbContext
    {
        public AdyenAPIContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<payments>? Payment { get; set; }
    }
}
