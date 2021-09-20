namespace CloudEngineMX.Erp.SetinTred.Web.Data.Mapper
{
    using AutoMapper;
    using Core.Entities;
    using ViewModels;

    public class ConfidenceLevelProfile : Profile
    {
        public ConfidenceLevelProfile()
        {
            CreateMap<ConfidenceLevel, ConfidenceLevelViewModel>(MemberList.None)
                .ForMember(x => x.ConfidenceLevelName, map => map.MapFrom(x => x.ConfidenceLevelName))
                .ForMember(x => x.Key, map => map.MapFrom(x => x.Key))
                .ForMember(x => x.IsActive, map => map.MapFrom(x => x.IsActive));
        }
    }
}
