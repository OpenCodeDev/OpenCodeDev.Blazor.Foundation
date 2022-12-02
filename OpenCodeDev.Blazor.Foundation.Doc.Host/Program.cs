using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OpenCodeDev.Blazor.Foundation;
using OpenCodeDev.Blazor.Foundation.Extensions;
using OpenCodeDev.Blazor.Foundation.Doc.Core.Plugins.CSS;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.StyleManager;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//builder.Services.AddServerSideBlazor();
builder.Services.AddLocalization();
builder.Services.AddScoped(sp => new HttpClient { });
builder.Services.AddBlazorFoundationServices(false);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseBlazorFrameworkFiles();
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllers();
    endpoints.MapFallbackToPage("/_Host");
});
app.Run();
