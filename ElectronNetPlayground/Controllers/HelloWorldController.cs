using System.Linq;
using ElectronNET.API;
using ElectronNetPlayground.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ElectronNetPlayground.Controllers
{
	[Route("api/hello-world")]
	public class HelloWorldController : Controller
	{
		#region Properties


		#endregion

		#region Constructor

		public HelloWorldController()
		{
		}

		#endregion

		#region Methods

		[HttpGet]
		public IActionResult Greet()
		{
			var model = new HelloWorldViewModel();
			model.Title = "This is hello world title";
			model.Content = "This is hello world content";
			return Ok(model);
		}

		#endregion
	}
}