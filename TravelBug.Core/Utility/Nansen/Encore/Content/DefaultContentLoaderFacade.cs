using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using TravelBug.Core.Utility.Extensions;

namespace TravelBug.Core.Utility.Nansen.Encore.Content
{
	/// <summary>
	/// Default implementation of <see cref="IContentLoaderFacade"/>
	/// </summary>
	public class DefaultContentLoaderFacade : IContentLoaderFacade
	{
		/// <summary>
		/// Default <see cref="LanguageLoaderOption"/> used for this <see cref="IContentLoaderFacade"/>.
		/// </summary>
		protected virtual LanguageLoaderOption DefaultLoaderOptions => LanguageLoaderOption.FallbackWithMaster();

		/// <summary>
		/// Checks if the current visitor has access to a <see cref="EPiServer.Core.IContent"/> instance.
		/// </summary>
		/// <param name="content">The <see cref="EPiServer.Core.IContent"/> instance to check access for</param>
		/// <param name="contentFilterFacade"><see cref="IContentFilterFacade"/> to use</param>
		/// <returns>Wether the current visitor has access to the content instance or not.</returns>
		public virtual bool QueryReadAccess<TContentType>(TContentType content, IContentFilterFacade contentFilterFacade) where TContentType : IContent
		{
			if (content == null)
			{
				return false;
			}
			if (contentFilterFacade == null)
			{
				contentFilterFacade = ServiceLocator.Current.GetInstance<IContentFilterFacade>();
			}
			return contentFilterFacade.QueryReadAccess(content);
		}

		/// <summary>
		/// Filters content for the current visitor/user according to publish status and page access.
		/// </summary>
		/// <typeparam name="TContentType">Type of content queried </typeparam>
		/// <param name="content">the content items to filter</param>
		/// <param name="contentFilterFacade">The <see cref="IContentFilterFacade"/> to use</param>
		/// <returns>A filtered <see cref="IEnumerable{TContentType}"/></returns>
		public virtual IEnumerable<TContentType> FilterForVisitor<TContentType>(IEnumerable<TContentType> content, IContentFilterFacade contentFilterFacade) where TContentType : IContent
		{
			if (contentFilterFacade == null)
			{
				contentFilterFacade = ServiceLocator.Current.GetInstance<IContentFilterFacade>();
			}
			return contentFilterFacade.FilterForVisitor(content);
		}

		/// <summary>
		/// Loads <see cref="EPiServer.Core.IContent"/> from a <see cref="IEnumerable{ContentReference}"/> in a certain language, optionally filtered for visitor read access.
		/// </summary>
		/// <typeparam name="TContentType">Type of content to load</typeparam>
		/// <param name="contentLinks">An <see cref="IEnumerable{ContentReference}"/> to load.</param>
		/// <param name="languageLoaderOption">The <see cref="LanguageLoaderOption"/> to use.</param>
		/// <param name="filterForVisitor">If the content should be filtered by visitor read access rights.</param>
		/// <param name="contentLoader">The <see cref="IContentLoader"/> to use when loading the <typeparamref name="TContentType"/> items.</param>
		/// <returns>A <see cref="IEnumerable{TContentType}"/> of all the valid <typeparamref name="TContentType"/> items.</returns>
		public virtual IEnumerable<TContentType> Get<TContentType>(IEnumerable<ContentReference> contentLinks, LanguageLoaderOption languageLoaderOption, bool filterForVisitor, IContentLoader contentLoader)
			where TContentType : IContentData
		{
			if (contentLinks.IsNullOrEmpty())
			{
				return Enumerable.Empty<TContentType>();
			}

			if (contentLoader == null)
			{
				contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
			}

			if (languageLoaderOption == null)
			{
				languageLoaderOption = DefaultLoaderOptions;
			}

			IEnumerable<TContentType> content = new TContentType[0];
			if (contentLinks.Count() == 1)
			{
				try
				{
					content = new[] { contentLoader.Get<TContentType>(contentLinks.Single(), new LoaderOptions { languageLoaderOption }) };
				}
				catch (ContentNotFoundException)
				{
				}
				catch (TypeMismatchException)
				{
				}
				catch (ArgumentNullException)
				{
				}
			}
			else
			{
				content = contentLoader.GetItems(contentLinks.Where(cl => !ContentReference.IsNullOrEmpty(cl)), new LoaderOptions { languageLoaderOption })
					.OfType<TContentType>();
			}
			if (filterForVisitor)
			{
				return content.OfType<IContent>().FilterForVisitor().OfType<TContentType>();
			}
			return content;
		}

		/// <summary>
		/// Loads a <see cref="EPiServer.Core.IContent"/> instance from a <see cref="EPiServer.Core.ContentReference"/> from a certain language, optionally filtered for visitor access
		/// </summary>
		/// <typeparam name="TContentType">Type of <see cref="EPiServer.Core.IContent"/> to load</typeparam>
		/// <param name="contentLink"><see cref="EPiServer.Core.ContentReference"/> of the content to load</param>
		/// <param name="languageLoaderOption">The <see cref="EPiServer.Core.LanguageLoaderOption"/> to use.</param>
		/// <param name="filterForVisitor">If the content should be filtered by visitor access rights.</param>
		/// <param name="contentLoader"><see cref="EPiServer.IContentLoader"/> to use.</param>
		/// <returns>A <see cref="EPiServer.Core.IContent"/> instance, or null if not found or the current user does not have access to the item.</returns>
		public virtual TContentType Get<TContentType>(ContentReference contentLink, LanguageLoaderOption languageLoaderOption, bool filterForVisitor, IContentLoader contentLoader) where TContentType : IContentData
		{
			if (ContentReference.IsNullOrEmpty(contentLink))
			{
				return default(TContentType);
			}

			if (contentLoader == null)
			{
				contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
			}

			if (languageLoaderOption == null)
			{
				languageLoaderOption = DefaultLoaderOptions;
			}

			try
			{
				var content = contentLoader.Get<TContentType>(contentLink, new LoaderOptions { languageLoaderOption });
				return filterForVisitor ? new[] { content as IContent }.FilterForVisitor().OfType<TContentType>().FirstOrDefault() : content;
			}
			catch (ContentNotFoundException)
			{
			}
			catch (TypeMismatchException)
			{
			}
			return default(TContentType);
		}

