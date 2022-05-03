using System.Web.Http;
using System.Web.Http.Cors;
using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;

[assembly: OwinStartup(typeof(MLS.OldApp.Startup))]
namespace MLS.OldApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var corsOrigins = "http://localhost:4200";
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });


            var openIdOptions = new OpenIdConnectAuthenticationOptions {
                Authority = MlsEnvironment.IdentityServerInfo.Uri,
                ClientId = MlsEnvironment.LegacyAppInfo.ClientId,
                ClientSecret = MlsEnvironment.LegacyAppInfo.ClientId,
                RedirectUri = "https://localhost:44360/signin-oidc",
                PostLogoutRedirectUri = "https://localhost:44360",
                ResponseType = "id_token token",
                // RequireHttpsMetadata = false,
                TokenValidationParameters = new System.IdentityModel.Tokens.TokenValidationParameters { NameClaimType = "name" }, // this is to set identity name
                SignInAsAuthenticationType = "Cookies",
                //Notifications = new OpenIdConnectAuthenticationNotifications
                //{
                //    AuthorizationCodeReceived = AuthorizationReceived
                //}
            };
            
            app.UseOpenIdConnectAuthentication(openIdOptions);

            
            
            var bearerTokenOptions = new IdentityServerBearerTokenAuthenticationOptions
             {
                 Authority = MlsEnvironment.IdentityServerInfo.Uri,
                 RequiredScopes = new[] { "myApiScope", "openid", "profile" },
                 ValidationMode = ValidationMode.Local,
                 ClientId = MlsEnvironment.LegacyAppInfo.ClientId,
                 ClientSecret = MlsEnvironment.LegacyAppInfo.ClientSecret
             };

             app.UseIdentityServerBearerTokenAuthentication(bearerTokenOptions);
        }
    }
}