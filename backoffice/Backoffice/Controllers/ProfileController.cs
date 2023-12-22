using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Backoffice.Controllers;

public class ProfileController : Controller
{
		public IActionResult Index()
		{
				return View();
		}
}