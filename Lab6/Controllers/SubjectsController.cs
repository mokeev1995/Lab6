using System.Linq;
using System.Threading.Tasks;
using Lab6.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers
{
	public class SubjectsController : Controller
	{
		private readonly PostgresContext _context;

		public SubjectsController(PostgresContext context)
		{
			_context = context;
		}

		// GET: Subjects
		public async Task<IActionResult> Index()
		{
			return View(await _context.Subject.ToListAsync());
		}

		// GET: Subjects/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
				return NotFound();

			var subject = await _context.Subject
				.SingleOrDefaultAsync(m => m.Id == id);
			if (subject == null)
				return NotFound();

			return View(subject);
		}

		// GET: Subjects/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Subjects/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name")] Subject subject)
		{
			if (ModelState.IsValid)
			{
				_context.Add(subject);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(subject);
		}

		// GET: Subjects/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
				return NotFound();

			var subject = await _context.Subject.SingleOrDefaultAsync(m => m.Id == id);
			if (subject == null)
				return NotFound();
			return View(subject);
		}

		// POST: Subjects/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Subject subject)
		{
			if (id != subject.Id)
				return NotFound();

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(subject);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!SubjectExists(subject.Id))
						return NotFound();
					throw;
				}
				return RedirectToAction("Index");
			}
			return View(subject);
		}

		// GET: Subjects/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
				return NotFound();

			var subject = await _context.Subject
				.SingleOrDefaultAsync(m => m.Id == id);
			if (subject == null)
				return NotFound();

			return View(subject);
		}

		// POST: Subjects/Delete/5
		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var subject = await _context.Subject.SingleOrDefaultAsync(m => m.Id == id);
			_context.Subject.Remove(subject);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		private bool SubjectExists(int id)
		{
			return _context.Subject.Any(e => e.Id == id);
		}
	}
}