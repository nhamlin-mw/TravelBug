using System.Collections.Generic;

namespace TravelBug.Core
{
	public static class Constants
	{
		public const string BaseChildrenCssClass = "";  //Grid-cell
		public const string BaseContainerCssClass = ""; //Grid

		public static readonly Dictionary<string, string> BlockDisplayClasses = new Dictionary<string, string>
		{
			{ BlockDisplayTags.Full, "col" },
			{ BlockDisplayTags.ThreeQuarters, "col-9" },
			{ BlockDisplayTags.TwoThirds, "col-8" },
			{ BlockDisplayTags.Half, "col-6" },
			{ BlockDisplayTags.OneThird, "col-4" },
			{ BlockDisplayTags.OneQuarter, "col-3" }
		};

		public static class BlockDisplayTags
		{
			public const string Full = "full";
			public const string ThreeQuarters = "threequarters";
			public const string TwoThirds = "twothirds";
			public const string Half = "half";
			public const string OneThird = "onethird";
			public const string OneQuarter = "onequarter";
		}
	}
}
