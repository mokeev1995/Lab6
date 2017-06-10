using System;

namespace Lab6.Entities
{
	public class AcademicRecord
	{
		public int Id { get; set; }
		public int Sheet { get; set; }
		public bool Result { get; set; }
		public DateTime Date { get; set; }

		public virtual Sheet SheetNavigation { get; set; }
	}
}