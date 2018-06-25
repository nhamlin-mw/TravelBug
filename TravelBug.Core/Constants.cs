using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBug.Core
{
	public class Constants
	{

		//public static string[] DefaultDisplayTags = {
		//	BlockDisplayTags.Full,
		//	BlockDisplayTags.TwoThirds,
		//	BlockDisplayTags.Half,
		//	BlockDisplayTags.OneThird,
		//	BlockDisplayTags.OneQuarter
		//};

		public static class BlockDisplayTags
		{
			public const string Full = "full";
			public const string TwoThirds = "twothirds";
			public const string Half = "half";
			public const string OneThird = "onethird";
			public const string OneQuarter = "onequarter";
		}

		//public static Dictionary<string, string> BlockDisplayClasses = new Dictionary<string, string>
		//{
		//	{ BlockDisplayTags.Full, "u-sizeFull" },
		//	{ BlockDisplayTags.TwoThirds, "u-md-size2of3" },
		//	{ BlockDisplayTags.Half, "u-md-size1of2" },
		//	{ BlockDisplayTags.OneThird, "u-md-size1of3" },
		//	{ BlockDisplayTags.OneQuarter, "u-sm-size1of2 u-md-size1of4" },
		//};
	}
}
