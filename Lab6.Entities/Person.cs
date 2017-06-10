using System.Collections.Generic;

namespace Lab6.Entities
{
	public class Person
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Middlename { get; set; }

		public virtual ICollection<Professor> Professor { get; set; } = new HashSet<Professor>();
		public virtual ICollection<Student> Student { get; set; } = new HashSet<Student>();
	}
}