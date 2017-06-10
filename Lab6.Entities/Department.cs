using System.Collections.Generic;

namespace Lab6.Entities
{
	public class Department
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public virtual ICollection<Professor> Professor { get; set; } = new HashSet<Professor>();
	}
}