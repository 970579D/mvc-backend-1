using Microsoft.EntityFrameworkCore;
using mvc_backend_1.Context;
using mvc_backend_1.Service;

var crosName = "_cros";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ReserveService, ReserveServiceImpl>();
builder.Services.AddTransient<ReserveService, ReserveServiceImpl>();

builder.Services.AddDbContext<ReserveContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: crosName,
                      builder =>
                      {
                          builder
                            .WithOrigins("http://localhost:4200")
                            .WithMethods("GET", "POST", "PUT", "DELETE")
                            .AllowAnyHeader();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors(crosName);
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
