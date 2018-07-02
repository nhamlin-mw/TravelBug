using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using TravelBug.Core.Model.Blocks;

namespace TravelBug.Model.Blocks
{
	[ContentType(DisplayName = "Hero Block", GUID = "31d0d43e-b7a5-4216-a88c-7dd2a6a22ff0", Description = "")]
	public class HeroBlock : BaseBlock
	{
		[Display(Name = "Background Image", Order = 10)]
		[UIHint(UIHint.Image)]
		public virtual ContentReference BackgroundImage { get; set; }

		[Display(Name = "Mobile Background Image", Order = 15)]
		[UIHint(UIHint.Image)]
		public virtual ContentReference MobileBackgroundImage { get; set; }

		[CultureSpecific]
		[Display(Name = "Header", Order = 20)]
		public virtual string Header { get; set; }

		[CultureSpecific]
		[Display(Name = "Subheader", Order = 30)]
		public virtual string Subheader { get; set; }
	}
}
