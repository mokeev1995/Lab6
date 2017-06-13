using System;
using System.Collections.Generic;

namespace Lab6.Entities
{
	public class StudentsGroup
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime DateStart { get; set; }
		public DateTime DateFinish { get; set; }
		public int Specialty { get; set; }
		public int Course { get; set; }

		public virtual ICollection<Student> Student { get; set; } = new HashSet<Student>();
		public virtual Specialty SpecialtyNavigation { get; set; }
	}
}