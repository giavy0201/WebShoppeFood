using BLL.Helpers;
using BLL.IService;
using BLL.Models.DTOs.Cart;
using BLL.Service;
using DAL;
using DAL.Entities;
using DAL.Non_Repository.AddressRepo;
using DAL.Non_Repository.CartRepo;
using DAL.Non_Repository.CollectionRepo;
using DAL.Non_Repository.CommentRepo;
using DAL.Non_Repository.MenuRepo;
using DAL.Non_Repository.ProductRepo;
using DAL.Non_Repository.StoreRepo;
using DAL.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using WebAPI.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddIdentity<Customer, IdentityRole>()
                    .AddEntityFrameworkStores<DataContext>()
                    .AddDefaultTokenProviders();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ConnectDB"));
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});
//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    string connection = builder.Configuration.GetConnectionString("Redis");
//    options.Configuration = connection;
//});

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IAddressRepository), typeof(AddressRepository));
builder.Services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
builder.Services.AddScoped(typeof(IStoreRepository), typeof(StoreRepository));
builder.Services.AddScoped(typeof(IMenuRepository), typeof(MenuRepository));
builder.Services.AddScoped(typeof(ICollectionRepository), typeof(CollectionRepository));
builder.Services.AddScoped(typeof(ICartRepository), typeof(CartRepository));
builder.Services.AddScoped(typeof(ICommentRepository), typeof(CommentRepository));


builder.Services.AddTransient<IAddressService, AddressService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IStoreService, StoreService>();
builder.Services.AddTransient<IMenuService, MenuService>();
builder.Services.AddTransient<ICollectionService, CollectionService>();
builder.Services.AddTransient<IAccountCustomer, AccountCustomer>();
builder.Services.AddTransient<ICartService, CartService>();
builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigurationSwagger>();
builder.Services.AddSingleton<ICacheService, CacheService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
builder.Services.AddSingleton<CartDtos>();

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.Configure<ApiBehaviorOptions>(options
    => options.SuppressModelStateInvalidFilter = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
