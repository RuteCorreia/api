using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Backoffice.Models;

namespace Backoffice.Controllers;

public class LayoutController : Controller
{
		public IActionResult StarterPage()
		{
				return View();
		}

		public IActionResult FixedFooter()
		{
				return View();
		}

		public IActionResult FullHeight()
		{
				return View();
		}

		public IActionResult FullWidth()
		{
				return View();
		}

		public IActionResult BoxedLayout()
		{
				return View();
		}

		public IActionResult CollapsedSidebar()
		{
				return View();
		}

		public IActionResult TopNav()
		{
				return View();
		}

		public IActionResult MixedNav()
		{
				return View();
		}

		public IActionResult MixedNavBoxedLayout()
		{
				return View();
		}
}