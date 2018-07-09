using EPiServer;
using EPiServer.Core;
using EPiServer.Core.Html.StringParsing;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using EPiServer.Web.Mvc.Html;
using System.Web.Mvc;
using static TravelBug.Core.Constants;

namespace TravelBug.Core.Rendering
{
	/// <summary>
	///     Extends the default <see cref="EPiServer.Web.Mvc.Html.ContentAreaRenderer" /> to apply custom CSS classes to each <see cref="ContentFragment" />.
	/// </summary>
	/// <remarks>
	///         ZG: Taken from encore and stripped of everything except display option rendering
	/// </remarks>
	public class CustomContentAreaRenderer : ContentAreaRenderer
	{
		private readonly string _containerCssClassKey = "cssclass";

		/// <summary>
		///     Initializes a new instance of the <see cref="CustomContentAreaRenderer" /> class.
		/// </summary>
		public CustomContentAreaRenderer(IContentRenderer contentRenderer, TemplateResolver templateResolver, IContentAreaItemAttributeAssembler attributeAssembler,
			IContentRepository contentRepository, IContentAreaLoader contentAreaLoader)
			: base(contentRenderer, templateResolver, attributeAssembler, contentRepository, contentAreaLoader)
		{
		}

		public override void Render(HtmlHelper htmlHelper, ContentArea contentArea)
		{
			//only checking for null, if may be empty on purpose to override defaults
			if (!htmlHelper.ViewContext.ViewData.ContainsKey(_containerCssClassKey) || htmlHelper.ViewContext.ViewData[_containerCssClassKey] == null)
			{
				htmlHelper.ViewContext.ViewData[_containerCssClassKey] = BaseContainerCssClass;
			}

			base.Render(htmlHelper, contentArea);
		}

		/// <summary>
		/// Gets the css class that should be used to render the <see cref="ContentAreaItem"/> based off of the <see cref="DisplayOption" /> that is defined for that item.
		/// </summary>
		protected override string GetContentAreaItemCssClass(HtmlHelper htmlHelper, ContentAreaItem contentAreaItem)
		{
			//retrieve display option ID if present on content area item. this id serves as the key to the BlockDisplayClasses dictionary. we do not need the entire DisplayOptions service here
			string displayOptionId;
			if (contentAreaItem.RenderSettings.ContainsKey(ContentFragment.ContentDisplayOptionAttributeName))
			{
				displayOptionId = contentAreaItem.RenderSettings[ContentFragment.ContentDisplayOptionAttributeName].ToString();
			}
			else
			{
				displayOptionId = null;
			}

			var cssClass = GetCssClassForDisplayOption(displayOptionId);

			// the base call should check for null, and not isnullorempty, as "" can be used to disable the default
			return base.GetContentAreaItemCssClass(htmlHelper, contentAreaItem) ?? $"{BaseChildrenCssClass} {cssClass}".Trim();
		}

		/// <summary>
		/// Gets the template tag for the <see cref="T:EPiServer.Core.ContentAreaItem" />. Unlike the base implementation, 
		/// display options are ignored in favor of the "Tag" property value on the content area itself in every case because we use display options to change CSS, not 
		/// the template that is used to render the content.
		/// </summary>
		/// <param name="htmlHelper">The html helper</param>
		/// <param name="contentAreaItem">The content area item</param>
		/// <returns>The template tag for the content area which contains this content area item</returns>
		protected override string GetContentAreaItemTemplateTag(HtmlHelper htmlHelper, ContentAreaItem contentAreaItem) => GetContentAreaTemplateTag(htmlHelper);

		private static string GetCssClassForDisplayOption(string tag)
		{
			if (string.IsNullOrEmpty(tag))
			{
				return null;
			}
			if (BlockDisplayClasses.ContainsKey(tag))
			{
				return BlockDisplayClasses[tag];
			}
			return null;
		}
	}
}
