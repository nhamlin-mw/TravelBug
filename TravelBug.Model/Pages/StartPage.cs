using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAnnotations;

namespace TravelBug.Model.Pages
{
	[ContentType(DisplayName = "StartPage", GUID = "fb200427-d190-49a3-9200-cc120f3d22bb", Description = "")]
	public class StartPage : PageData
	{
		[CultureSpecific]
		[Display(Name = "Main Content Area", Order = 20)]
		public virtual ContentArea MainContentArea { get; set; }
		
		[CultureSpecific]
		[Display(Name = "Navigation Area", Order = 10)]
		public virtual ContentArea NavContentArea { get; set; }
	}
}
