using System.ComponentModel.DataAnnotations;
using EPiServer.DataAnnotations;
using TravelBug.Core.Model.Interfaces;

namespace TravelBug.Core.Model.Pages
{
	[ContentType(DisplayName = "Container Page", GUID = "7fc18fae-33c1-4f64-94c6-c5a279e6bf7e", Description = "")]
	[ImageUrl("~/Assets/images/icons/folder-icon.svg")]
	public class ContainerPage : BaseContainer, INavigationMenu
	{
		[Display(Name = "Show in Navigation", Order = 10)]
		public virtual bool IsVisible { get; set; }
	}
}
