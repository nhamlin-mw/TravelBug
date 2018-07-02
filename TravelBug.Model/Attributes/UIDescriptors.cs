using EPiServer.Shell;
using TravelBug.Core;
using TravelBug.Core.Model.Pages;

namespace TravelBug.Model.Attributes
{
	class UIDescriptors
	{
		/// <summary>
		///     Describes how the UI should appear for <see cref="ContainerPage" /> content.
		/// </summary>
		[UIDescriptorRegistration]
		public class ContainerPageUIDescriptor : UIDescriptor<BaseContainer>
		{
			public ContainerPageUIDescriptor()
				: base(ContentTypeCssClassNames.Container)
			{
				DefaultView = CmsViewNames.AllPropertiesView;
				IconClass = EpiserverDefaultContentIcons.ObjectIcons.Folder;
			}
		}
	}
}
