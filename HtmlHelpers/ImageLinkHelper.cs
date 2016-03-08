using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WDTAssignment2NWBA.HtmlHelpers
{
    public static class ImageLinkHelper
    {
        /*
         * Reference:
         * ----------
         * This helper method uses code from the asp.net forum found on this link
         * http://forums.asp.net/t/1854075.aspx
         * 
         * Usage.
         * 1. You can then add the following namespace of Helpers in the asp.net MVC aspx template like the following
         *    @using WDTAssignment2.HtmlHelpers;
         * 2. Then whenever you need to call an Image which needs to be a link you can do something like the following
         *    @Html.ImageLink("Link to page", "../../Images/logo.png", "Alternative Text", new { id = 0 })
         */
        /// <summary>
        /// A Simple ActionLink Image
        /// </summary>
        /// <param name="actionName">name of the action in controller</param>
        /// <param name="imgUrl">url of the image</param>
        /// <param name="alt">alt text of the image</param>
        /// <returns></returns>
        public static MvcHtmlString ImageLink(this HtmlHelper helper, string actionName, string imgUrl, string alt)
        {
            return ImageLink(helper, actionName, imgUrl, alt, null, null, null);
        }

        /// <summary>
        /// A Simple ActionLink Image
        /// </summary>
        /// <param name="actionName">name of the action in controller</param>
        /// <param name="imgUrl">url of the iamge</param>
        /// <param name="alt">alt text of the image</param>
        /// <returns></returns>
        public static MvcHtmlString ImageLink(this HtmlHelper helper, string actionName, string imgUrl, string alt, object routeValues)
        {
            return ImageLink(helper, actionName, imgUrl, alt, routeValues, null, null);
        }

        /// <summary>
        /// A Simple ActionLink Image
        /// </summary>
        /// <param name="actioNName">name of the action in controller</param>
        /// <param name="imgUrl">url of the image</param>
        /// <param name="alt">alt text of the image</param>
        /// <param name="linkHtmlAttributes">attributes for the link</param>
        /// <param name="imageHtmlAttributes">attributes for the image</param>
        /// <returns></returns>
        public static MvcHtmlString ImageLink(this HtmlHelper helper, string actioNName, string imgUrl, string alt, object routeValues, object linkHtmlAttributes, object imageHtmlAttributes)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(actioNName, routeValues);

            //Create the link
            var linkTagBuilder = new TagBuilder("a");
            linkTagBuilder.MergeAttribute("href", url);
            linkTagBuilder.MergeAttributes(new RouteValueDictionary(linkHtmlAttributes));

            //Create image
            var imageTagBuilder = new TagBuilder("img");
            imageTagBuilder.MergeAttribute("src", urlHelper.Content(imgUrl));
            imageTagBuilder.MergeAttribute("alt", urlHelper.Content(alt));
            imageTagBuilder.MergeAttributes(new RouteValueDictionary(imageHtmlAttributes));

            //Add image to link
            linkTagBuilder.InnerHtml = imageTagBuilder.ToString(TagRenderMode.SelfClosing);

            string htmlLinkTagBuilder = linkTagBuilder.ToString();

            return MvcHtmlString.Create(htmlLinkTagBuilder);
        }
    }
}