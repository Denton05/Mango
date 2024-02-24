using Mango.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.Data;

public class AppDbContext : DbContext
{
    #region Properties

    private DbSet<Coupon> Coupons { get; set; }

    #endregion

    #region Construction

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    #endregion
}
