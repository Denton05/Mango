using AutoMapper;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;

namespace Mango.Services.CouponAPI;

public class MappingConfig
{
    #region Public Methods

    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
                                                    {
                                                        config.CreateMap<CouponDto, Coupon>();
                                                        config.CreateMap<Coupon, CouponDto>();
                                                    });

        return mappingConfig;
    }

    #endregion
}
