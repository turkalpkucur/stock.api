using Stock.Entities.Entities;
using AutoMapper;
using Stock.Entities.Dtos.ProductGroup;
using Stock.Entities.Dtos.Product;
namespace Stock.Entities.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductGroupInsertRequestDto, ProductGroup>();
            CreateMap<ProductGroupUpdateRequestDto, ProductGroup>();

            CreateMap<ProductInsertRequestDto, Product>();
            CreateMap<ProductUpdateRequestDto, Product>();
            //CreateMap<ProductGroup, ProductGroupResponseDto>();

            //// Diğer entity'ler için de burada tanımlarsınız
            //CreateMap<ProductRequestDto, Product>();
            //CreateMap<Product, ProductResponseDto>();
        }
    }
}
