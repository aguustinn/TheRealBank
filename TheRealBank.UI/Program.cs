using Microsoft.EntityFrameworkCore;
using TheRealBank.Repositories;
using TheRealBank.Services;
using TheRealBank.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Registra EF Core (MainContext) e Repositórios
TheRealBank.Repositories.ExtensionMethods.AddDesignerRepositories(builder.Services, builder.Configuration);

// Registra serviços de aplicação
builder.Services.AddApplicationServices();

// Razor Pages + Authorization conventions for Admin-only access
builder.Services.AddRazorPages(options =>
{
    // Área do Cliente: somente autenticados
    options.Conventions.AuthorizePage("/Experiencia/Layout");

    // Pasta Customers: somente Admin por padrão
    options.Conventions.AuthorizeFolder("/Customers", "AdminOnly");

    // EXCEÇÃO: liberar AddCliente para qualquer um (anônimo)
    options.Conventions.AllowAnonymousToPage("/Customers/AddCliente");
});

// Auth Cookie + Policy de Admin
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o =>
    {
        o.LoginPath = "/Autentifica/Auth";
        o.AccessDeniedPath = "/Autentifica/AuthAdm";
        o.Cookie.Name = "TheRealBank.Auth";
    });

builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("AdminOnly", p => p.RequireRole("Admin"));
});

var app = builder.Build();

// Aplica migrações automaticamente (cria DB/tabelas se necessário)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MainContext>();
    db.Database.Migrate();
}

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
