using System.Collections.Generic;
using EPiServer;
using EPiServer.Core;

namespace TravelBug.Core.Utility.Nansen.Encore.Content
{
	/// <summary>
	/// Wraps calls made to <see cref="IContentLoader"/> in Encore. The default can be replaced by injecting a custom implementation into the current <see cref="IServiceLocator"/>.
	/// </summary>
	/// <remarks>
	/// Ensure null handling for parameters is handled when implementing.
	/// </remarks>
	public interface IContentLoaderFacade
	{
		/// <summary>
		/// Checks if the current visitor has access to a <see cref="EPiServer.Core.IContent"/> instance.
		/// </summary>
		/// <param name="content">The <see cref="EPiServer.Core.IContent"/> instance to check access for</param>
		/// <param name="contentFilterFacade"><see cref="IContentFilterFacade"/> to use</param>
		/// <returns>Wether the current visitor has access to the content instance or not.</returns>
		bool QueryReadAccess<TContentType>(TContentType content, IContentFilterFacade contentFilterFacade)
			where TContentType : IContent;

		/// <summary>
		/// Filters content for the current visitor/user according to publish status and page access.
		/// </summary>
		/// <typeparam name="TContentType">Type of content queried </typeparam>
		/// <param name="content">the content items to filter</param>
		/// <param name="contentFilterFacade">The <see cref="IContentFilterFacade"/> to use</param>
		/// <returns>A filtered <see cref="IEnumerable{TContentType}"/></returns>
		IEnumerable<TContentType> FilterForVisitor<TContentType>(IEnumerable<TContentType> content, IContentFilterFacade contentFilterFacade)
			where TContentType : IContent;

		/// <summary>
		/// Loads <see cref="EPiServer.Core.IContent"/> from a <see cref="IEnumerable{ContentReference}"/> in a certain language, optionally filtered for visitor read access.
		/// </summary>
		/// <typeparam name="TContentType">Type of content to load</typeparam>
		/// <param name="contentLinks">An <see cref="IEnumerable{ContentReference}"/> to load.</param>
		/// <param name="languageLoaderOption">The <see cref="LanguageLoaderOption"/> to use.</param>
		/// <param name="filterForVisitor">If the content should be filtered by visitor read access rights.</param>
		/// <param name="contentLoader">The <see cref="IContentLoader"/> to use when loading the <typeparamref name="TContentType"/> items.</param>
		/// <returns>A <see cref="IEnumerable{TContentType}"/> of all the valid <typeparamref name="TContentType"/> items.</returns>
		/// <remarks>There are two separate Get methods purely for performance reasons.</remarks>
		IEnumerable<TContentType> Get<TContentType>(IEnumerable<ContentReference> contentLinks, LanguageLoaderOption languageLoaderOption,
			bool filterForVisitor, IContentLoader contentLoader)
			where TContentType : IContentData;

		/// <summary>
		/// Loads a <see cref="EPiServer.Core.IContent"/> instance from a <see cref="EPiServer.Core.ContentReference"/> from a certain language, optionally filtered for visitor access
		/// </summary>
		/// <typeparam name="TContentType">Type of <see cref="EPiServer.Core.IContent"/> to load</typeparam>
		/// <param name="contentLink"><see cref="EPiServer.Core.ContentReference"/> of the content to load</param>
		/// <param name="languageLoaderOption">The <see cref="EPiServer.Core.LanguageLoaderOption"/> to use.</param>
		/// <param name="filterForVisitor">If the content should be filtered by visitor access rights.</param>
		/// <param name="contentLoader"><see cref="EPiServer.IContentLoader"/> to use.</param>
		/// <returns>A <see cref="EPiServer.Core.IContent"/> instance, or null if not found or the current user does not have access to the item.</returns>
		/// <remarks>There are two separate Get methods purely for performance reasons.</remarks>
		TContentType Get<TContentType>(ContentReference contentLink, LanguageLoaderOption languageLoaderOption, bool filterForVisitor,
			IContentLoader contentLoader)
			where TContentType : IContentData;

		/// <summary>
		/// Loads ancestors of the specificed type <typeparamref name="TContentType"/> for the provided <paramref name="contentLink"/>.
		/// </summary>
		/// <typeparam name="TContentType">Type of ancestors to load</typeparam>
		/// <param name="contentLink">The starting page to load from</param>
		/// <param name="filterForVisitor">If the content should be filtered by visitor access rights.</param>
		/// <param name="contentLoader"><see cref="EPiServer.IContentLoader"/> to use.</param>
		/// <param name="languageLoaderOption">The <see cref="EPiServer.Core.LanguageLoaderOption"/> to use.</param>
		/// <returns>
		/// A collection of <typeparamref name="TContentType"/> items, ordered by closest ancestor first (ie. parent of the <paramref name="contentLink"/>, 
		/// assuming it is of type <typeparamref name="TContentType"/>).
		/// </returns>
		IEnumerable<TContentType> GetAncestors<TContentType>(ContentReference contentLink, LanguageLoaderOption languageLoaderOption, bool filterForVisitor, IContentLoader contentLoader)
			where TContentType : IContent;


		/// <summary>
		/// Loads children of the specificed type <typeparamref name="TContentType"/> for the provided <paramref name="contentLink"/>
		/// </summary>
		/// <typeparam name="TContentType">Type of children to load</typeparam>
		/// <param name="contentLink">The parent to load from</param>
		/// <param name="languageLoaderOption">The <see cref="EPiServer.Core.LanguageLoaderOption"/> to use.</param>
		/// <param name="startIndex">Start index, useful when paging</param>
		/// <param name="maxRows">Max items to load</param>
		/// <param name="filterForVisitor">If the content should be filtered by visitor access rights.</param>
		/// <param name="contentLoader"><see cref="EPiServer.IContentLoader"/> to use.</param>
		/// <returns>A collection of <see cref="EPiServer.Core.IContent"/> items.</returns>
		IEnumerable<TContentType> GetChildren<TContentType>(ContentReference contentLink, LanguageLoaderOption languageLoaderOption,
			int startIndex, int maxRows, bool filterForVisitor, IContentLoader contentLoader)
			where TContentType : IContent;

		/// <summary>
		/// Loads all <see cref="EPiServer.Core.IContent"/> descendents of type <typeparamref name="TContentType"/> from a <see cref="EPiServer.Core.ContentReference"/> parent, from a certain language and filtered for visitor access
		/// </summary>
		/// <remarks>Please use with caution as this can potentially be a very expensive operation if a large amount of content items are retrieved</remarks>
		/// <typeparam name="TContentType">Type of content to load</typeparam>
		/// <param name="contentLink"><see cref="EPiServer.Core.ContentReference"/> parent to load</param>
		/// <param name="languageLoaderOption">The <see cref="EPiServer.Core.LanguageLoaderOption"/> to use.</param>
		/// <param name="filterForVisitor">If the content should be filtered by visitor access rights.</param>
		/// <param name="contentLoader"><see cref="EPiServer.IContentLoader"/> to use.</param>
		/// <param name="startIndex">Poisition to start takeing items from</param>
		/// <param name="maxRows">Maximum number of items to take</param>
		/// <returns>A <see cref="EPiServer.Core.IContent"/> instance</returns>
		IEnumerable<TContentType> GetDescendents<TContentType>(ContentReference contentLink, LanguageLoaderOption languageLoaderOption,
			bool filterForVisitor, IContentLoader contentLoader, int startIndex, int maxRows)
			where TContentType : IContentData;
	}
}