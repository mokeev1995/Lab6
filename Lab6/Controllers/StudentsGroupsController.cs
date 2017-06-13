using System.Linq;
using System.Threading.Tasks;
using Lab6.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers
{
	public class StudentsGroupsController : Controller
	{
		private readonly PostgresContext _context;

		public StudentsGroupsController(PostgresContext context)
		{
			_context = context;
		}

		// GET: StudentsGroups
		public async Task<IActionResult> Index()
		{
			var postgresContext = _context.Studentsgroup.Include(s => s.SpecialtyNavigation);
			return View(await postgresContext.ToListAsync());
		}

		// GET: StudentsGroups/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
				return NotFound();

			var studentsGroup = await _context.Studentsgroup
				.Include(s => s.SpecialtyNavigation)
				.SingleOrDefaultAsync(m => m.Id == id);
			if (studentsGroup == null)
				return NotFound();

			return View(studentsGroup);
		}

		// GET: StudentsGroups/Create
		public IActionResult Create()
		{
			ViewData["Specialty"] = new SelectList(_context.Specialty, "Id", "Name");
			return View();
		}

		// POST: StudentsGroups/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(
			[Bind("Id,Name,DateStart,DateFinish,Specialty,Course")] StudentsGroup studentsGroup)
		{
			if (ModelState.IsValid)
			{
				_context.Add(studentsGroup);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			ViewData["Specialty"] = new SelectList(_context.Specialty, "Id", "Name", studentsGroup.Specialty);
			return View(studentsGroup);
		}

		// GET: StudentsGroups/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
				return NotFound();

			var studentsGroup = await _context.Studentsgroup.SingleOrDefaultAsync(m => m.Id == id);
			if (studentsGroup == null)
				return NotFound();
			ViewData["Specialty"] = new SelectList(_context.Specialty, "Id", "Name", studentsGroup.Specialty);
			return View(studentsGroup);
		}

		// POST: StudentsGroups/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id,
			[Bind("Id,Name,DateStart,DateFinish,Specialty,Course")] StudentsGroup studentsGroup)
		{
			if (id != studentsGroup.Id)
				return NotFound();

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(studentsGroup);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!StudentsGroupExists(studentsGroup.Id))
						return NotFound();
					throw;
				}
				return RedirectToAction("Index");
			}
			ViewData["Specialty"] = new SelectList(_context.Specialty, "Id", "Name", studentsGroup.Specialty);
			return View(studentsGroup);
		}

		// GET: StudentsGroups/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
				return NotFound();

			var studentsGroup = await _context.Studentsgroup
				.Include(s => s.SpecialtyNavigation)
				.SingleOrDefaultAsync(m => m.Id == id);
			if (studentsGroup == null)
				return NotFound();

			return View(studentsGroup);
		}

		// POST: StudentsGroups/Delete/5
		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var studentsGroup = await _context.Studentsgroup.SingleOrDefaultAsync(m => m.Id == id);
			_context.Studentsgroup.Remove(studentsGroup);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		private bool StudentsGroupExists(int id)
		{
			return _context.Studentsgroup.Any(e => e.Id == id);
		}
	}
}