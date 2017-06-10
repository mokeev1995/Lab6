using System.Collections.Generic;

namespace Lab6.Entities
{
	public class Curriculum
	{
		public int Id { get; set; }
		public int Subject { get; set; }
		public int Semester { get; set; }
		public int Course { get; set; }
		public int Specialty { get; set; }
		public int HoursAmount { get; set; }
		public int ProfessorId { get; set; }
		public bool HasExam { get; set; }

		public virtual ICollection<Sheet> Sheet { get; set; } = new HashSet<Sheet>();
		public virtual Professor Professor { get; set; }
		public virtual Specialty SpecialtyNavigation { get; set; }
		public virtual Subject SubjectNavigation { get; set; }
	}
}