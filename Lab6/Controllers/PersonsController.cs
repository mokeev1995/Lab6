using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab6.Core;
using Lab6.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers
{
	public class PersonsController : Controller
	{
		private readonly PostgresContext _context;
		private readonly IDbQueryable _queryable;

		public PersonsController(PostgresContext context, IDbQueryable queryable)
		{
			_context = context;
			_queryable = queryable;
		}

		// GET: Persons
		public async Task<IActionResult> Index()
		{
			return View(await _context.Person.ToListAsync());
		}

		// GET: Persons/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
				return NotFound();

			var person = await _context.Person
				.SingleOrDefaultAsync(m => m.Id == id);
			if (person == null)
				return NotFound();

			return View(person);
		}

		// GET: Persons/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Persons/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Surname,Middlename")] Person person)
		{
			if (ModelState.IsValid)
			{
				_context.Add(person);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(person);
		}

		// GET: Persons/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
				return NotFound();

			var person = await _context.Person.SingleOrDefaultAsync(m => m.Id == id);
			if (person == null)
				return NotFound();
			return View(person);
		}

		// POST: Persons/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Middlename")] Person person)
		{
			if (id != person.Id)
				return NotFound();

			if (ModelState.IsValid)
			{
				try
				{
					await _queryable.SendSqlQueryAsync(
						"update_person",
						new []
						{
							new KeyValuePair<string, object>("p_id", person.Id),
							new KeyValuePair<string, object>("p_name", person.Name),
							new KeyValuePair<string, object>("p_surname", person.Surname),
							new KeyValuePair<string, object>("p_middlename", person.Middlename)
						},
						System.Data.CommandType.StoredProcedure
					);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!PersonExists(person.Id))
						return NotFound();
					throw;
				}
				return RedirectToAction("Index");
			}
			return View(person);
		}

		// GET: Persons/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
				return NotFound();

			var person = await _context.Person
				.SingleOrDefaultAsync(m => m.Id == id);
			if (person == null)
				return NotFound();

			return View(person);
		}

		// POST: Persons/Delete/5
		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var person = await _context.Person.SingleOrDefaultAsync(m => m.Id == id);
			_context.Person.Remove(person);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		private bool PersonExists(int id)
		{
			return _context.Person.Any(e => e.Id == id);
		}
	}
}