namespace CloudEngineMX.Erp.SetinTred.Web.Data.Mapper
{
    using AutoMapper;
    using Core.Entities;
    using ViewModels;

    public class OperatingBaseProfile : Profile
    {
        public OperatingBaseProfile()
        {
            CreateMap<OperatingBase, OperatingBaseViewModel>(MemberList.None)
                .ForMember(x => x.OperatingBaseName, map => map.MapFrom(x => x.OperatingBaseName))
                .ForMember(x => x.Key, map => map.MapFrom(x => x.Key))
                .ForMember(x => x.IsActive, map => map.MapFrom(x => x.IsActive));
        }
    }
}
