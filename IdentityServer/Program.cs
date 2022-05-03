using Microsoft.IdentityModel.Logging;
using MSL.IdentityServer;


var builder = WebApplication.CreateBuilder(args);



var services = builder.Services;
IdentityModelEventSource.ShowPII = true;  
var identityServerBuilder  = services.AddIdentityServer(options =>
{
    // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
    // needed for older access token validation plumbing
    options.EmitStaticAudienceClaim = true;
    options.AccessTokenJwtType = "JWT";
})
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryClients(Config.Clients)
    .AddTestUsers(Config.TestUsers);
identityServerBuilder.AddDeveloperSigningCredential();

services.AddControllersWithViews();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());



app.Run();
