using Microsoft.EntityFrameworkCore;

namespace SmartPlate.API.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
