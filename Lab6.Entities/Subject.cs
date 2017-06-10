using System.Collections.Generic;

namespace Lab6.Entities
{
	public class Subject
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public virtual ICollection<Curriculum> Curriculum { get; set; } = new HashSet<Curriculum>();
		public virtual ICollection<ProfessorSubject> ProfessorSubject { get; set; } = new HashSet<ProfessorSubject>();
	}
}