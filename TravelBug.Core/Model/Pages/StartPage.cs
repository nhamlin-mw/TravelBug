using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAnnotations;

namespace TravelBug.Core.Model.Pages
{
	[ContentType(DisplayName = "Start Page", GUID = "fb200427-d190-49a3-9200-cc120f3d22bb", Description = "")]
	[ImageUrl("~/Assets/images/icons/start_page.png")]
	public class StartPage : BasePage
	{
		[CultureSpecific]
		[Display(Name = "Hero Area", Order = 10)]
		public virtual ContentArea HeroContentArea { get; set; }

		[CultureSpecific]
		[Display(Name = "Navigation Area", Order = 20)]
		public virtual ContentArea NavContentArea { get; set; }

		[CultureSpecific]
		[Display(Name = "Main Content Area", Order = 30)]
		public virtual ContentArea MainContentArea { get; set; }
	}
}
