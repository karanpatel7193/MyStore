using ECommerce.Api.Common;
using ECommerce.Business.Account;
using ECommerce.Business.Admin.Globalization;
using ECommerce.Business.Admin.Homepage;
using ECommerce.Business.Admin.Invoice.OrderInvoice;
using ECommerce.Business.Admin.Invoice.OrderInvoice.Invoice;
using ECommerce.Business.Admin.Master;
using ECommerce.Business.Admin.Vendor;
using ECommerce.Business.Client.Cart;
using ECommerce.Business.Client.Globalization;
using ECommerce.Business.Client.Home;
using ECommerce.Business.Client.Product;
using ECommerce.Business.Client.Review;
using ECommerce.Business.Client.Search;
using ECommerce.Business.Client.Wishlist;
using ECommerce.Business.Master;
using ECommerce.Entity.Admin.Globalization;
using ECommerce.Entity.Admin.Master;
using ECommerce.Entity.Client.Address;
using ECommerce.Entity.Client.Cart;
using ECommerce.Entity.Client.Globalization;
using ECommerce.Repository.Account;
using ECommerce.Repository.Admin.Globalization;
using ECommerce.Repository.Admin.Homepage;
using ECommerce.Repository.Admin.Invoice.OrderInvoice;
using ECommerce.Repository.Admin.Invoice.OrderInvoice.Invoice;
using ECommerce.Repository.Admin.Master;
using ECommerce.Repository.Admin.Master.CategoryProperty;
using ECommerce.Repository.Admin.Vendor;
using ECommerce.Repository.Client.Address;
using ECommerce.Repository.Client.Globalization;
using ECommerce.Repository.Client.Home;
using ECommerce.Repository.Client.Product;
using ECommerce.Repository.Client.Review;
using ECommerce.Repository.Client.Search;
using ECommerce.Repository.Client.Wishlist;
using ECommerce.Repository.Master;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
namespace ECommerce.Api
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression();

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Portfolio Api", Version = "v1"  });
            });

            //Add JWT token authentication with all setting.
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = Startup.GetValidationParameters();
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "forbidScheme";
                options.DefaultForbidScheme = "forbidScheme";
                options.AddScheme<AuthenticationSchemeHandle>("forbidScheme", "Handle Forbidden");
            });
            services.AddScoped<IMenuRepository, MenuBusiness>();
            services.AddScoped<IPmsRepository, PmsBusiness>();
            services.AddScoped<IRoleRepository, RoleBusiness>();
            services.AddScoped<IRoleMenuAccessRepository, RoleMenuAccessBusiness>();
            services.AddScoped<IUserReppository, UserBusiness>();
            services.AddScoped<IMasterRepositoroy, MasterBusiness>();
            services.AddScoped<IPropertyRepository, PropertyBusiness>();
            services.AddScoped<IProductRepositoroy, ProductBusiness>();
            services.AddScoped<ICategoryPropertyRepository, CategoryPropertyBusiness>();
            services.AddScoped<ICategoryRepository, CategoryBusiness>();
            services.AddScoped<ICustomerRepository, CustomerBusiness>();
            services.AddScoped<IVendorRepository, VendorBusiness>();
            services.AddScoped<IPurchaseOrderRepository, PurchaseOrderBusiness>();
            services.AddScoped<ICountryRepository, CountryBusiness>();
            services.AddScoped<IStateRepository, StateBusiness>();
            services.AddScoped<ICityRepository, CityBusiness>();
            services.AddScoped<IBlockRepository, BlockBusiness>();
            services.AddScoped<IPurchaseInvoiceRepository, PurchaseInvoiceBusiness>();
            services.AddScoped<IWishlistRepository, WishlistBusiness>();
            services.AddScoped<ISearchProductRepository, SearchProductMongoBusiness>();
            services.AddScoped<ISearchProductRepository, SearchProductSqlBusiness>();
            services.AddScoped<IHomeRepository, HomeBusiness>();
            services.AddScoped<IProductDetailsRepository, ProductDetailsSqlBusiness>();
            services.AddScoped<IProductDetailsRepository, ProductDetailsMongoBusiness>();
            services.AddScoped<IProductDetailsRepository, ProductDetailsRedisBusiness>();
            services.AddScoped<IReviewRepository, ReviewSqlBusiness>();
            services.AddScoped<ICartRepository, CartBusiness>();
            services.AddScoped<IAddressRepository, AddressBusiness>();
            services.AddScoped<ICountryRepositoryClient, CountryBusinessClient>();
            services.AddScoped<IStateRepositoryClient, StateBusinessClient>();
            services.AddScoped<ICityRepositoryClient, CityBusinessClient>();
            services.AddScoped<IEmployeeRepository, EmployeeBusiness>();
        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), AppSettings.PathDocumentUpload)),
            //    RequestPath = "/" + AppSettings.PathDocumentUpload + "/"
            //});
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"UploadedFiles")),
                RequestPath = "/UploadedFiles"
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(AppSettings.ApplicationSwagger, "Portfolio Api - v1");
            });

            app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.Run();
        }

        public static TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = AppSettings.SecurityTokenIssuer,
                ValidAudience = AppSettings.SecurityTokenKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.SecurityTokenKey))
            };
        }
    }
}
