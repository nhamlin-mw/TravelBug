using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using TravelBug.Core.Utility.Nansen.Encore.Content;

namespace TravelBug.Core.Utility.Nansen.Encore
{
	public static class ContentUtility
	{
		private static IContentLoaderFacade _contentLoaderFacade;

		/// <summary>
		/// <see cref="IContentLoaderFacade"/> to use. Use setter to temporary change the default one used. Change the default implementation though structuremap.
		/// </summary>
		public static IContentLoaderFacade ContentLoaderFacade
		{
			get { return _contentLoaderFacade ?? (_contentLoaderFacade = ServiceLocator.Current.GetInstance<IContentLoaderFacade>()); }
			set { _contentLoaderFacade = value; }
		}

		/// <summary>
		/// Filters content for the current visitor/user according to publish status and page access
		/// </summary>
		/// <typeparam name="TContentType">Type of content items</typeparam>
		/// <param name="content">The items to check access for</param>
		/// <param name="contentFilterFacade">Optional <see cref="IContentFilterFacade"/>, uses <see cref="ServiceLocator.Current"/> to resolve if null</param>
		/// <returns>A filtered list containing content items that the current user has access to</returns>
		public static IEnumerable<TContentType> FilterForVisitor<TContentType>(this IEnumerable<TContentType> content, IContentFilterFacade contentFilterFacade = null)
			where TContentType : IContent
		{
			return ContentLoaderFacade.FilterForVisitor(content, contentFilterFacade);
		}
	}
}
