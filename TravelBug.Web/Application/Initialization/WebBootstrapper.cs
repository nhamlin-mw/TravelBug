using StructureMap;
using TravelBug.Core.Utility.Nansen.Encore.Content;

namespace TravelBug.Application.Initialization
{
	public class WebBootstrapper : Registry
	{
		public WebBootstrapper()
		{
			// layout/metadata modifiers
			
			For<IContentLoaderFacade>().Use<DefaultContentLoaderFacade>();
		}
	}
}
