using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace Backoffice.Controllers;

public class AnalyticsController : Controller
{
		public IActionResult Index()
		{
				return View();
		}
}