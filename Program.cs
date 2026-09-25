using IPC2_Proy02_202602_202503481.Estructuras;
using IPC2_Proy02_202602_202503481.Datos;
using IPC2_Proy02_202602_202503481.Servicios;
using IPC2_Proy02_202602_202503481.Archivos;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<Catalogo>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();