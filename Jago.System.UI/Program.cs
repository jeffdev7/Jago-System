using AutoMapper;
using Jago.Application.AutoMapper;
using Jago.Application.Services;
using Jago.domain.Interfaces.Repositories;
using Jago.Infrastructure.DBConfiguration;
using Jago.Infrastructure.Repositories;
using Jago.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IPassengerServices, PassengerServices>();
builder.Services.AddScoped<ITripServices, TripServices>();

builder.Services.AddScoped<IPassengerRepository, PassengerRepository>();
builder.Services.AddScoped<ITripRepository, TripRepository>();

builder.Services.AddAutoMapper(typeof(DomainVMMapping), typeof(VMDomainMapping));

builder.Services.AddDbContext<ApplicationContext>();


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
