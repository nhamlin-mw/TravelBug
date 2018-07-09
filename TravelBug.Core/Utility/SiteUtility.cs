using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using TravelBug.Core.Model.Pages;

namespace TravelBug.Core.Utility
{
	public static class SiteUtility
	{
		private static IContentLoader _contentLoader => ServiceLocator.Current.GetInstance<IContentLoader>();

		public static StartPage StartPage
		{
			get
			{
				return _contentLoader.Get<StartPage>(ContentReference.StartPage);
			}
		}
	}
}
