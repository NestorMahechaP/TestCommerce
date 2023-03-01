using Clients.WebClient.Models;
using Clients.WebClient.Proxies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ApiUrls>(builder.Configuration.GetSection("ApiUrls"));
builder.Services.AddHttpClient<ICatalogProxy, CatalogProxy>();
builder.Services.AddHttpClient<ICustomerProxy, CustomerProxy>();
builder.Services.AddHttpClient<IOrderProxy, OrderProxy>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
