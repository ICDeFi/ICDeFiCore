using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BeCoreApp.Data.EF;
using BeCoreApp.Data.Entities;
using BeCoreApp.Extensions;
using PaulMiami.AspNetCore.Mvc.Recaptcha;
using AutoMapper;
using BeCoreApp.Services;
using BeCoreApp.Helpers;
using Microsoft.AspNetCore.Mvc.Razor;
using Newtonsoft.Json.Serialization;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using BeCoreApp.Infrastructure.Interfaces;
using BeCoreApp.Data.IRepositories;
using BeCoreApp.Data.EF.Repositories;
using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.Implementation;
//using BeCoreApp.Application.Dapper.Interfaces;
//using BeCoreApp.Application.Dapper.Implementation;
using Microsoft.AspNetCore.Authorization;
using BeCoreApp.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BeCoreApp.Web
{
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                o => o.MigrationsAssembly("BeCoreApp.Data.EF")));

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddMemoryCache();

            services.AddMinResponse();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(5);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(10);
                options.LoginPath = "/Admin/Account/Login";
                //options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddRecaptcha(new RecaptchaOptions()
            {
                SiteKey = Configuration["Recaptcha:SiteKey"],
                SecretKey = Configuration["Recaptcha:SecretKey"]
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(5);
                options.Cookie.HttpOnly = true;
            });
            services.AddImageResizer();
            services.AddAutoMapper();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
               {
                   options.AccessDeniedPath = new PathString("/Account/Access");
                   options.Cookie = new CookieBuilder
                   {
                       //Domain = "",
                       HttpOnly = true,
                       Name = ".aspNetCoreDemo.Security.Cookie",
                       Path = "/",
                       SameSite = SameSiteMode.Lax,
                       SecurePolicy = CookieSecurePolicy.SameAsRequest
                   };
                   options.Events = new CookieAuthenticationEvents
                   {
                       OnSignedIn = context =>
                       {
                           Console.WriteLine("{0} - {1}: {2}", DateTime.Now,
                               "OnSignedIn", context.Principal.Identity.Name);
                           return Task.CompletedTask;
                       },
                       OnSigningOut = context =>
                       {
                           Console.WriteLine("{0} - {1}: {2}", DateTime.Now,
                               "OnSigningOut", context.HttpContext.User.Identity.Name);
                           return Task.CompletedTask;
                       },
                       OnValidatePrincipal = context =>
                       {
                           Console.WriteLine("{0} - {1}: {2}", DateTime.Now,
                               "OnValidatePrincipal", context.Principal.Identity.Name);
                           return Task.CompletedTask;
                       }
                   };

                   options.ExpireTimeSpan = TimeSpan.FromDays(10);
                   options.LoginPath = new PathString("/Admin/Account/Login");
                   options.ReturnUrlParameter = "RequestPath";
                   options.SlidingExpiration = true;
               })
               .AddFacebook(facebookOpts =>
               {
                   facebookOpts.AppId = Configuration["Authentication:Facebook:AppId"];
                   facebookOpts.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
               })
               .AddGoogle(googleOpts =>
               {
                   googleOpts.ClientId = Configuration["Authentication:Google:ClientId"];
                   googleOpts.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
               });

            // Add application services.
            services.AddScoped<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddScoped<RoleManager<AppRole>, RoleManager<AppRole>>();

            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService));

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IViewRenderService, ViewRenderService>();

            services.AddTransient<DbInitializer>();

            services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, CustomClaimsPrincipalFactory>();

            services
                .AddMvc(options =>
                {
                    options.CacheProfiles.Add("Default", new CacheProfile() { Duration = 60 });
                    options.CacheProfiles.Add("Never", new CacheProfile() { Location = ResponseCacheLocation.None, NoStore = true });
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, opts => { opts.ResourcesPath = "Resources"; })
                .AddDataAnnotationsLocalization()
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

            services.Configure<RequestLocalizationOptions>(
             opts =>
             {
                 var supportedCultures = new List<CultureInfo>
                 {
                        new CultureInfo("en-US"),
                        new CultureInfo("vi-VN")
                 };

                 opts.DefaultRequestCulture = new RequestCulture("en-US");
                 // Formatting numbers, dates, etc.
                 opts.SupportedCultures = supportedCultures;
                 // UI strings that we have localized.
                 opts.SupportedUICultures = supportedCultures;
             });

            services.AddTransient(typeof(IUnitOfWork), typeof(EFUnitOfWork));
            services.AddTransient(typeof(IRepository<,>), typeof(EFRepository<,>));

            //Repository
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IFunctionRepository, FunctionRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<IProductTagRepository, ProductTagRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IBillRepository, BillRepository>();
            services.AddTransient<IBillDetailRepository, BillDetailRepository>();
            services.AddTransient<IColorRepository, ColorRepository>();
            services.AddTransient<ISizeRepository, SizeRepository>();
            services.AddTransient<IProductQuantityRepository, ProductQuantityRepository>();
            services.AddTransient<IProductImageRepository, ProductImageRepository>();
            services.AddTransient<IWholePriceRepository, WholePriceRepository>();
            services.AddTransient<IMenuGroupRepository, MenuGroupRepository>();
            services.AddTransient<IMenuItemRepository, MenuItemRepository>();
            services.AddTransient<IBlogCategoryRepository, BlogCategoryRepository>();
            services.AddTransient<IBlogRepository, BlogRepository>();
            services.AddTransient<IBlogTagRepository, BlogTagRepository>();
            services.AddTransient<ISlideRepository, SlideRepository>();
            services.AddTransient<ISystemConfigRepository, SystemConfigRepository>();
            services.AddTransient<IFooterRepository, FooterRepository>();
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IFeedbackRepository, FeedbackRepository>();
            services.AddTransient<IPageRepository, PageRepository>();
            services.AddTransient<IProvinceRepository, ProvinceRepository>();
            services.AddTransient<IDistrictRepository, DistrictRepository>();
            services.AddTransient<IWardRepository, WardRepository>();
            services.AddTransient<IStreetRepository, StreetRepository>();
            services.AddTransient<ITypeRepository, TypeRepository>();
            services.AddTransient<IUnitRepository, UnitRepository>();
            services.AddTransient<IClassifiedCategoryRepository, ClassifiedCategoryRepository>();
            services.AddTransient<IDirectionRepository, DirectionRepository>();
            services.AddTransient<IProjectCategoryRepository, ProjectCategoryRepository>();
            services.AddTransient<IEnterpriseRepository, EnterpriseRepository>();
            services.AddTransient<IFieldRepository, FieldRepository>();
            services.AddTransient<IEnterpriseFieldRepository, EnterpriseFieldRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<IProjectLibraryRepository, ProjectLibraryRepository>();
            services.AddTransient<IProjectImageRepository, ProjectImageRepository>();
            services.AddTransient<IProductTypeRepository, ProductTypeRepository>();
            services.AddTransient<IUserBonusRepository, UserBonusRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<IInvestRepository, InvestRepository>();
            services.AddTransient<IExchangeRepository, ExchangeRepository>();
            services.AddTransient<ISupportRepository, SupportRepository>();
            services.AddTransient<INotifyRepository, NotifyRepository>();
            services.AddTransient<ITransactionHistoryRepository, TransactionHistoryRepository>();
            services.AddTransient<IWalletTransferRepository, WalletTransferRepository>();
            services.AddTransient<ILuckyRoomRepository, LuckyRoomRepository>();
            services.AddTransient<IAppUserLuckyRoomRepository, AppUserLuckyRoomRepository>();

            //Service
            services.AddTransient<ILuckyRoomService, LuckyRoomService>();
            services.AddTransient<IAppUserLuckyRoomService, AppUserLuckyRoomService>();
            services.AddTransient<IWalletTransferService, WalletTransferService>();
            services.AddTransient<ITransactionHistoryService, TransactionHistoryService>();
            services.AddTransient<ITRONService, TRONService>();
            services.AddTransient<IHttpService, HttpService>();
            services.AddTransient<INotifyService, NotifyService>();
            services.AddTransient<ISupportService, SupportService>();
            services.AddTransient<IExchangeService, ExchangeService>();
            services.AddTransient<ISlideService, SlideService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<IBlockChainService, BlockChainService>();
            services.AddTransient<IUserBonusService, UserBonusService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IFunctionService, FunctionService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IBillService, BillService>();
            services.AddTransient<IMenuGroupService, MenuGroupService>();
            services.AddTransient<IMenuItemService, MenuItemService>();
            services.AddTransient<IBlogCategoryService, BlogCategoryService>();
            services.AddTransient<IBlogService, BlogService>();
            services.AddTransient<ICommonService, CommonService>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<IFeedbackService, FeedbackService>();
            services.AddTransient<IPageService, PageService>();
            //services.AddTransient<IReportService, ReportService>();
            services.AddTransient<IProvinceService, ProvinceService>();
            services.AddTransient<IDistrictService, DistrictService>();
            services.AddTransient<IWardService, WardService>();
            services.AddTransient<IStreetService, StreetService>();
            services.AddTransient<ITypeService, TypeService>();
            services.AddTransient<IUnitService, UnitService>();
            services.AddTransient<IClassifiedCategoryService, ClassifiedCategoryService>();
            services.AddTransient<IDirectionService, DirectionService>();
            services.AddTransient<IProjectCategoryService, ProjectCategoryService>();
            services.AddTransient<IEnterpriseService, EnterpriseService>();
            services.AddTransient<IFieldService, FieldService>();
            services.AddTransient<IEnterpriseFieldService, EnterpriseFieldService>();
            services.AddTransient<IProjectService, ProjectService>();

            services.AddTransient<IAuthorizationHandler, BaseResourceAuthorizationHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddFile("Logs/fbsbatdongsan-{Date}.txt");
            if (env.IsDevelopment())
            {
                //app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseImageResizer();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMinResponse();
            app.UseAuthentication();
            app.UseSession();

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(name: "areaRoute", template: "{area:exists}/{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
