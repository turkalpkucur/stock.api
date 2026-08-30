using Stock.Entities.Dtos;
using Stock.Entities.Entities;
using AutoMapper;
namespace Stock.Entities.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductGroupInsertRequestDto, ProductGroup>();
            CreateMap<ProductGroupUpdateRequestDto, ProductGroup>();
            //CreateMap<ProductGroup, ProductGroupResponseDto>();

            //// Diğer entity'ler için de burada tanımlarsınız
            //CreateMap<ProductRequestDto, Product>();
            //CreateMap<Product, ProductResponseDto>();
        }
    }
}
