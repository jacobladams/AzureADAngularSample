using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Routing;
using Newtonsoft.Json;

namespace AzureADAngularSample.Controllers
{
    public class TestController : ApiController
    {
		[Route("ServiceTest")]
		[HttpGet]
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
		public List<string> DoubleHopServiceTest()
		{
			string json = new WebClient() { Credentials = CredentialCache.DefaultNetworkCredentials }.DownloadString("https://jakeazureadtestservice.azurewebsites.net/ServiceTest");
			List<string> messages = JsonConvert.DeserializeObject<List<String>>(json);
			string user = User.Identity.Name == string.Empty ? "Anonymous" : User.Identity.Name;
			return new List<string>
			{
				$"Embedded API - {user}"
			}.Concat(messages).ToList();

		}
	}
}
