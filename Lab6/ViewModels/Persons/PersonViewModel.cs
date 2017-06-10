using Lab6.Entities;

namespace Lab6.ViewModels.Persons
{
	public class PersonViewModel
	{
		public int? Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Middlename { get; set; }

		public static PersonViewModel FromPerson(Person person)
		{
			return new PersonViewModel
			{
				Id = person.Id,
				Middlename = person.Middlename,
				Surname = person.Surname,
				Name = person.Name
			};
		}
	}
}