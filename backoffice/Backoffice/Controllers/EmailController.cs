using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Backoffice.Models;

namespace Backoffice.Controllers;

public class EmailController : Controller
{
		public IActionResult Inbox()
		{
				return View();
		}

		public IActionResult Compose()
		{
				return View();
		}

		public IActionResult Detail()
		{
				return View();
		}
}