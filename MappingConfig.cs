using AutoMapper;
using DesafioApi.Models;
using DesafioApi.Models.Dto;

namespace DesafioApi
{
    public class MappingConfig
    {

        public static MapperConfiguration RegisterMaps()
        {

            var mappingConfiguration = new MapperConfiguration(config =>
           {
               config.CreateMap<StoreDto, Store>().ReverseMap();
               config.CreateMap<CategoryDto, Category>().ReverseMap();
               config.CreateMap<ProductDto, Product>().ReverseMap();
               config.CreateMap<VoucherDto, Voucher>().ReverseMap();
               config.CreateMap<StoreProductsDto, StoreProducts>().ReverseMap();
               config.CreateMap<VoucherProductsDto, VoucherProducts>().ReverseMap();
               config.CreateMap<CartDetailDto, CartDetail>().ReverseMap();
               config.CreateMap<CartDto, Cart>().ReverseMap();


           }
            );

            return mappingConfiguration;

        }
    }
}
