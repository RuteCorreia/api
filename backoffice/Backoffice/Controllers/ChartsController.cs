using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Backoffice.Models;

namespace Backoffice.Controllers;

public class ChartsController : Controller
{
		public IActionResult ChartJs()
		{
				return View();
		}

		public IActionResult ApexchartsJs()
		{
				return View();
		}
}
