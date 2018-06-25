using EPiServer.Core;
using EPiServer.DataAnnotations;
using static TravelBug.Core.Constants.BlockDisplayTags;

namespace TravelBug.Model.Blocks
{
	[ContentType(DisplayName = "BaseBlock", GUID = "d98e7485-38cf-4701-a6da-3ea180d69983", Description = "")]
	public class BaseBlock : BlockData
	{
		//public virtual string[] GetSupportedDisplayOptions()
		//{
		//	return new[]
		//	{
		//		Full,
		//		TwoThirds,
		//		Half,
		//		OneThird,
		//		OneQuarter
		//	};
		//}
	}
}
