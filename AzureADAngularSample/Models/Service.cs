using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using AzureADAngularSample.Utils;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;

namespace AzureADAngularSample.Models
{
	public class Service
	{
		private string todoListResourceId = ConfigurationManager.AppSettings["ServiceResourceId"];
		private string todoListBaseAddress = ConfigurationManager.AppSettings["ServiceUrl"];
		private const string TenantIdClaimType = "http://schemas.microsoft.com/identity/claims/tenantid";
		private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
		private static string appKey = ConfigurationManager.AppSettings["ida:AppKey"];

		public async Task<List<string>> CallService()
		{
			string userObjectID = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;

			AuthenticationContext authContext = new AuthenticationContext(Startup.Authority, new NaiveSessionCache(userObjectID));
			//AuthenticationContext authContext = new AuthenticationContext(Startup.Authority);
			ClientCredential credential = new ClientCredential(clientId, appKey);
			AuthenticationResult result = authContext.AcquireTokenSilent(todoListResourceId, credential, new UserIdentifier(userObjectID, UserIdentifierType.UniqueId));
			//AuthenticationResult result = authContext.AcquireToken(todoListResourceId, credential,) new UserIdentifier(userObjectID, UserIdentifierType.UniqueId));
			//AuthenticationResult result = authContext.AcquireTokenByAuthorizationCode(code, new Uri(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path)), credential);
			
			//
			// Retrieve the user's To Do List.
			//
			HttpClient client = new HttpClient();
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, todoListBaseAddress + "/ServiceTest");
			request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
			HttpResponseMessage response = await client.SendAsync(request);

			//
			// Return the To Do List in the view.
			//
			if (response.IsSuccessStatusCode)
			{
				//List<Dictionary<String, String>> responseElements = new List<Dictionary<String, String>>();
				JsonSerializerSettings settings = new JsonSerializerSettings();
				String responseString = await response.Content.ReadAsStringAsync();

				//responseElements = JsonConvert.DeserializeObject<List<String, String>>(responseString, settings);
				List<string> messages = JsonConvert.DeserializeObject<List<String>>(responseString, settings);

				//foreach (Dictionary<String, String> responseElement in responseElements)
				//{
				//	TodoItem newItem = new TodoItem();
				//	newItem.Title = responseElement["Title"];
				//	newItem.Owner = responseElement["Owner"];
				//	itemList.Add(newItem);
				//}
				return messages;
			}
			else
			{
				throw new UnauthorizedAccessException();
			}
		}

		public async Task<List<string>> CallServiceAsApp()
		{
			string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
			string appKey = ConfigurationManager.AppSettings["ida:AppKey"];
			string aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
			string tenant = ConfigurationManager.AppSettings["ida:Tenant"];
		//private static string postLogoutRedirectUri = ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];



			string authority = String.Format(CultureInfo.InvariantCulture, aadInstance, tenant);
			 AuthenticationContext authContext = new AuthenticationContext(authority);
			ClientCredential clientCredential = new ClientCredential(clientId, appKey);
			AuthenticationResult result = authContext.AcquireToken(todoListResourceId, clientCredential);

			string userObjectID = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;

			HttpClient client = new HttpClient();
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, todoListBaseAddress + "/ServiceTest");
			request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
			HttpResponseMessage response = await client.SendAsync(request);




			//
			// Return the To Do List in the view.
			//
			if (response.IsSuccessStatusCode)
			{
				//List<Dictionary<String, String>> responseElements = new List<Dictionary<String, String>>();
				JsonSerializerSettings settings = new JsonSerializerSettings();
				String responseString = await response.Content.ReadAsStringAsync();

				//responseElements = JsonConvert.DeserializeObject<List<String, String>>(responseString, settings);
				List<string> messages = JsonConvert.DeserializeObject<List<String>>(responseString, settings);

				//foreach (Dictionary<String, String> responseElement in responseElements)
				//{
				//	TodoItem newItem = new TodoItem();
				//	newItem.Title = responseElement["Title"];
				//	newItem.Owner = responseElement["Owner"];
				//	itemList.Add(newItem);
				//}
				return messages;
			}
			else
			{
				throw new UnauthorizedAccessException();
			}
		}
	}
}