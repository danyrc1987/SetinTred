using System.Linq;

namespace CloudEngineMX.Erp.SetinTred.Web.Data.Mapper
{
    using AutoMapper;
    using Core.Entities;
    using ViewModels;

    public class PurchaseOrderProfile : Profile
    {
        public PurchaseOrderProfile()
        {
            CreateMap<PurchaseOrder, PurchaseOrderViewModel>(MemberList.None)
                .ForMember(x => x.PurchaseOrderKey, map => map.MapFrom(x => x.Key))
                .ForMember(x => x.UserName, map => map.MapFrom(x => x.UserCore.FullName))
                .ForMember(x => x.OperatingBaseName, map => map.MapFrom(x => x.OperatingBase.OperatingBaseName))
                .ForMember(x => x.SupplierName, map => map.MapFrom(x => x.Supplier.FiscalName))
                .ForMember(x => x.CurrencyCode, map => map.MapFrom(x => x.Currency.CurrencyName))
                .ForMember(x => x.WithApprovedDetails,
                    map => map.MapFrom(x =>
                        x.PurchaseOrderDetails != null &&
                        x.PurchaseOrderDetails.Count(detail => detail.IsApproved) > 0))
                .ForMember(x => x.Subtotal,
                    map => map.MapFrom(x => x.PurchaseOrderDetails.Sum(o => o.Quantity * o.UnitPrice)))
                .ForMember(x => x.CurrencyCode, map => map.MapFrom(x => x.Currency.CurrencyCode));

            CreateMap<PurchaseOrderDetail, PurchaseOrderDetailViewModel>(MemberList.None)
                .ForMember(x => x.PurchaseOrderDetailKey, map => map.MapFrom(x => x.Key));

            CreateMap<Quotation, PurchaseOrderQuotationViewModel>(MemberList.None)
                .ForMember(x => x.FileName, map => map.MapFrom(x => x.FileName))
                .ForMember(x => x.RouteFile, map => map.MapFrom(x => x.RouteFile))
                .ForMember(x => x.QuotationKey, map => map.MapFrom(x => x.Key));
        }
    }
}
