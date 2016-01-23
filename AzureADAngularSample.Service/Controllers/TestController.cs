using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Routing;

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
				$"Service API - {user}"
			};
		}
	}
}
