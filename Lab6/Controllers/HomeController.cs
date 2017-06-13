using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Lab6.Controllers
{
	public class MenuLink
	{
		public MenuLink(string name, string link)
		{
			Link = link;
			Name = name;
		}

		public string Link { get; }
		public string Name { get; }
	}

	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			var links = new List<MenuLink>
			{
				new MenuLink("Curriculums", "/curriculums"),
				new MenuLink("Persons", "/persons"),
				new MenuLink("Professors", "/professors"),
				new MenuLink("Specialities", "/specialities"),
				new MenuLink("Students", "/students"),
				new MenuLink("Subjects", "/subjects"),
				new MenuLink("Departments", "/departments"),
				new MenuLink("StudentsGroups", "/studentsgroups"),
				new MenuLink("Sheets", "/sheets")
			};

			return View(links);
		}
	}
}