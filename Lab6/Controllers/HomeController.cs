using Lab6.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace Lab6.Controllers
{
	public class HomeController: Controller
	{
		public IActionResult Index()
		{
			var model = new IndexModel();

			model.Links.Add(new MenuLink("Curriculums", "/curriculums"));
			model.Links.Add(new MenuLink("Persons", "/persons"));
			model.Links.Add(new MenuLink("Professors", "/professors"));
			model.Links.Add(new MenuLink("Specialities", "/specialities"));
			model.Links.Add(new MenuLink("Students", "/students"));
			model.Links.Add(new MenuLink("Subjects", "/subjects"));

			return View(model);
		}
	}
}