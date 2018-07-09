using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using TravelBug.Model.Blocks.ViewModels;

namespace TravelBug.Controllers
{
	[TemplateDescriptor(Inherited = true,
		TemplateTypeCategory = TemplateTypeCategories.MvcController,
		Tags = new[] { RenderingTags.Preview, RenderingTags.Edit },
		AvailableWithoutTag = false)]
	public class PreviewBlockController : ActionControllerBase, IRenderTemplate<BlockData>
	{
		private readonly IContentLoader _contentLoader;

		public PreviewBlockController(IContentLoader contentLoader)
		{
			_contentLoader = contentLoader;
		}

		public ActionResult Index(IContent currentContent)
		{
			var startPage = _contentLoader.Get<PageData>(ContentReference.StartPage);
			var model = new BlockEditPageViewModel(startPage, currentContent);

			return View("PreviewBlock", model);
		}
	}
}
