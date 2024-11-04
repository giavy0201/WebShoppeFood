using BLL.IService;
using BLL.Service;
using Microsoft.Extensions.FileProviders;
using WebMVC.Helper;
using WebMVC.Models.Payments;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddLogging();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
builder.Services.Configure<VnPaySettings>(builder.Configuration.GetSection("VNPAY"));


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
var app = builder.Build();
Console.WriteLine($"Environment: {app.Environment.EnvironmentName}");
//var ipAddress = Utils.GetIpAddress((HttpContext)app.Services.GetRequiredService<IHttpContextAccessor>());

// Configure the HTTP request pipeline.
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/PageNotFound");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}
app.Use(async (ctx, next) =>
{
    await next();
    if (ctx.Response.StatusCode == 404 && !ctx.Response.HasStarted)
    {
        ctx.Request.Path = "/NotFound";
        await next();
    }
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine("C:", "Users", "Vy", "OneDrive","Desktop", "WebShoppeFood", "ImgShoppe", "ImageShoppeFood", "ListStore")),
    RequestPath = "/image"
});
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine("C:","Users","Vy", "OneDrive", "Desktop", "WebShoppeFood", "ImgShoppe", "ImageShoppeFood", "ImgWeb")),
    RequestPath = "/imgWeb"
});
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine("C:", "Users", "Vy", "OneDrive", "Desktop", "WebShoppeFood", "ImgShoppe", "ImageShoppeFood", "collections")),
    RequestPath = "/imgCollection"
});
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
