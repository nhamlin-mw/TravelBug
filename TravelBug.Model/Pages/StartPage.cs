using System.ComponentModel.DataAnnotations;
using System.Web.Razor.Parser.SyntaxTree;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using TravelBug.Model.Blocks;

namespace TravelBug.Model.Pages
{
	[ContentType(DisplayName = "Start Page", GUID = "fb200427-d190-49a3-9200-cc120f3d22bb", Description = "")]
	[ImageUrl("~/Assets/images/icons/start_page.png")]
	public class StartPage : BasePage
	{
		[CultureSpecific]
		[Display(Name = "Navigation Area", Order = 10)]
		public virtual ContentArea NavContentArea { get; set; }
		
		[Display(Name = "Hero Block", Order = 20)]
		[UIHint(UIHint.Block)]
		public virtual HeroBlock HeroBlock { get; set; }

		[CultureSpecific]
		[Display(Name = "Main Content Area", Order = 30)]
		public virtual ContentArea MainContentArea { get; set; }
	}
}
