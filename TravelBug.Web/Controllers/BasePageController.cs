using EPiServer.Web.Mvc;
using TravelBug.Core.Model.Pages;

namespace TravelBug.Controllers
{
	public abstract class BasePageController<TPageType> : PageController<TPageType> where TPageType : BasePage
	{
		
	}
}
