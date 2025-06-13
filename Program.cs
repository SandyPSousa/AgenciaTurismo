using Microsoft.EntityFrameworkCore;
using AgenciaTurismo.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AgenciaTurismoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string not found.")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
    });

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Clientes");
    options.Conventions.AuthorizeFolder("/Cidades");
    options.Conventions.AuthorizeFolder("/Paises");
    options.Conventions.AuthorizeFolder("/Pacotes");
    options.Conventions.AuthorizeFolder("/Reservas");
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();   
app.UseRouting();      

app.UseAuthentication();  
app.UseAuthorization();  

app.MapRazorPages();  
app.Run();