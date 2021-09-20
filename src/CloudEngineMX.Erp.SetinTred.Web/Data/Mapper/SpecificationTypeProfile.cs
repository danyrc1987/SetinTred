namespace CloudEngineMX.Erp.SetinTred.Web.Data.Mapper
{
    using AutoMapper;
    using Core.Entities;
    using ViewModels;

    public class SpecificationTypeProfile : Profile
    {
        public SpecificationTypeProfile()
        {
            CreateMap<SpecificationType, SpecificationTypeViewModel>(MemberList.None)
                .ForMember(x => x.SpecificationTypeName, map => map.MapFrom(x => x.SpecificationTypeName))
                .ForMember(x => x.Key, map => map.MapFrom(x => x.Key))
                .ForMember(x => x.IsActive, map => map.MapFrom(x => x.IsActive));
        }
    }
}
