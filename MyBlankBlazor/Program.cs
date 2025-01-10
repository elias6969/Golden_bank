using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBlankBlazor.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add RazorPages + Blazor
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<AccountState>();
builder.Services.AddHttpClient("PhpApi", client =>
{
    //Connects the client to the PHP API
    client.BaseAddress = new Uri("http://localhost/bank/");
});

var app = builder.Build();

// Ensures if there is an error we go to the error page
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Map the Blazor hub & fallback
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
