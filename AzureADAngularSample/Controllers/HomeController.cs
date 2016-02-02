using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AzureADAngularSample.Models;
using Newtonsoft.Json;

namespace AzureADAngularSample.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
		[Authorize]
        public async Task<ActionResult> Index()
        {
			//string json = new WebClient() {Credentials = CredentialCache.DefaultNetworkCredentials}.DownloadString("https://localhost:44301/ServiceTest");
			//List<string> messages = JsonConvert.DeserializeObject<List<String>>(json);

			//List<string> messages = await new Service().CallServiceAsApp();
			

			dynamic model = new ExpandoObject();
	        model.MVC = User.Identity.Name;
			//model.Service = messages[0];
	        model.Service = "TBD";


			return View(model);
        }
    }
}