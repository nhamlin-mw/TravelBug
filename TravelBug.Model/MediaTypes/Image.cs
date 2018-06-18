using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Framework.DataAnnotations;

namespace TravelBug.Model.MediaTypes
{
	[ContentType(DisplayName = "ImageData", GUID = "e9db569e-a90a-45d9-8137-2b79b42b3470", Description = "")]
	[MediaDescriptor(ExtensionString = "jpg,jpeg,jpe,ico,gif,bmp,png,svg")]
	public class Image : ImageData
	{
		[Display(Name="Alt Text", Order=10, GroupName = SystemTabNames.Settings)]
		public virtual string AltText { get; set; }
	}
}