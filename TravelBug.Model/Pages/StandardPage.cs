using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace TravelBug.Model.Pages
{
	[ContentType(DisplayName = "StandardPage", GUID = "a3c1b128-180d-4543-8812-e9dfed532983", Description = "")]
	public class StandardPage : PageData
	{
		[CultureSpecific]
		[Display(
			Name = "Main body",
			Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
			GroupName = SystemTabNames.Content,
			Order = 1)]
		public virtual XhtmlString MainBody { get; set; }
	}
}
