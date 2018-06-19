using System.Collections.Generic;
using EPiServer.Core;
using TravelBug.Model.Interfaces;
using TravelBug.Model.Pages;

namespace TravelBug.Model.Blocks.ViewModels
{
	public class TopNavViewModel
	{
		// The TopNavBlock
		public TopNavBlock TopNavBlock { get; private set; }

		// The children of the ParentPage specifed in the TopNavBlock 
		public IEnumerable<INavigationMenu> PageChildren { get; set; }

		// Constructor
		public TopNavViewModel(TopNavBlock topNavBlock, IEnumerable<INavigationMenu> pageChildren)
		{
			TopNavBlock = topNavBlock;
			PageChildren = pageChildren;
		}
	}
}
