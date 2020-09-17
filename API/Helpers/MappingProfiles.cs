using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.OrderAggregate;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
            .ForMember(p => p.ProductBrand, o => o.MapFrom( s => s.ProductBrand.Name))
            .ForMember(p => p.ProductType, o => o.MapFrom( s => s.ProductType.Name))
            .ForMember(p => p.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

            CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();
            // CreateMap<AddressDto, Address>();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();

            CreateMap<Order, OrderToReturnDto>()
            .ForMember(p => p.DeliveryMethod, o => o.MapFrom( s => s.DeliveryMethod.ShortName))
            .ForMember(p => p.ShippingPrice, o => o.MapFrom( s => s.DeliveryMethod.Price));
            
            CreateMap<OrderItem, OrderItemDto>()
            .ForMember(p => p.ProductId, o => o.MapFrom( s => s.ItemOrdered.ProductItemId))
            .ForMember(p => p.ProductName, o => o.MapFrom( s => s.ItemOrdered.ProductName))
            .ForMember(p => p.PictureUrl, o => o.MapFrom( s => s.ItemOrdered.PictureUrl))
            .ForMember(p => p.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>());
        }
    }
}