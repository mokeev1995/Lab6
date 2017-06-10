using System;

namespace Lab6.Entities
{
	public class Examinationlist
	{
		public int Id { get; set; }
		public int Sheet { get; set; }
		public int Result { get; set; }
		public DateTime Date { get; set; }

		public virtual Sheet SheetNavigation { get; set; }
	}
}