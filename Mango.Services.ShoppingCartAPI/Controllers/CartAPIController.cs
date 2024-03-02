using AutoMapper;
using Mango.Services.ShoppingCartAPI.Data;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.Dto;
using Mango.Services.ShoppingCartAPI.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ShoppingCartAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartAPIController : ControllerBase
    {
        #region Fields

        private readonly AppDbContext _context;
        private readonly ICouponService _couponService;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly ResponseDto _response;

        #endregion

        #region Construction

        public CartAPIController(AppDbContext context, IMapper mapper, IProductService productService, ICouponService couponService)
        {
            _context = context;
            _response = new ResponseDto();
            _mapper = mapper;
            _productService = productService;
            _couponService = couponService;
        }

        #endregion

        #region Public Methods

        [HttpPost("ApplyCoupon")]
        public async Task<object> ApplyCoupon([FromBody] CartDto cartDto)
        {
            try
            {
                var cart = await _context.CartHeaders.FirstAsync(ch => ch.UserId == cartDto.CartHeader.UserId);
                cart.CouponCode = cartDto.CartHeader.CouponCode;
                _context.CartHeaders.Update(cart);
                await _context.SaveChangesAsync();
                _response.Result = true;
            }
            catch(Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }

        [HttpPost("CartUpsert")]
        public async Task<ResponseDto> CartUpsert(CartDto cartDto)
        {
            try
            {
                var cartHeaderFromDb = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(ch => ch.UserId == cartDto.CartHeader.UserId);
                if(cartHeaderFromDb == null)
                {
                    var cartHeader = _mapper.Map<CartHeader>(cartDto.CartHeader);
                    await _context.CartHeaders.AddAsync(cartHeader);
                    await _context.SaveChangesAsync();

                    cartDto.CartDetails.First().CartHeaderId = cartHeader.CartHeaderId;
                    var cartDetails = _mapper.Map<CartDetails>(cartDto.CartDetails.First());
                    await _context.CartDetails.AddAsync(cartDetails);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var cartDetailsFromDb = await _context.CartDetails.AsNoTracking()
                                                          .FirstOrDefaultAsync(cd => cd.ProductId == cartDto.CartDetails.First().ProductId &&
                                                                                     cd.CartHeaderId == cartHeaderFromDb.CartHeaderId);

                    if(cartDetailsFromDb == null)
                    {
                        cartDto.CartDetails.First().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                        var cartDetails = _mapper.Map<CartDetails>(cartDto.CartDetails.First());
                        await _context.CartDetails.AddAsync(cartDetails);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        var cardDetails = cartDto.CartDetails.First();
                        cardDetails.Count += cartDetailsFromDb.Count;
                        cardDetails.CartHeaderId = cartDetailsFromDb.CartHeaderId;
                        cardDetails.CartDetailsId = cartDetailsFromDb.CartDetailsId;
                        var cartDetails = _mapper.Map<CartDetails>(cartDto.CartDetails.First());
                        _context.CartDetails.Update(cartDetails);
                        await _context.SaveChangesAsync();
                    }
                }

                _response.Result = cartDto;
            }
            catch(Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }

        [HttpGet("GetCart/{userId}")]
        public async Task<ResponseDto> GetCart(string userId)
        {
            try
            {
                CartDto cartDto = new()
                                  {
                                      CartHeader = _mapper.Map<CartHeaderDto>(_context.CartHeaders.First(ch => ch.UserId == userId))
                                  };
                cartDto.CartDetails = _mapper.Map<IEnumerable<CartDetailsDto>>(_context.CartDetails.Where(cd => cd.CartHeaderId == cartDto.CartHeader.CartHeaderId));

                var productDtos = await _productService.GetProducts();
                foreach(var item in cartDto.CartDetails)
                {
                    item.Product = productDtos.FirstOrDefault(p => p.ProductId == item.ProductId);
                    cartDto.CartHeader.CartTotal += item.Count * item.Product.Price;
                }

                if(!string.IsNullOrEmpty(cartDto.CartHeader.CouponCode))
                {
                    var couponDto = await _couponService.GetCoupon(cartDto.CartHeader.CouponCode);
                    if(couponDto != null && cartDto.CartHeader.CartTotal > couponDto.MinAmount)
                    {
                        cartDto.CartHeader.CartTotal -= couponDto.DiscountAmount;
                        cartDto.CartHeader.Discount = couponDto.DiscountAmount;
                    }
                }

                _response.Result = cartDto;
            }
            catch(Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }

        [HttpPost("RemoveCart")]
        public async Task<ResponseDto> RemoveCart([FromBody] int cartDetailsId)
        {
            try
            {
                var cartDetails = await _context.CartDetails.FirstAsync(cd => cd.CartDetailsId == cartDetailsId);
                var totalCountOfCartItems = await _context.CartDetails.CountAsync(cd => cd.CartHeaderId == cartDetails.CartHeaderId);
                _context.CartDetails.Remove(cartDetails);

                if(totalCountOfCartItems == 1)
                {
                    var cartHeaderToRemove = await _context.CartHeaders.FirstOrDefaultAsync(ch => ch.CartHeaderId == cartDetails.CartHeaderId);
                    _context.CartHeaders.Remove(cartHeaderToRemove);
                }

                await _context.SaveChangesAsync();

                _response.Result = true;
            }
            catch(Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }

        [HttpPost("RemoveCoupon")]
        public async Task<object> RemoveCoupon([FromBody] CartDto cartDto)
        {
            try
            {
                var cart = await _context.CartHeaders.FirstAsync(ch => ch.UserId == cartDto.CartHeader.UserId);
                cart.CouponCode = string.Empty;
                _context.CartHeaders.Update(cart);
                await _context.SaveChangesAsync();
                _response.Result = true;
            }
            catch(Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }

        #endregion
    }
}
