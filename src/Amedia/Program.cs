using Amedia.DataAccess;
using Microsoft.EntityFrameworkCore;
using static System.Formats.Asn1.AsnWriter;
using System.Net.NetworkInformation;
using System;
using Amedia.Logic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(s => s.IdleTimeout = TimeSpan.FromMinutes(30));
builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<AuthLogic>();
builder.Services.AddTransient<UsersLogic>();

builder.Services.AddDbContext<AmediaContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("AmediaContext")));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AmediaContext>();

    context.Database.EnsureCreated();
    //context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.Run();
