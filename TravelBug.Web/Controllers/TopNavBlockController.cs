using System.Linq;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Mvc;
using TravelBug.Core.Model.Interfaces;
using TravelBug.Model.Blocks;
using TravelBug.Model.Blocks.ViewModels;

namespace TravelBug.Controllers
{
	public class TopNavBlockController : BlockController<TopNavBlock>
	{
		private readonly IContentLoader _contentLoader;

		public TopNavBlockController(IContentLoader contentLoader)
		{
			_contentLoader = contentLoader;
		}

		public override ActionResult Index(TopNavBlock currentBlock)
		{
			var pageChildren = Enumerable.Empty<INavigationMenu>();

			// Get the children of the page specified in the ParentPage property of the block
			if (!PageReference.IsNullOrEmpty(currentBlock.ParentPage))
			{
				pageChildren = _contentLoader.GetChildren<INavigationMenu>(currentBlock.ParentPage).Where(m=>m.IsVisible);
			}

			return PartialView("TopNavBlock", new TopNavViewModel(currentBlock, pageChildren));
		}
	}
}
