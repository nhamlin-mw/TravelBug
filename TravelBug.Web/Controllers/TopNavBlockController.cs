using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc;
using TravelBug.Model.Blocks;
using TravelBug.Model.Blocks.ViewModels;
using TravelBug.Model.Interfaces;
using TravelBug.Model.Pages;

namespace TravelBug.Controllers
{
	public class TopNavBlockController : BlockController<TopNavBlock>
	{
		public override ActionResult Index(TopNavBlock currentBlock)
		{
			var pageChildren = Enumerable.Empty<INavigationMenu>();

			// Get the content repository
			IContentRepository contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();

			// Get the children of the page specified in the ParentPage property of the block
			if (!PageReference.IsNullOrEmpty(currentBlock.ParentPage))
			{
				pageChildren = contentRepository.GetChildren<INavigationMenu>(currentBlock.ParentPage).Where(m=>m.IsVisible);
			}

			return PartialView("TopNavBlock", new TopNavViewModel(currentBlock, pageChildren));
		}
	}
}
