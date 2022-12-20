using Microsoft.EntityFrameworkCore;
using SimpleAppLoginAPI.Context;
using SimpleAppLoginAPI.Interface;
using SimpleAppLoginAPI.Models;
using SimpleAppLoginAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var db = new DbSettings();
builder.Configuration.Bind("AppConfig:DBSettings", db);
builder.Services.AddSingleton(db);
builder.Services.AddDbContext<AppDbContext>(option =>
    {
        option.UseSqlServer(
            db.ConnectionString,
            asm => asm.MigrationsAssembly("SimpleAppLoginAPI.Context.AppDbContext"));
   
    }, ServiceLifetime.Transient
);

builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider();
builder.Services.AddRazorPages();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(180);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

#region API Supporting Classes
builder.Services.AddScoped<IUsers, UserApi>();
builder.Services.AddScoped<ISignup, SignupApi>();
builder.Services.AddScoped<IEmployee, EmployeeApi>();
builder.Services.AddScoped<IProfile, ProfileApi>();
#endregion

builder.Services.AddControllers();
builder.Services.AddCors(option => 
{
    option.AddPolicy("MyPolicy", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();    
}
app.UseHttpsRedirection();

//app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowAll");
app.UseCookiePolicy();
app.UseSession();
app.UseCors("MyPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});
app.Run();
