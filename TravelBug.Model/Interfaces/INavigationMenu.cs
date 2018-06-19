using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPiServer.Core;

namespace TravelBug.Model.Interfaces
{
	public interface INavigationMenu : IContent
	{
		bool IsVisible { get; set; }
	}
}
