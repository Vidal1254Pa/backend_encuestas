using Encuestadora.Backend.Application.Configuracion;
using Encuestadora.Backend.Application.Requerimiento;
using Encuestadora.Backend.Domain.Configuracion.Interfaces;
using Encuestadora.Backend.Domain.Requerimiento.Interfaces;
using Encuestadora.Backend.Infraestructure;
using Encuestadora.Backend.Infraestructure.Configuracion;
using Encuestadora.Backend.Infraestructure.Requerimiento;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using NLog.Web;

string AllAllowSpecificOrigins = "_AllAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.local.json", true, true);

// Add services to the container.
//builder.Services.ConfigureJWT(true);

//START::AddAuthentication.cs
builder.Services.AddAuthenticationCore();
builder.Services.AddDataProtection();
builder.Services.AddWebEncoders();
builder.Services.TryAddSingleton<ISystemClock, SystemClock>();
//END::AddAuthentication.cs

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("*")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

    c.TagActionsBy(api =>
    {
        if (api.GroupName != null)
        {
            return new[] { api.GroupName };
        }

        var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
        if (controllerActionDescriptor != null)
        {
            return new[] { controllerActionDescriptor.ControllerName };
        }

        throw new InvalidOperationException("Unable to determine tag for endpoint.");
    });
    c.DocInclusionPredicate((name, api) => true);
});

builder.Services.AddScoped<ICustomConnection, CustomConnection>();

////////////// SERIVCES ///////////////
///
builder.Services.AddTransient<CategoriaApp>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddTransient<EscalaApp>();
builder.Services.AddScoped<IEscalaRepository, EscalaRepository>();
builder.Services.AddTransient<ProvinciaApp>();
builder.Services.AddScoped<IProvinciaRepository, ProvinciaRepository>();
builder.Services.AddTransient<CiudadApp>();
builder.Services.AddScoped<ICiudadRepository, CiudadRepository>();
builder.Services.AddTransient<SucursalApp>();
builder.Services.AddScoped<ISucursalRepository, SucursalRepository>();
builder.Services.AddTransient<EncuestaApp>();
builder.Services.AddScoped<IEncuestaRepository, EncuestaRepository>();
builder.Services.AddTransient<EncuestadoApp>();
builder.Services.AddScoped<IEncuestadoRepository, EncuestadoRepository>();
builder.Services.AddTransient<EncuestaRequerimientoApp>();
builder.Services.AddScoped<IEncuestaRequerimientoRepository, RequerimientoRepository>();


builder.Host.UseNLog();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(AllAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
