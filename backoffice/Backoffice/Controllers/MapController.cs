using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Backoffice.Models;

namespace Backoffice.Controllers;

public class MapController : Controller
{
		public IActionResult Index()
		{
				return View();
		}
}