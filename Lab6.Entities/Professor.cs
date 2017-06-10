using System.Collections.Generic;

namespace Lab6.Entities
{
	public class Professor
	{
		public int Id { get; set; }
		public int Person { get; set; }
		public int Department { get; set; }

		public virtual ICollection<Curriculum> Curriculum { get; set; } = new HashSet<Curriculum>();
		public virtual ICollection<ProfessorSubject> ProfessorSubject { get; set; } = new HashSet<ProfessorSubject>();
		public virtual Department DepartmentNavigation { get; set; }
		public virtual Person PersonNavigation { get; set; }
	}
}