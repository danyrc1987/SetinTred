using CloudEngineMX.Erp.SetinTred.Core;
using jsreport.AspNetCore;
using jsreport.Binary;
using jsreport.Local;
using Microsoft.AspNetCore.Mvc;

namespace CloudEngineMX.Erp.SetinTred.Web
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using AutoMapper;
    using Core.Interfaces;
    using Core.Services;
    using Data.Validator;
    using Extensions.Identity;
    using FluentValidation.AspNetCore;
    using Infrastructure.Data;
    using Infrastructure.Data.Identity;
    using Infrastructure.Interfaces;
    using Infrastructure.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("es-MX");
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("es-MX") };
                options.RequestCultureProviders.Clear();
            });


            services.AddControllersWithViews();
            services.AddRazorPages()
                .AddRazorRuntimeCompilation();
            var tempPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "jsreport");

            services.AddMvc(setup => { })
                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<LoginValidator>());
            services.AddJsReport(new LocalReporting()
                .UseBinary(JsReportBinary.GetBinary())
                .KillRunningJsReportProcesses()
                .AsUtility()
                .Create());

            services.AddIdentity<User, IdentityRole>(cfg =>
                {
                    cfg.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
                    cfg.SignIn.RequireConfirmedEmail = true;
                    cfg.User.RequireUniqueEmail = true;
                    cfg.Password.RequireDigit = false;
                    cfg.Password.RequiredUniqueChars = 1;
                    cfg.Password.RequireLowercase = false;
                    cfg.Password.RequireNonAlphanumeric = false;
                    cfg.Password.RequireUppercase = false;
                    cfg.Password.RequiredLength = 8;
                })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<DataContext>();

            services.AddDbContext<DataContext>(cfg =>
            {
                cfg.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddTransient<SeedDb>();
            services.AddScoped<IUserCoreService, UserCoreService>();
            services.AddScoped<IAreaService, AreaService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IOperatingBaseService, OperatingBaseService>();
            services.AddScoped<IConfidenceLevelService, ConfidenceLevelService>();
            services.AddScoped<ISpecificationService, SpecificationService>();
            services.AddScoped<ISpecificationTypeService, SpecificationTypeService>();
            services.AddScoped<IRequisitionService, RequisitionService>();
            services.AddScoped<IRequisitionDetailService, RequisitionDetailService>();
            services.AddScoped<IMeasurementMaterialService, MeasurementMaterialService>();
            services.AddScoped<IManufacturerService, ManufacturerService>();
            services.AddScoped<IMaterialInventoryService, MaterialInventoryService>();
            services.AddScoped<IMaterialRequestService, MaterialRequestService>();
            services.AddScoped<IMaterialRequestDetailService, MaterialRequestDetailService>();
            services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
            services.AddScoped<IPurchaseOrderDetailService, PurchaseOrderDetailService>();
            services.AddScoped<IQuotationService, QuotationService>();
            services.AddScoped<IPreQuotationService, PreQuotationService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IReportToPayService, ReportToPayService>();
            services.AddScoped<IReportToPayDetailService, ReportToPayDetailService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ICostCenterService, CostCenterService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerDocumentService, CustomerDocumentService>();
            services.AddScoped<ICustomerQuoteService, CustomerQuoteService>();
            services.AddScoped<ICustomerQuoteDetailService, CustomerQuoteDetailService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserClaimsPrincipalFactory<User>, CustomClaimsPrincipalFactory>();
            services.AddScoped<IMailService, MailService>();

            services.AddSingleton((ILogger)new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs\\SetinTred-.log"),
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 15,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}" +
                                    "{Exception}",
                    shared: true,
                    rollOnFileSizeLimit: true)
                //.WriteTo.Console()
                .CreateLogger());

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;

            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(15);

                options.LoginPath = "/Account/Login";
                options.SlidingExpiration = true;
            });

            services.Configure<CookieTempDataProviderOptions>(options =>
            {
                options.Cookie.IsEssential = true;
            });

            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthorization();
            app.UseRequestLocalization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