		/// <summary>
		/// Loads ancestors of the specificed type <typeparamref name="TContentType"/> for the provided <paramref name="contentLink"/>.
		/// </summary>
		/// <typeparam name="TContentType">Type of ancestors to load</typeparam>
		/// <param name="contentLink">The starting page to load from</param>
		/// <param name="contentLoader"><see cref="EPiServer.IContentLoader"/> to use.</param>
		/// <param name="languageLoaderOption">The <see cref="EPiServer.Core.LanguageLoaderOption"/> to use.</param>
		/// <param name="filterForVisitor">If the content should be filtered by visitor access rights.</param>
		/// <returns>
		/// A collection of <typeparamref name="TContentType"/> items, ordered by closest ancestor first (ie. parent of the <paramref name="contentLink"/>, 
		/// assuming it is of type <typeparamref name="TContentType"/>).
		/// </returns>
		public virtual IEnumerable<TContentType> GetAncestors<TContentType>(ContentReference contentLink, LanguageLoaderOption languageLoaderOption, bool filterForVisitor, IContentLoader contentLoader) where TContentType : IContent
		{
			if (ContentReference.IsNullOrEmpty(contentLink))
			{
				return Enumerable.Empty<TContentType>();
			}

			if (contentLoader == null)
			{
				contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
			}

			// use the built-in method if no language settings are supplied
			if (languageLoaderOption == null)
			{
				var ancestors = contentLoader.GetAncestors(contentLink).OfType<TContentType>();
				if (filterForVisitor)
				{
					return FilterForVisitor(ancestors, null);
				}
				return ancestors;
			}

			if (ContentReference.IsNullOrEmpty(contentLink))
			{
				return Enumerable.Empty<TContentType>();
			}

			var contentItems = new Collection<TContentType>();
			if (!ContentReference.IsNullOrEmpty(contentLink))
			{
				var content = Get<TContentType>(contentLink, languageLoaderOption, filterForVisitor, contentLoader);
				if (content != null)
				{
					TContentType item;
					for (var parentLink = content.ParentLink; !ContentReference.IsNullOrEmpty(parentLink); parentLink = item.ParentLink)
					{
						item = Get<TContentType>(parentLink, languageLoaderOption, filterForVisitor, contentLoader);
						if (item != null)
						{
							contentItems.Add(item);
						}
						else
						{
							break;
						}
					}
				}
			}
			return contentItems;
		}

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
		public virtual IEnumerable<TContentType> GetChildren<TContentType>(ContentReference contentLink, LanguageLoaderOption languageLoaderOption, int startIndex, int maxRows, bool filterForVisitor, IContentLoader contentLoader) where TContentType : IContent
		{
			if (ContentReference.IsNullOrEmpty(contentLink))
			{
				return Enumerable.Empty<TContentType>();
			}

			if (contentLoader == null)
			{
				contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
			}

			if (languageLoaderOption == null)
			{
				//TODO: fallback or fallbackwithmaster?
				languageLoaderOption = DefaultLoaderOptions;
			}

			try
			{
				// we have to get all children and do a skip/take afterward if filtering for visitor
				if (filterForVisitor)
				{
					var content = contentLoader.GetChildren<TContentType>(contentLink, new LoaderOptions { languageLoaderOption });
					content = FilterForVisitor(content, null);
					return content.Skip(startIndex).Take(maxRows);
				}
				return contentLoader.GetChildren<TContentType>(contentLink, new LoaderOptions { languageLoaderOption }, startIndex, maxRows);
			}
			catch (ArgumentException)
			{
			}
			catch (ContentNotFoundException)
			{
			}
			catch (TypeMismatchException)
			{
			}
			return Enumerable.Empty<TContentType>();
		}

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
		public virtual IEnumerable<TContentType> GetDescendents<TContentType>(ContentReference contentLink, LanguageLoaderOption languageLoaderOption, bool filterForVisitor, IContentLoader contentLoader, int startIndex, int maxRows) where TContentType : IContentData
		{
			if (ContentReference.IsNullOrEmpty(contentLink))
			{
				return Enumerable.Empty<TContentType>();
			}

			if (contentLoader == null)
			{
				contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
			}

			var contentLinks = contentLoader.GetDescendents(contentLink);

			// this should be faster if we do not have to filter
			if (!filterForVisitor)
			{
				contentLinks = contentLinks.Skip(startIndex).Take(maxRows);
			}

			if (!contentLinks.IsNullOrEmpty())
			{
				var content = Get<TContentType>(contentLinks, languageLoaderOption, filterForVisitor, contentLoader);
				// must do the skip/take after filtering for visitor
				if (filterForVisitor)
				{
					return content.Skip(startIndex).Take(maxRows);
				}
				return content;
			}

			return Enumerable.Empty<TContentType>();
		}
	}
}