using System.Linq;
using System.Threading.Tasks;
using Lab6.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers
{
	public class CurriculumsController : Controller
	{
		private readonly PostgresContext _context;

		public CurriculumsController(PostgresContext context)
		{
			_context = context;
		}

		// GET: Curriculums
		public async Task<IActionResult> Index()
		{
			var postgresContext = _context.Curriculum.Include(c => c.Professor).ThenInclude(professor => professor.PersonNavigation).Include(c => c.SpecialtyNavigation)
				.Include(c => c.SubjectNavigation);
			return View(await postgresContext.ToListAsync());
		}

		// GET: Curriculums/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
				return NotFound();

			var curriculum = await _context.Curriculum
				.Include(c => c.Professor)
				.Include(c => c.SpecialtyNavigation)
				.Include(c => c.SubjectNavigation)
				.SingleOrDefaultAsync(m => m.Id == id);
			if (curriculum == null)
				return NotFound();

			return View(curriculum);
		}

		// GET: Curriculums/Create
		public IActionResult Create()
		{
			ViewData["ProfessorId"] = new SelectList(
				_context.Professor.Select(professor => new
				{
					professor.Id,
					Name = (from person in _context.Person
						where person.Id == professor.Person
						select $"{person.Surname} {person.Name} {person.Middlename}").Single()
				}), "Id", "Name");
			ViewData["Specialty"] = new SelectList(_context.Specialty, "Id", "Name");
			ViewData["Subject"] = new SelectList(_context.Subject, "Id", "Name");
			return View();
		}

		// POST: Curriculums/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(
			[Bind("Id,Subject,Semester,Course,Specialty,HoursAmount,ProfessorId,HasExam")] Curriculum curriculum)
		{
			if (ModelState.IsValid)
			{
				_context.Add(curriculum);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			ViewData["ProfessorId"] = new SelectList(_context.Professor, "Id", "Id", curriculum.ProfessorId);
			ViewData["Specialty"] = new SelectList(_context.Specialty, "Id", "Name", curriculum.Specialty);
			ViewData["Subject"] = new SelectList(_context.Subject, "Id", "Name", curriculum.Subject);
			return View(curriculum);
		}

		// GET: Curriculums/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
				return NotFound();

			var curriculum = await _context.Curriculum.SingleOrDefaultAsync(m => m.Id == id);
			if (curriculum == null)
				return NotFound();
			ViewData["ProfessorId"] =
				new SelectList(
					_context.Professor.Select(professor => new
					{
						professor.Id,
						Name = (from person in _context.Person
							where person.Id == professor.Person
							select $"{person.Surname} {person.Name} {person.Middlename}").Single()
					}), "Id", "Name", curriculum.ProfessorId);
			ViewData["Specialty"] = new SelectList(_context.Specialty, "Id", "Name", curriculum.Specialty);
			ViewData["Subject"] = new SelectList(_context.Subject, "Id", "Name", curriculum.Subject);
			return View(curriculum);
		}

		// POST: Curriculums/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id,
			[Bind("Id,Subject,Semester,Course,Specialty,HoursAmount,ProfessorId,HasExam")] Curriculum curriculum)
		{
			if (id != curriculum.Id)
				return NotFound();

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(curriculum);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!CurriculumExists(curriculum.Id))
						return NotFound();
					throw;
				}
				return RedirectToAction("Index");
			}
			ViewData["ProfessorId"] = new SelectList(_context.Professor.Select(professor => new
			{
				professor.Id,
				Name = (from person in _context.Person
					where person.Id == professor.Person
					select $"{person.Surname} {person.Name} {person.Middlename}").Single()
			}), "Id", "Name", curriculum.ProfessorId);
			ViewData["Specialty"] = new SelectList(_context.Specialty, "Id", "Name", curriculum.Specialty);
			ViewData["Subject"] = new SelectList(_context.Subject, "Id", "Name", curriculum.Subject);
			return View(curriculum);
		}

		// GET: Curriculums/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
				return NotFound();

			var curriculum = await _context.Curriculum
				.Include(c => c.Professor)
				.Include(c => c.SpecialtyNavigation)
				.Include(c => c.SubjectNavigation)
				.SingleOrDefaultAsync(m => m.Id == id);
			if (curriculum == null)
				return NotFound();

			return View(curriculum);
		}

		// POST: Curriculums/Delete/5
		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var curriculum = await _context.Curriculum.SingleOrDefaultAsync(m => m.Id == id);
			_context.Curriculum.Remove(curriculum);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		private bool CurriculumExists(int id)
		{
			return _context.Curriculum.Any(e => e.Id == id);
		}
	}
}