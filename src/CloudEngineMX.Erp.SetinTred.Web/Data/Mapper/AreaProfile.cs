using AutoMapper;
using CloudEngineMX.Erp.SetinTred.Core.Entities;
using CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels;

namespace CloudEngineMX.Erp.SetinTred.Web.Data.Mapper
{
    public class AreaProfile : Profile
    {
        public AreaProfile()
        {
            CreateMap<Area, AreaViewModel>(MemberList.None)
                .ForMember(x => x.AreaName, map => map.MapFrom(x => x.AreaName))
                .ForMember(x => x.AreaKey, map => map.MapFrom(x => x.Key));
        }
    }
}
