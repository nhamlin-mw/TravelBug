using System.Collections.Generic;
using System.Linq;

namespace TravelBug.Core.Utility.Extensions
{
	public static class IEnumerableExtensions
	{
		/// <summary>
		/// Returns whether the IEnumerable{TSource} is null or empty
		/// </summary>
		/// <typeparam name="TSource">Source Type</typeparam>
		/// <param name="source">IEnumerable to be compared against</param>
		/// <returns></returns>
		public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> source)
		{
			return source == null || !source.Any();
		}
	}
}
