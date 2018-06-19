using System.Linq;
using System.Web.Mvc;

namespace TravelBug.Application.Configuration
{
	public class TravelViewEngine : RazorViewEngine
	{
		private static readonly string[] _additionalViewsFormats =
		{
			"~/Views/Blocks/{0}.cshtml",
			"~/Views/Pages/{1}/{0}.cshtml",
			"~/Views/Pages/{0}.cshtml",
			"~/Views/Shared/PagePartials/{0}.cshtml",
			"~/Views/Shared/Partials/{0}.cshtml",
			"~/Views/Shared/Blocks/{0}.cshtml"
		};

		public TravelViewEngine()
		{
			PartialViewLocationFormats = PartialViewLocationFormats.Union(_additionalViewsFormats).ToArray();
			ViewLocationFormats = ViewLocationFormats.Union(_additionalViewsFormats).ToArray();
		}
	}
}
