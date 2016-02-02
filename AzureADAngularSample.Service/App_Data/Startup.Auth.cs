using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// The following using statements were added for this sample.
using Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Configuration;
using System.Globalization;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Owin.Security.ActiveDirectory;

namespace AzureADAngularSample.Service
{
	public partial class Startup
	{
		private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
		private static string appKey = ConfigurationManager.AppSettings["ida:AppKey"];
		private static string aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
		private static string tenant = ConfigurationManager.AppSettings["ida:Tenant"];
		//private static string postLogoutRedirectUri = ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];

		public static readonly string Authority = String.Format(CultureInfo.InvariantCulture, aadInstance, tenant);

		// For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
		public void ConfigureAuth(IAppBuilder app)
		{
			app.UseWindowsAzureActiveDirectoryBearerAuthentication(
				new WindowsAzureActiveDirectoryBearerAuthenticationOptions
				{
					Audience = ConfigurationManager.AppSettings["ida:Audience"],
					Tenant = ConfigurationManager.AppSettings["ida:Tenant"],
					
				});



			app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

			app.UseCookieAuthentication(new CookieAuthenticationOptions());

			app.UseOpenIdConnectAuthentication(
				new OpenIdConnectAuthenticationOptions
				{
					ClientId = clientId,
					Authority = Authority,
					//PostLogoutRedirectUri = postLogoutRedirectUri,

					//Notifications = new OpenIdConnectAuthenticationNotifications()
					//{
					//	//
					//	// If there is a code in the OpenID Connect response, redeem it for an access token and refresh token, and store those away.
					//	//
					//	AuthorizationCodeReceived = (context) =>
					//	{
					//		var code = context.Code;

					//		ClientCredential credential = new ClientCredential(clientId, appKey);
					//		string userObjectID = context.AuthenticationTicket.Identity.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
					//		//AuthenticationContext authContext = new AuthenticationContext(Authority, new NaiveSessionCache(userObjectID));
					//		AuthenticationContext authContext = new AuthenticationContext(Authority);
					//		//AuthenticationResult result = authContext.AcquireTokenByAuthorizationCode(code, new Uri(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path)), credential, graphResourceId);

					//		return Task.FromResult(0);
					//	},

					//	AuthenticationFailed = context =>
					//	{
					//		context.HandleResponse();
					//		context.Response.Redirect("/Home/Error?message=" + context.Exception.Message);
					//		return Task.FromResult(0);
					//	}

					//}

				});
		}
	}
}