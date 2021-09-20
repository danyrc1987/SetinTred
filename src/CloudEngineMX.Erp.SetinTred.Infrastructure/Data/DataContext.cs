namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Data
{
    using System.Linq;
    using Configuration;
    using Core.Entities;
    using Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<UserCore> UserCores { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ConfidenceLevel> ConfidenceLevels { get; set; }
        public DbSet<OperatingBase> OperatingBases { get; set; }
        public DbSet<SpecificationType> SpecificationTypes { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<Requisition> Requisitions { get; set; }
        public DbSet<RequisitionDetail> RequisitionDetails { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<MeasurementMaterial> MeasurementMaterials { get; set; }
        public DbSet<MaterialInventory> MaterialInventories { get; set; }
        public DbSet<MaterialRequest> MaterialRequests { get; set; }
        public DbSet<MaterialRequestDetail> MaterialRequestDetails { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public DbSet<Quotation> Quotations { get; set; }
        public DbSet<PreQuotation> PreQuotations { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ReportEntity> ReportEntities { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ReportToPayDetail> ReportToPayDetails { get; set; }
        public DbSet<CostCenter> CostCenters { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerDocument> CustomerDocuments { get; set; }
        public DbSet<CustomerQuote> CustomerQuotes { get; set; }
        public DbSet<CustomerQuoteDetail> CustomerQuoteDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AreaEntityConfiguration());
            builder.ApplyConfiguration(new UserCoreEntityConfiguration());
            builder.ApplyConfiguration(new SupplierEntityConfiguration());
            builder.ApplyConfiguration(new ConfidenceLevelEntityConfiguration());
            builder.ApplyConfiguration(new OperatingBaseEntityConfiguration());
            builder.ApplyConfiguration(new SpecificationTypeEntityConfiguration());
            builder.ApplyConfiguration(new SpecificationEntityConfiguration());
            builder.ApplyConfiguration(new ManufacturerEntityConfiguration());
            builder.ApplyConfiguration(new MeasurementMaterialEntityConfiguration());
            builder.ApplyConfiguration(new MaterialInventoryEntityConfiguration());
            builder.ApplyConfiguration(new MaterialRequestEntityConfiguration());
            builder.ApplyConfiguration(new MaterialRequestDetailEntityConfiguration());
            builder.ApplyConfiguration(new PurchaseOrderEntityConfiguration());
            builder.ApplyConfiguration(new PurchaseOrderDetailEntityConfiguration());
            builder.ApplyConfiguration(new QuotationEntityConfiguration());
            builder.ApplyConfiguration(new PreQuotationEntityConfiguration());
            builder.ApplyConfiguration(new CurrencyEntityConfiguration());
            builder.ApplyConfiguration(new PaymentEntityConfiguration());
            builder.ApplyConfiguration(new CostCenterEntityConfiguration());
            builder.ApplyConfiguration(new CustomerEntityConfiguration());
            builder.ApplyConfiguration(new CustomerDocumentEntityConfiguration());
            builder.ApplyConfiguration(new CustomerQuoteEntityConfiguration());
            builder.ApplyConfiguration(new CustomerQuoteDetailEntityConfiguration());

            builder.Entity<ReportEntity>().HasNoKey();
            builder.Entity<ReportToPayDetail>().HasNoKey();

            var cascadeFks = builder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var fk in cascadeFks)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(builder);
        }
    }
}
