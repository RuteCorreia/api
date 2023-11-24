using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace Backoffice.Controllers;

public class CalendarController : Controller
{
		public IActionResult Index()
		{
				return View();
		}
}
