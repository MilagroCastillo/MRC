var builder = WebApplication.CreateBuilder(args);//crea un constructor de aplicaciones web

// Agrega servicios al contenedor de depedencias
builder.Services.AddControllersWithViews();//Agrega servicios para controladores y vistas

//configura y agrega un cliente HTTP con nombre "CRMAPI"
builder.Services.AddHttpClient("CRMAPI", c =>
{
    //Configura la direccion base del cliente HTTP desde la configuracion 
    c.BaseAddress = new Uri(builder.Configuration["UrlsAPI:CRM"]);

});

var app = builder.Build();// crea una instancia de la aplicacion web

// configura el pipeline de solicitudes HTTP.
if (!app.Environment.IsDevelopment())
{
    //maneja excepciones en caso de errore y redirige a la accion "Error"en el controlador "Home"
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();//redirege las solicitudes HTTP a HTTP
app.UseStaticFiles();//Habilita el uso de archivos estaticos como CSS, Javascript, imagenes

app.UseRouting();//configura el enrutamiento de solicitudes

app.UseAuthorization();//habilita la autorizacion para proteger rutas y acciones de controladores

//mapea la ruta predeterminada de controlador y accion 
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();//inicia la aplicacion web
