using Discount.API.Data;
using Discount.API.Models;
using Discount.Grpc;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.API.Services;

public class DiscountService(DiscountContext db) : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await db.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

        coupon ??= new Coupon
        {
            ProductName = "No Discount",
            Amount = 0,
            Description = "Not discount for this product"
        };

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = new Coupon
        {
            Id = request.Coupon.Id,
            ProductName = request.Coupon.ProductName,
            Amount = request.Coupon.Amount,
            Description = request.Coupon.Description,
        };

        db.Coupons.Add(coupon);
        await db.SaveChangesAsync();

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = new Coupon
        {
            Id = request.Coupon.Id,
            ProductName = request.Coupon.ProductName,
            Amount = request.Coupon.Amount,
            Description = request.Coupon.Description,
        };

        db.Coupons.Update(coupon);
        await db.SaveChangesAsync();

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await db.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

        ArgumentNullException.ThrowIfNull(coupon);

        db.Coupons.Remove(coupon);
        await db.SaveChangesAsync();

        return new DeleteDiscountResponse { Success = true };
    }
}