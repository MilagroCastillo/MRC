
using CRM.AppWebBlazor.Data;

var builder = WebApplication.CreateBuilder(args);

//agregar servicios al contenedor de dependenciaa
builder.Services.AddRazorPages(); //agregar suporte para paginas Razor
builder.Services.AddServerSideBlazor(); // agregar soporte para Blazor en el lado del servidor

//registrar CustomerService como un servicio singlen (una instancia unica para toda la aplicacion)
builder.Services.AddSingleton<CustomerService>();

//configurar y agregar un cliente HTTP con nombre "CRMAPI"
builder.Services.AddHttpClient("CRMAPI", c =>
{
    //configura la direccion base del cliente HTTP desde la configuracion 
    c.BaseAddress = new Uri(builder.Configuration["UrlsAPI:CRM"]);

});

var app = builder.Build(); //crear una instancia de la aplicacion web

//configura el pipeline de solicitudes HTTP 
if(!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error"); //maneja excepciones en caso de errores
    // el valor HSTS predeterminado es de 30 dias. Puedes cambiarlo para escenarios de produccion
}
app.UseHttpsRedirection();//redireje la solicitud HTTP a Htttps
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();

