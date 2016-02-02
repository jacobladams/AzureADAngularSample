using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;
using AzureADAngularSample.Models;
using Newtonsoft.Json;

namespace AzureADAngularSample.Controllers
{
    public class TestController : ApiController
    {
		[Route("ServiceTest")]
		[HttpGet]
		[Authorize]
		public List<string> ServiceTest()
		{
			string user = User.Identity.Name == string.Empty ? "Anonymous" : User.Identity.Name;
			return new List<string>
			{
				$"Embedded API - {user}"
			};
		}

		[Route("DoubleHopServiceTest")]
		[HttpGet]
		[Authorize]
		public async Task<List<string>> DoubleHopServiceTest()
		{
			//string json = new WebClient() { Credentials = CredentialCache.DefaultNetworkCredentials }.DownloadString("https://localhost:44301/ServiceTest");
			//List<string> messages = JsonConvert.DeserializeObject<List<String>>(json);
			List<string> messages = await new Service().CallServiceAsApp();
			string user = User.Identity.Name == string.Empty ? "Anonymous" : User.Identity.Name;
			return new List<string>
			{
				$"Embedded API - {user}"
			}.Concat(messages).ToList();

		}
	}
}
