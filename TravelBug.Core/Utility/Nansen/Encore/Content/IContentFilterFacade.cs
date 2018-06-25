using System.Collections.Generic;
using EPiServer.Core;

namespace TravelBug.Core.Utility.Nansen.Encore.Content
{
	/// <summary>
	/// Deifines how content should be filtered when using ex. <see cref="ContentUtility.FilterForVisitor{TContentType}"/>.
	/// </summary>
	public interface IContentFilterFacade
	{
		/// <summary>
		/// Determines wether the current visitor has read access to the specified content.
		/// </summary>
		bool QueryReadAccess<TContentType>(TContentType content) where TContentType : IContent;

		/// <summary>
		/// Filters content items and returns only those the current visitor has access to.
		/// </summary>
		/// <typeparam name="TContentType"></typeparam>
		/// <param name="content"></param>
		/// <returns></returns>
		IEnumerable<TContentType> FilterForVisitor<TContentType>(IEnumerable<TContentType> content) where TContentType : IContent;
	}
}