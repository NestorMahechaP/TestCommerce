using Api.Gateway.Proxies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ApiUrls>(builder.Configuration.GetSection("ApiUrls"));
builder.Services.AddHttpClient<ICatalogProxy, CatalogProxy>();
builder.Services.AddHttpClient<ICustomerProxy, CustomerProxy>();
builder.Services.AddHttpClient<IOrderProxy, OrderProxy>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
