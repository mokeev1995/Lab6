using System.Linq;
using System.Threading.Tasks;
using Lab6.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers
{
	public class StudentsController : Controller
	{
		private readonly PostgresContext _context;

		public StudentsController(PostgresContext context)
		{
			_context = context;
		}

		// GET: Students
		public async Task<IActionResult> Index()
		{
			var postgresContext = _context.Student.Include(s => s.PersonNavigation).Include(s => s.StudentsGroupNavigation);
			return View(await postgresContext.ToListAsync());
		}

		// GET: Students/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
				return NotFound();

			var student = await _context.Student
				.Include(s => s.PersonNavigation)
				.Include(s => s.StudentsGroupNavigation)
				.SingleOrDefaultAsync(m => m.Id == id);
			if (student == null)
				return NotFound();

			return View(student);
		}

		// GET: Students/Create
		public IActionResult Create()
		{
			ViewData["Person"] = new SelectList(_context.Person, "Id", "Name");
			ViewData["StudentsGroup"] = new SelectList(_context.Studentsgroup, "Id", "Name");
			return View();
		}

		// POST: Students/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Person,StudentsGroup")] Student student)
		{
			if (ModelState.IsValid)
			{
				_context.Add(student);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			ViewData["Person"] = new SelectList(_context.Person, "Id", "Name", student.Person);
			ViewData["StudentsGroup"] = new SelectList(_context.Studentsgroup, "Id", "Name", student.StudentsGroup);
			return View(student);
		}

		// GET: Students/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
				return NotFound();

			var student = await _context.Student.SingleOrDefaultAsync(m => m.Id == id);
			if (student == null)
				return NotFound();
			ViewData["Person"] = new SelectList(_context.Person, "Id", "Name", student.Person);
			ViewData["StudentsGroup"] = new SelectList(_context.Studentsgroup, "Id", "Name", student.StudentsGroup);
			return View(student);
		}

		// POST: Students/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Person,StudentsGroup")] Student student)
		{
			if (id != student.Id)
				return NotFound();

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(student);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!StudentExists(student.Id))
						return NotFound();
					throw;
				}
				return RedirectToAction("Index");
			}
			ViewData["Person"] = new SelectList(_context.Person, "Id", "Name", student.Person);
			ViewData["StudentsGroup"] = new SelectList(_context.Studentsgroup, "Id", "Name", student.StudentsGroup);
			return View(student);
		}

		// GET: Students/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
				return NotFound();

			var student = await _context.Student
				.Include(s => s.PersonNavigation)
				.Include(s => s.StudentsGroupNavigation)
				.SingleOrDefaultAsync(m => m.Id == id);
			if (student == null)
				return NotFound();

			return View(student);
		}

		// POST: Students/Delete/5
		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var student = await _context.Student.SingleOrDefaultAsync(m => m.Id == id);
			_context.Student.Remove(student);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		private bool StudentExists(int id)
		{
			return _context.Student.Any(e => e.Id == id);
		}
	}
}