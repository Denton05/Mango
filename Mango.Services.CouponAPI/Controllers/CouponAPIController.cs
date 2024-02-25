using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        #region Fields

        private readonly AppDbContext _context;

        #endregion

        #region Construction

        public CouponAPIController(AppDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Public Methods

        [HttpGet]
        public object Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _context.Coupons.ToList();
                return objList;
            }
            catch(Exception ex)
            {
            }

            return null;
        }

        [HttpGet]
        [Route("{id:int}")]
        public object Get(int id)
        {
            try
            {
                var obj = _context.Coupons.Find(id);
                return obj;
            }
            catch(Exception ex)
            {
            }

            return null;
        }

        #endregion
    }
}
