using System.Linq;
using System.Threading.Tasks;
using Lab6.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers
{
	public class ProfessorsController : Controller
	{
		private readonly PostgresContext _context;

		public ProfessorsController(PostgresContext context)
		{
			_context = context;
		}

		// GET: Professors
		public async Task<IActionResult> Index()
		{
			var postgresContext = _context.Professor.Include(p => p.DepartmentNavigation).Include(p => p.PersonNavigation);
			return View(await postgresContext.ToListAsync());
		}

		// GET: Professors/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
				return NotFound();

			var professor = await _context.Professor
				.Include(p => p.DepartmentNavigation)
				.Include(p => p.PersonNavigation)
				.SingleOrDefaultAsync(m => m.Id == id);
			if (professor == null)
				return NotFound();

			return View(professor);
		}

		// GET: Professors/Create
		public IActionResult Create()
		{
			ViewData["Department"] = new SelectList(_context.Department, "Id", "Name");
			ViewData["Person"] = new SelectList(_context.Person, "Id", "Name");
			return View();
		}

		// POST: Professors/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Person,Department")] Professor professor)
		{
			if (ModelState.IsValid)
			{
				_context.Add(professor);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			ViewData["Department"] = new SelectList(_context.Department, "Id", "Name", professor.Department);
			ViewData["Person"] = new SelectList(_context.Person, "Id", "Name", professor.Person);
			return View(professor);
		}

		// GET: Professors/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
				return NotFound();

			var professor = await _context.Professor.SingleOrDefaultAsync(m => m.Id == id);
			if (professor == null)
				return NotFound();
			ViewData["Department"] = new SelectList(_context.Department, "Id", "Name", professor.Department);
			ViewData["Person"] = new SelectList(_context.Person, "Id", "Name", professor.Person);
			return View(professor);
		}

		// POST: Professors/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Person,Department")] Professor professor)
		{
			if (id != professor.Id)
				return NotFound();

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(professor);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ProfessorExists(professor.Id))
						return NotFound();
					throw;
				}
				return RedirectToAction("Index");
			}
			ViewData["Department"] = new SelectList(_context.Department, "Id", "Name", professor.Department);
			ViewData["Person"] = new SelectList(_context.Person, "Id", "Name", professor.Person);
			return View(professor);
		}

		// GET: Professors/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
				return NotFound();

			var professor = await _context.Professor
				.Include(p => p.DepartmentNavigation)
				.Include(p => p.PersonNavigation)
				.SingleOrDefaultAsync(m => m.Id == id);
			if (professor == null)
				return NotFound();

			return View(professor);
		}

		// POST: Professors/Delete/5
		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var professor = await _context.Professor.SingleOrDefaultAsync(m => m.Id == id);
			_context.Professor.Remove(professor);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		private bool ProfessorExists(int id)
		{
			return _context.Professor.Any(e => e.Id == id);
		}
	}
}