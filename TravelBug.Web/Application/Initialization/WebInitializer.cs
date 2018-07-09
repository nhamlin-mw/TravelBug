using System.Linq;
using System.Web.Mvc;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using TravelBug.Core.Utility;
using static TravelBug.Core.Constants;
using InitializationModule = EPiServer.Web.InitializationModule;

namespace TravelBug.Application.Initialization
{
	[InitializableModule]
	[ModuleDependency(typeof(InitializationModule), typeof(ServiceContainerInitialization))]
	public class WebInitializer : IConfigurableModule
	{
		public void Initialize(InitializationEngine context)
		{
			//Add initialization logic, this method is called once after CMS has been initialized
			AddDisplayOptions(context.Locate.Advanced.GetInstance<DisplayOptions>());
		}

		public void Uninitialize(InitializationEngine context)
		{
			//Add uninitialization logic
		}

		public void ConfigureContainer(ServiceConfigurationContext context)
		{
			// Add local web registry
			context.StructureMap().Configure(ctx => ctx.AddRegistry<WebBootstrapper>());

			// Wire up MVC dependency resolvers
			DependencyResolver.SetResolver(new StructureMapDependencyResolver(context.StructureMap())); // mvc
		}

		private void AddDisplayOptions(DisplayOptions displayOptions)
		{
			if (displayOptions.All(dO => dO.Tag != BlockDisplayTags.Full))
			{
				displayOptions.Add(
				                   id: BlockDisplayTags.Full,
				                   tag: BlockDisplayTags.Full,
				                   name: "(1/1) Full",
				                   iconClass: "col",
				                   description: null
				                  );
			}

			if (displayOptions.All(dO => dO.Tag != BlockDisplayTags.TwoThirds))
			{
				displayOptions.Add(
				                   id: BlockDisplayTags.TwoThirds,
				                   tag: BlockDisplayTags.TwoThirds,
				                   name: "(2/3) Two Thirds",
				                   iconClass: "col-9",
				                   description: null
				                  );
			}

			if (displayOptions.All(dO => dO.Tag != BlockDisplayTags.Half))
			{
				displayOptions.Add(
				                   id: BlockDisplayTags.Half,
				                   tag: BlockDisplayTags.Half,
				                   name: "(1/2) Half",
				                   iconClass: "col-6",
				                   description: null
				                  );
			}

			if (displayOptions.All(dO => dO.Tag != BlockDisplayTags.OneThird))
			{
				displayOptions.Add(
				                   id: BlockDisplayTags.OneThird,
				                   tag: BlockDisplayTags.OneThird,
				                   name: "(1/3) One Third",
				                   iconClass: "col-4",
				                   description: null
				                  );
			}

			if (displayOptions.All(dO => dO.Tag != BlockDisplayTags.OneQuarter))
			{
				displayOptions.Add(
				                   id: BlockDisplayTags.OneQuarter,
				                   tag: BlockDisplayTags.OneQuarter,
				                   name: "(1/4) One Quarter",
				                   iconClass: "col-3",
				                   description: null
				                  );
			}
		}
	}
}
