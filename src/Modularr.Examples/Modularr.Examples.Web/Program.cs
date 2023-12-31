using Modularr.Examples.HelloWorld;
using Modularr.MultiTenancy.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddModularr(builder =>
{
    builder.AddModule<HelloWorldModule>();
}).WithMultiTenancy();

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

app.UseAuthorization();

app.UseModularr();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
