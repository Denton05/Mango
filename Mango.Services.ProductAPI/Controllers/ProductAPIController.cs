using AutoMapper;
using Mango.Services.ProductAPI.Data;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        #region Fields

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ResponseDto _response;

        #endregion

        #region Construction

        public ProductAPIController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        #endregion

        #region Public Methods

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseDto> Delete(int id)
        {
            try
            {
                var obj = await _context.Products.FirstAsync(o => o.ProductId == id);
                _context.Products.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpGet]
        public async Task<ResponseDto> Get()
        {
            try
            {
                IEnumerable<Product> objList = await _context.Products.ToListAsync();
                _response.Result = _mapper.Map<IEnumerable<ProductDto>>(objList);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ResponseDto> Get(int id)
        {
            try
            {
                var obj = await _context.Products.FirstAsync(o => o.ProductId == id);
                _response.Result = _mapper.Map<ProductDto>(obj);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseDto> Post([FromBody] ProductDto couponDto)
        {
            try
            {
                var obj = _mapper.Map<Product>(couponDto);
                await _context.Products.AddAsync(obj);
                await _context.SaveChangesAsync();
                _response.Result = _mapper.Map<ProductDto>(obj);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseDto> Put([FromBody] ProductDto couponDto)
        {
            try
            {
                var obj = _mapper.Map<Product>(couponDto);
                _context.Products.Update(obj);
                await _context.SaveChangesAsync();
                _response.Result = _mapper.Map<ProductDto>(obj);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        #endregion
    }
}
