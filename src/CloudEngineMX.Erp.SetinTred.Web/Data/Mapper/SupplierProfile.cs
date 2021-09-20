namespace CloudEngineMX.Erp.SetinTred.Web.Data.Mapper
{
    using System;
    using AutoMapper;
    using Core.Entities;
    using ViewModels;

    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<SupplierViewModel, Supplier>(MemberList.None)
                .ForMember(x => x.Key, map => map.MapFrom(x => Guid.NewGuid().ToString()))
                .ForMember(x => x.CreationDate, map => map.MapFrom(x => DateTime.Now));

            CreateMap<Supplier, SupplierViewModel>(MemberList.None)
                .ForMember(x => x.SupplierKey, map => map.MapFrom(x => x.Key));
        }
    }
}
