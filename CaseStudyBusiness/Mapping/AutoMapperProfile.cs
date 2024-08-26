using AutoMapper;
using CaseStudyBusiness.Dtos;
using CaseStudyEntity.Entity;

namespace CaseStudyBusiness.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserCreateDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            CreateMap<UserUpdateDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            // Product mappings
            CreateMap<Product, ProductDto>();
            CreateMap<ProductCreateDto, Product>()
                .ForMember(dest => dest.SellerId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Enabled, opt => opt.Ignore());
            CreateMap<ProductUpdateDto, Product>();

            // Product Comment mappings
            CreateMap<ProductComment, ProductCommentDto>();
            CreateMap<CreateProductCommentDto, ProductComment>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsConfirmed, opt => opt.Ignore());
            CreateMap<UpdateProductCommentDto, ProductComment>();
        }
    }
}
