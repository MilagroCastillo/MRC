

//importar los espacios de nombres necesarios para el proyecto
using CRM.API.Endpoints;
using CRM.API.Models.DAL;
using Microsoft.EntityFrameworkCore;

//crear un nuevo constructor de la aplicacion web
var builder = WebApplication.CreateBuilder(args);

// agregar servicios para habilitar la generacion de documentacion de API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//configura y agrega un contexto de base de datos para Entity Framework Core
builder.Services.AddDbContext<CRMContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Conn"))
);

// agregar una instancia de la clase CustomerDAL como un servicio para la inyeccion de depedencias
builder.Services.AddScoped<CustomerDAL>();
// construye la applicacion web
var app = builder.Build();
//agegar los puntos finales relacionados con los clientes a la aplicacion
app.AddCustomerEndpoints();
//verificar si la aplicacion se esta ejecutando en un entorno desarrollado
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// agregar middleware para redirigir las solicitudes HTTP a HTTPS
app.UseHttpsRedirection();
// EJECUTA LA APLICACION
app.Run();