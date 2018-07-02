using EPiServer.Core;

namespace TravelBug.Core.Model.Interfaces
{
	public interface INavigationMenu : IContent
	{
		bool IsVisible { get; set; }
	}
}
