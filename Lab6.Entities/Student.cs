using System.Collections.Generic;

namespace Lab6.Entities
{
	public class Student
	{
		public int Id { get; set; }
		public int Person { get; set; }
		public int StudentsGroup { get; set; }

		public virtual ICollection<Sheet> Sheet { get; set; } = new HashSet<Sheet>();
		public virtual Person PersonNavigation { get; set; }
		public virtual Studentsgroup StudentsGroupNavigation { get; set; }
	}
}