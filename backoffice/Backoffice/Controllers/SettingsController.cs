using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Backoffice.Controllers;

public class SettingsController : Controller
{
		public IActionResult Index()
		{
				return View();
		}
}