using Microsoft.EntityFrameworkCore;
using ProductSaling.Core;
using ProductSaling.Core.Services;
using ProductSaling.Core.UnitOfWork.Abstract;
using ProductSaling.Core.UnitOfWork.Concrete;
using ProductSaling.Data;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//public Startup(IConfiguration configuration)
//{
//    Configuration = configuration;
//}
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//public IConfiguration Configuration { get; }
builder.Services.AddDbContext<ApplicationDbContext>(conf =>
{
    conf.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddMemoryCache();

builder.Services.ConfigureRateLimiting();
builder.Services.AddHttpContextAccessor();

builder.Services.ConfigureHttpCacheHeaders();

builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
//builder.Services.ConfigureJWT(Configuration);

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthManager, AuthManager>();

builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});
builder.Services.ConfigureAutoMapper();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.ConfigureSwaggerDoc();

builder.Services.AddControllers(/*config => {
                config.CacheProfiles.Add("120SecondsDuration", new CacheProfile
                {
                    Duration = 120

                });
            }*/).AddNewtonsoftJson(op =>
            op.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.ConfigureVersioning();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseResponseCaching();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
