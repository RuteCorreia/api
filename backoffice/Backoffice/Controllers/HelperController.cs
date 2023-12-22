using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Backoffice.Controllers;

public class HelperController : Controller
{
		public IActionResult Index()
		{
				return View();
		}
}