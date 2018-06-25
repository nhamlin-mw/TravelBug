using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StructureMap;

namespace TravelBug.Core.Utility
{
	public class StructureMapDependencyResolver : IDependencyResolver, IServiceProvider
	{
		/// <summary>
		///     The underlying container used by the DI provider.
		/// </summary>
		private readonly IContainer Container;

		/// <summary>
		///     Initializes a new instance of the <see cref="StructureMapDependencyResolver" /> class.
		/// </summary>
		/// <param name="container">The container.</param>
		/// <exception cref="System.ArgumentNullException">container</exception>
		public StructureMapDependencyResolver(IContainer container)
		{
			Container = container ?? throw new ArgumentNullException(nameof(container));
		}

		/// <summary>
		///     Retrieves a collection of services from the scope.
		/// </summary>
		/// <param name="serviceType">The collection of services to be retrieved.</param>
		/// <returns>
		///     The retrieved collection of services.
		/// </returns>
		public virtual IEnumerable<object> GetServices(Type serviceType)
		{
			return Container.GetAllInstances(serviceType).Cast<object>();
		}

		/// <summary>
		///     Implementation of <see cref="M:System.IServiceProvider.GetService(System.Type)" />.
		/// </summary>
		/// <param name="serviceType">The requested service.</param>
		/// <returns>
		///     The requested object.
		/// </returns>
		public virtual object GetService(Type serviceType)
		{
			if (serviceType == null)
			{
				return null;
			}

			// as per http://world.episerver.com/documentation/Items/Developers-Guide/Episerver-CMS/9/Initialization/dependency-injection/
			try
			{
				return Container.GetInstance(serviceType);
			}
			catch (StructureMapException)
			{
				return null;
			}
			catch (InvalidOperationException)
			{
				// is this the correct way of dealing with this exception during site startup?
				return Container.TryGetInstance(serviceType);
			}
		}

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public virtual void Dispose()
		{
			Container?.Dispose();
		}
	}
}
