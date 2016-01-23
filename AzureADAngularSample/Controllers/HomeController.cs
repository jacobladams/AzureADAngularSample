using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace AzureADAngularSample.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
			string json = new WebClient() {Credentials = CredentialCache.DefaultNetworkCredentials}.DownloadString("https://jakeazureadtestservice.azurewebsites.net/ServiceTest");
			List<string> messages = JsonConvert.DeserializeObject<List<String>>(json);


			dynamic model = new ExpandoObject();
	        model.MVC = User.Identity.Name;
	        model.Service = messages[0];
	        //model.Service = "TBD";


			return View(model);
        }
    }
}