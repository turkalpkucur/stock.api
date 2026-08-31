using Stock.Entities.Entities;
using AutoMapper;
using Stock.Entities.Dtos.ProductGroup;
using Stock.Entities.Dtos.Product;
using Stock.Entities.Dtos.UserProfile;
using Stock.Entities.Dtos.User;
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

            CreateMap<UserProfileInsertRequestDto, UserProfile>();
            CreateMap<UserProfileUpdateRequestDto, UserProfile>();

            CreateMap<UserInsertRequestDto, User>();
            CreateMap<UserUpdateRequestDto, User>();
        }
    }
}
