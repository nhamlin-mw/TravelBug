using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;

namespace TravelBug.Model.Pages
{
	[ContentType(DisplayName = "StartPage", GUID = "fb200427-d190-49a3-9200-cc120f3d22bb", Description = "")]
	public class StartPage : PageData
	{
		/*
				[CultureSpecific]
				[Display(
					Name = "Main body",
					Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
					GroupName = SystemTabNames.Content,
					Order = 1)]
				public virtual XhtmlString MainBody { get; set; }
		 */
	}
}