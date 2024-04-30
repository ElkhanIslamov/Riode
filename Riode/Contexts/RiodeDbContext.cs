using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Riode.Models;

namespace Riode.Contexts;

public class RiodeDbContext : IdentityDbContext<AppUser>
{
    public RiodeDbContext(DbContextOptions<RiodeDbContext> options) : base(options)
    {

    }

    public DbSet<Slider> Sliders { get; set; } = null!;
    public DbSet<Shipping> Shippings { get; set; } = null!; 
    
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;   
    public DbSet<Setting> Settings { get; set; } = null !;
    public DbSet<Blog> Blogs { get; set; } = null !;
    public DbSet<BasketModel> BasketModels { get; set; } = null !;
    
    
    

    
}
