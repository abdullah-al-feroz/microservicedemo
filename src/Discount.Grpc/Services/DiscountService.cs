using AutoMapper;
using Discount.Grpc.Protos;
using Discount.Grpc.Repository;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        ICouponRepository _couponRepository;
        ILogger<DiscountService> _logger;
        IMapper _mapper;

        public DiscountService(ICouponRepository couponRepository, ILogger<DiscountService> logger, IMapper Mapper)
        {
            _couponRepository = couponRepository;
            _logger = logger;
            _mapper = Mapper;
        }

        public override async Task<CouponRequest>GetDiscount (GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _couponRepository.GetDiscount(request.ProductId);
            if(coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Discount Not Found"));
            }
            _logger.LogInformation("Discount is retrived for PoductName: {poductName}, Amount: {amount}", coupon.PoductName, coupon.ProductId, coupon.Amount);
            //return new CouponRequest { ProductId = coupon.ProductId, PoductName = coupon.PoductName, Description = coupon.Description, Amount = coupon.Amount };
            return _mapper.Map<CouponRequest>(coupon)
        }
    }
}
