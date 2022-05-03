# MixedLegacySolution

This solution represent a sandbox to try authentication based on IdentityServer in an 
environment with different applications.

The main goal is to integrate *legacy* application based on .NET Framework with new 
applications based on .NET 6 sharing authentication and authorization processes.

The choice was to use IndentityServer4 since it's the framework choosen by Microsoft in
it's project templates.

> NOTE: IdentityServer4 is facing its end of life period, but given that version five is
> not free (at least not for all use cases) I choose to base this solution on the fourth version.

This project is a **work in progress** and incomplete. It will be expanded with new parts and 
functionality to cover as many scenarios as possibile.

## Solution

The solution consist of the following VS projects:
- **Shared** based on .NET Standard 2.0 allow to share base class betweens projects based on Framework and Core.   
- **IdentityServer** (debug on [localhost:7046](https://localhost:7046/)) based on ASP.NET Core 6 implements identity server.   
- **OldApp** (debug on [localhost:44360](https://localhost:44360/)) based on .NET Framework 4.7.2, 
  include also areas of the applicaiton managed via AngularJS 1.5.9 framework.
- **AngularApp** (debug on [localhost:4200](https://localhost:4200))  

### MSL.Shared

The Shared project contains structures used by 2 or more projects to avoid code duplication.

### MSL.IdentityServer

*IdentiyServer* project use [IdentityServer4](https://identityserver4.readthedocs.io/en/latest/) to expose authentication and authorization 
functionality via OpenId / OAuth2.

The configuration of the server is at the momento completly managed in memory via the `Config` class.

The application expose the [openid-configuration page](https://localhost:7046/.well-known/openid-configuration) to document 
the configuration and how the service could be used by clients.

To enable UI the template could be appied. (ref. [IdentityServer4.Quickstart.UI](https://github.com/IdentityServer/IdentityServer4.Quickstart.UI)).
```
dotnet new -i identityserver4.templates
dotnet new is4ui
```

### MSL.OldApp (.NET Framework 4.7.2)

This web application replicate a legacy solution on wich I worked that include in one single 
ASP.NET application different parts:

- MVC standard controllers and views (public and private one).
- API controllers, to implement REST functionality eventually available also for external applications.
- An area managed by *AngularJs* application that access to API functionality.

> In this application the version 3 of IdentityServer was used since it's the last compatible with .NET Framework 
> regarding [IdentityServer3.AccessTokenValidation](https://github.com/identityserver/IdentityServer3.AccessTokenValidation).

In the `Startup` class both cookie authentication (via OpenId `app.UseOpenIdConnectAuthentication`)
and JWT authentication (`app.UseIdentityServerBearerTokenAuthentication`) are enabled.

Cors it's also enabled on API services using [`Microsoft.AspNet.WebApi.Cors.5.2.7`](https://www.nuget.org/packages/Microsoft.AspNet.WebApi.Cors/)
package (view `WebApiConfig.cs`).

#### AngularJS application authentication

Unlike MSL.AngularApp the part of the site managed by AngularJs doesn't configure 
specific authentication since the authentication token is common for the entire site.

#### Reference material

- [IdentityServer4 and ASP.NET MVC](https://nahidfa.com/posts/identityserver4-and-asp-.net-mvc/)  (by Nahid Farrokhi)
  Describe how to to configure cookie authorization for ASP.NET MVC Applications

### MSL.AngularApp

The angular app make use of [oidc-client](https://www.npmjs.com/package/oidc-client) npm package.

[Here](https://github.com/IdentityModel/oidc-client-js/wiki) you can find documentation of the package.

This app use API controller in *OldApp* to retrieve data  passing JWT in request header.

#### Reference material

- [SPA Authentication using OpenID Connect, Angular CLI and oidc-client](https://www.scottbrady91.com/angular/spa-authentiction-using-openid-connect-angular-cli-and-oidc-client)   
  a step by step guide to implement authentication in Angular
- [Implementing User Authentication in Angular using IdentityServer4](https://referbruv.com/blog/posts/implementing-user-authentication-in-angular-using-identityserver4)

## Other Resources

- [IndentityServer4 (github)](https://github.com/IdentityServer/IdentityServer4)
- [Using IdentityServer4 with .NET Framework](https://alex-driver.medium.com/using-identity-server-4-with-net-framework-fb0b4536d27b)   
  Describes integration between .NET Framework Web API and IdentityServer4
- [IdentityServer4 UI and Web API Basic Security](https://code-maze.com/identityserver4-ui-webapi-basic-security/)
- [IdentityServer4.Quickstart.UI](https://github.com/IdentityServer/IdentityServer4.Quickstart.UI)
- [Share authentication cookies among ASP.NET apps](https://docs.microsoft.com/en-us/aspnet/core/security/cookie-sharing?view=aspnetcore-6.0) (Microsoft Docs)   
  describe using cookie authentication in a shared environment
- [Use Bearer Token Authentication for API and OpenId authentication for MVC on the same application project](https://stackoverflow.com/questions/30881720/use-bearer-token-authentication-for-api-and-openid-authentication-for-mvc-on-the)

## TODO

- Make Angular app run on HTTPS.
- Implement redirect in Angular app after authentication
- Use configuration to load openid settings in clients
- Use Entity Framework (Windows Identity?) to manage clients, scopes, resources and users
- Implement Silet Refresh in Angular App as described in [Silent Refresh - Refreshing Access Tokens when using the Implicit Flow](https://www.scottbrady91.com/openid-connect/silent-refresh-refreshing-access-tokens-when-using-the-implicit-flow)
