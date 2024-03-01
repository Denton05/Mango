using AutoMapper;
using Mango.Services.ShoppingCartAPI.Data;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.Dto;
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
        private readonly IMapper _mapper;
        private readonly ResponseDto _response;

        #endregion

        #region Construction

        public CartAPIController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _response = new ResponseDto();
            _mapper = mapper;
        }

        #endregion

        #region Public Methods

        [HttpPost("CartUpsert")]
        public async Task<ResponseDto> CartUpsert(CartDto cartDto)
        {
            try
            {
                var cartHeaderFromDb = await _context.CartHeaders.FirstOrDefaultAsync(ch => ch.UserId == cartDto.CartHeader.UserId);
                if(cartHeaderFromDb == null)
                {
                    // create header and details
                    var cartHeader = _mapper.Map<CartHeader>(cartDto.CartHeader);
                    await _context.CartHeaders.AddAsync(cartHeader);
                    await _context.SaveChangesAsync();

                    cartDto.CartDetails.First().CartDetailsId = cartHeader.CartHeaderId;
                    var cartDetails = _mapper.Map<CartDetails>(cartDto.CartDetails.First());
                    await _context.CartDetails.AddAsync(cartDetails);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // if header is not null
                    // check if details has same product
                    var cartDetailsFromDb =
                        await _context.CartDetails.FirstOrDefaultAsync(cd => cd.ProductId == cartDto.CartDetails.First().ProductId &&
                                                                             cd.CartHeaderId == cartHeaderFromDb.CartHeaderId);

                    if(cartDetailsFromDb == null)
                    {
                        // create cartdetails
                    }
                    // update count in cartdetails
                }
            }
            catch(Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
        }

        #endregion
    }
}
