using Microsoft.EntityFrameworkCore;
 
namespace RESTauranter.Models
{
    public class RestauranterContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public RestauranterContext(DbContextOptions<RestauranterContext> options) : base(options) { }

        public DbSet<Review> reviews { get; set; }
    }
}