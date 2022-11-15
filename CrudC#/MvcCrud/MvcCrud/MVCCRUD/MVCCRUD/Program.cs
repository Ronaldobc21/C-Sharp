using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MVCCRUD.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Scafold-DbContext "server=localhost; database=MVCCRUD; integrated security=true;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models
//Arriba es el comando con el que creas en el models 
builder.Services.AddDbContext<UsuariosContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("conexion")));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
