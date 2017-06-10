using System;
using System.Collections.Generic;

namespace Lab6.Entities
{
	public class Sheet
	{
		public int Id { get; set; }
		public int Student { get; set; }
		public int Curriculum { get; set; }
		public DateTime Date { get; set; }

		public virtual ICollection<AcademicRecord> AcademicRecord { get; set; } = new HashSet<AcademicRecord>();
		public virtual ICollection<ExaminationList> ExaminationList { get; set; } = new HashSet<ExaminationList>();
		public virtual Curriculum CurriculumNavigation { get; set; }
		public virtual Student StudentNavigation { get; set; }
	}
}