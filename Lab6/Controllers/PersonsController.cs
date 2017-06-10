using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab6.Controllers
{
	public class PersonsController : BaseController
	{
		public PersonsController(PostgresContext context) : base(context)
		{
		}

		// GET: Persons
		public ActionResult Index()
		{
			return View(Context.Person.ToArray());
		}

		// GET: Persons/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: Persons/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Persons/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				// TODO: Add insert logic here
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		// GET: Persons/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: Persons/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				// TODO: Add update logic here

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		// GET: Persons/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: Persons/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				// TODO: Add delete logic here

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}
	}
}