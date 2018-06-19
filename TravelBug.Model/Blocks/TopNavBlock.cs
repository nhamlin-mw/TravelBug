using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace TravelBug.Model.Blocks
{
	[ContentType(DisplayName = "TopNavBlock", GUID = "5eec3a03-19f0-46bb-9d1e-15874ffabfae", Description = "")]
	public class TopNavBlock : BlockData
	{

		[CultureSpecific]
		[Display(
			Name = "Parent Page",
			Description = "Select the parent of the pages you want to display",
			GroupName = SystemTabNames.Content,
			Order = 1)]
		public virtual PageReference ParentPage { get; set; }

	}
}