using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace Backoffice.Controllers;

public class WidgetsController : Controller
{
		public IActionResult Index()
		{
				return View();
		}
}
