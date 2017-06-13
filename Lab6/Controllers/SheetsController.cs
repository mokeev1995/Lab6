using System.Linq;
using System.Threading.Tasks;
using Lab6.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers
{
	public class SheetsController : Controller
	{
		private readonly PostgresContext _context;

		public SheetsController(PostgresContext context)
		{
			_context = context;
		}

		// GET: Sheets
		public async Task<IActionResult> Index()
		{
			var postgresContext = _context.Sheet
				.Include(s => s.CurriculumNavigation)
					.ThenInclude(curriculum => curriculum.SubjectNavigation)
				.Include(s=>s.CurriculumNavigation)
					.ThenInclude(curriculum => curriculum.SpecialtyNavigation)
				.Include(s => s.StudentNavigation)
					.ThenInclude(student => student.PersonNavigation);
			return View(await postgresContext.ToListAsync());
		}

		// GET: Sheets/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
				return NotFound();

			var sheet = await _context.Sheet
				.Include(s => s.CurriculumNavigation)
					.ThenInclude(curriculum => curriculum.SubjectNavigation)
				.Include(s => s.CurriculumNavigation)
					.ThenInclude(curriculum => curriculum.SpecialtyNavigation)
				.Include(s => s.StudentNavigation)
					.ThenInclude(student => student.PersonNavigation)
				.SingleOrDefaultAsync(m => m.Id == id);
			if (sheet == null)
				return NotFound();

			return View(sheet);
		}

		// GET: Sheets/Create
		public IActionResult Create()
		{
			ViewData["Curriculum"] =
				new SelectList(
					from cur in _context.Curriculum
					select new
					{
						cur.Id,
						Name = $"{cur.SpecialtyNavigation.Name}, {cur.SubjectNavigation.Name}, Course: {cur.Course}, Semester: {cur.Semester}"
					}, "Id", "Name");
			ViewData["Student"] = 
				new SelectList(
					from student in _context.Student
					select new
					{
						student.Id,
						Name = $"{student.PersonNavigation.Surname} {student.PersonNavigation.Name} {student.PersonNavigation.Middlename}"
					}, 
					"Id", "Name");
			return View();
		}

		// POST: Sheets/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Student,Curriculum,Date")] Sheet sheet)
		{
			if (ModelState.IsValid)
			{
				_context.Add(sheet);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			ViewData["Curriculum"] =
				new SelectList(
					from cur in _context.Curriculum
					select new
					{
						cur.Id,
						Name = $"{cur.SpecialtyNavigation.Name}, {cur.SubjectNavigation.Name}, Course: {cur.Course}, Semester: {cur.Semester}"
					}, "Id", "Name",
					sheet.Curriculum);
			ViewData["Student"] = new SelectList(from student in _context.Student
				select new
				{
					student.Id,
					Name = $"{student.PersonNavigation.Surname} {student.PersonNavigation.Name} {student.PersonNavigation.Middlename}"
				},
				"Id", "Name", sheet.Student);
			return View(sheet);
		}

		// GET: Sheets/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
				return NotFound();

			var sheet = await _context.Sheet.SingleOrDefaultAsync(m => m.Id == id);
			if (sheet == null)
				return NotFound();
			ViewData["Curriculum"] =
				new SelectList(
					from cur in _context.Curriculum
					select new
					{
						cur.Id,
						Name = $"{cur.SpecialtyNavigation.Name}, {cur.SubjectNavigation.Name}, Course: {cur.Course}, Semester: {cur.Semester}"
					}, "Id", "Name",
					sheet.Curriculum);
			ViewData["Student"] = new SelectList(from student in _context.Student
				select new
				{
					student.Id,
					Name = $"{student.PersonNavigation.Surname} {student.PersonNavigation.Name} {student.PersonNavigation.Middlename}"
				},
				"Id", "Name", sheet.Student);
			return View(sheet);
		}

		// POST: Sheets/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Student,Curriculum,Date")] Sheet sheet)
		{
			if (id != sheet.Id)
				return NotFound();

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(sheet);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!SheetExists(sheet.Id))
						return NotFound();
					throw;
				}
				return RedirectToAction("Index");
			}
			ViewData["Curriculum"] =
				new SelectList(
					from cur in _context.Curriculum
					select new
					{
						cur.Id,
						Name = $"{cur.SpecialtyNavigation.Name}, {cur.SubjectNavigation.Name}, Course: {cur.Course}, Semester: {cur.Semester}"
					}, "Id", "Name",
					sheet.Curriculum);
			ViewData["Student"] = new SelectList(from student in _context.Student
				select new
				{
					student.Id,
					Name = $"{student.PersonNavigation.Surname} {student.PersonNavigation.Name} {student.PersonNavigation.Middlename}"
				},
				"Id", "Name", sheet.Student);
			return View(sheet);
		}

		// GET: Sheets/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
				return NotFound();

			var sheet = await _context.Sheet
				.Include(s => s.CurriculumNavigation)
				.Include(s => s.StudentNavigation)
				.SingleOrDefaultAsync(m => m.Id == id);
			if (sheet == null)
				return NotFound();

			return View(sheet);
		}

		// POST: Sheets/Delete/5
		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var sheet = await _context.Sheet.SingleOrDefaultAsync(m => m.Id == id);
			_context.Sheet.Remove(sheet);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		private bool SheetExists(int id)
		{
			return _context.Sheet.Any(e => e.Id == id);
		}
	}
}