using Microsoft.EntityFrameworkCore;
using SeuProjeto.Data;
using TheRealBank.Repositories;
using TheRealBank.Services;
using TheRealBank.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Registra EF Core (MainContext) e Repositórios
TheRealBank.Repositories.ExtensionMethods.AddDesignerRepositories(builder.Services, builder.Configuration);

// Registra serviços de aplicação
builder.Services.AddApplicationServices();

builder.Services.AddRazorPages();

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
app.UseAuthorization();
app.MapRazorPages();
app.Run();
