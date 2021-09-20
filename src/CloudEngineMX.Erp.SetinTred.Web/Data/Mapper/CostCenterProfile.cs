using AutoMapper;
using CloudEngineMX.Erp.SetinTred.Core.Entities;
using CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels;

namespace CloudEngineMX.Erp.SetinTred.Web.Data.Mapper
{
    public class CostCenterProfile : Profile
    {
        public CostCenterProfile()
        {
            CreateMap<CostCenter, CostCenterViewModel>(MemberList.None)
                .ForMember(x => x.CostCenterName, map => map.MapFrom(x => x.CostCenterName))
                .ForMember(x => x.Key, map => map.MapFrom(x => x.Key))
                .ForMember(x => x.IsActive, map => map.MapFrom(x => x.IsActive));
        }
    }
}
