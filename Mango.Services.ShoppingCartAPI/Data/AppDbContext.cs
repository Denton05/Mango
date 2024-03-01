using Mango.Services.ShoppingCartAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ShoppingCartAPI.Data;

public class AppDbContext : DbContext
{
    #region Properties

    public DbSet<CartDetails> CartDetails { get; set; }

    public DbSet<CartHeader> CartHeaders { get; set; }

    #endregion

    #region Construction

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    #endregion
}
