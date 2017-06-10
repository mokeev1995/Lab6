using System.Collections.Generic;

namespace Lab6.ViewModels.Home
{
	public class MenuLink
	{
		public string Name { get; }
		public string Link { get; }

		public MenuLink(string name, string link)
		{
			Name = name;
			Link = link;
		}
	}

	public class IndexModel
	{
		public List<MenuLink> Links { get; set; } = new List<MenuLink>();
	}
}