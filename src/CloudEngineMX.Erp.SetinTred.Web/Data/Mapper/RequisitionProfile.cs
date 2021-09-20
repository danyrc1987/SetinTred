namespace CloudEngineMX.Erp.SetinTred.Web.Data.Mapper
{
    using AutoMapper;
    using Core.Entities;
    using ViewModels;

    public class RequisitionProfile : Profile
    {
        public RequisitionProfile()
        {
            CreateMap<Requisition, RequisitionViewModel>(MemberList.None)
                .ForMember(x => x.OperatingBaseName, map => map.MapFrom(x => x.OperatingBase.OperatingBaseName))
                .ForMember(x => x.AreaName, map => map.MapFrom(x => x.Area.AreaName))
                .ForMember(x => x.UserName, map => map.MapFrom(x => x.UserCore.FullName))
                .ForMember(x => x.RequisitionKey, map => map.MapFrom(x => x.Key));

            CreateMap<RequisitionDetail, RequisitionDetailItemViewModel>(MemberList.None)
                .ForMember(x => x.DetailKey, map => map.MapFrom(x => x.Key))
                .ForMember(x => x.Review, map => map.MapFrom(x => x.Review));

            CreateMap<PreQuotation, RequisitionQuotationViewModel>(MemberList.None)
                .ForMember(x => x.FileName, map => map.MapFrom(x => x.FileName))
                .ForMember(x => x.RouteFile, map => map.MapFrom(x => x.RouteFile))
                .ForMember(x => x.QuotationKey, map => map.MapFrom(x => x.Key))
                .ForMember(x => x.RequisitionKey, map => map.MapFrom(x => x.Key));
        }
    }
}
