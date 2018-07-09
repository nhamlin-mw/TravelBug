using EPiServer.Core;

namespace TravelBug.Model.Blocks.ViewModels
{
	public class BlockEditPageViewModel
	{
		public PreviewBlock previewBlock { get; set; }

		public BlockEditPageViewModel(PageData page, IContent content)
		{
			previewBlock = new PreviewBlock(page, content);
		}
	}
}
