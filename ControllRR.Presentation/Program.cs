using ControllRR.Application.Interfaces;
using ControllRR.Application.Services;
using ControllRR.Domain.Interfaces;
using ControllRR.Infrastructure.Data.Context;
using ControllRR.Infrastructure.Data.Seeding;
using ControllRR.Infrastructure.Repositories;
using ControllRR.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using Microsoft.AspNetCore.Identity;
using ControllRR.Domain.Entities;
var builder = WebApplication.CreateBuilder(args);

// Adicionar o DbContext com MySQL
builder.Services.AddEntityFrameworkMySQL()
    .AddDbContext<ControllRRContext>(options =>
    {
        options.UseMySQL(builder.Configuration.GetConnectionString("ControlContext"));
    });

// Configurar o Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ControllRRContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

// Configurar AutoMapper
builder.Services.AddAutoMapper(typeof(MaintenanceMappingProfile));
builder.Services.AddAutoMapper(typeof(DeviceMappingProfile));
builder.Services.AddAutoMapper(typeof(SectorMappingProfile));
builder.Services.AddAutoMapper(typeof(DocumentMappingProfile));
builder.Services.AddAutoMapper(typeof(ApplicationUserMappingProfile));
builder.Services.AddScoped<SignInManager<ApplicationUser>>();
// Registrar serviços
builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMaintenanceService, MaintenanceService>();
builder.Services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<IFileStorageService, FileStorageService>();
builder.Services.AddScoped<ISectorService, SectorService>();
builder.Services.AddScoped<ISectorRepository, SectorRepository>();
builder.Services.AddScoped<IApplicationUserService, ApplicationUserService>();

// Adicionar suporte ao MVC e Razor Pages
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedingService.Initialize(services);
     SeedUser.InitializeAsync(services);
   
  var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "Manager", "Member" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();//ADD THIS LINE
});
app.Run();