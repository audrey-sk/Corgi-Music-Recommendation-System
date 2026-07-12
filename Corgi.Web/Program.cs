using Corgi.Data;
using Corgi.Infrastructure;
using Corgi.Web.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCorgiInfrastructure(builder.Configuration);
builder.Services.AddCorgiData(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Dev convenience: make sure the SQLite file/schema exists.
// Swap for real migrations before this goes anywhere near production.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CorgiDbContext>();
    db.Database.EnsureCreated();
}

app.Run();
