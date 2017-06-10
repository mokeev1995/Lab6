namespace Lab6.Entities
{
	public class ProfessorSubject
	{
		public int Id { get; set; }
		public int Professor { get; set; }
		public int Subject { get; set; }

		public virtual Professor ProfessorNavigation { get; set; }
		public virtual Subject SubjectNavigation { get; set; }
	}
}