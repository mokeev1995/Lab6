using Microsoft.AspNetCore.Mvc;

namespace Lab6.Controllers
{
	public abstract class BaseController : Controller
	{
		protected BaseController(PostgresContext context)
		{
			Context = context;
		}

		public PostgresContext Context { get; }
	}
}