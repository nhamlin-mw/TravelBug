using System.Web.Mvc;
using EPiServer.Core;
using EPiServer.Web.Mvc;
using TravelBug.Model.Pages;

namespace TravelBug.Controllers
{
	public abstract class BasePageController<TPageType> : PageController<TPageType> where TPageType : BasePage
	{
		
	}
}
