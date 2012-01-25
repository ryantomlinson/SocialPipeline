using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace SocialPipeline.Web.Custom.WebHelpers
{
    public static class SocialPipelineMvcHelpers
    {
        /// <summary>
        /// Creates a link that will open a jQuery UI dialog form.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="linkText">The inner text of the anchor element</param>
        /// <param name="dialogContentUrl">The url that will return the content to be loaded into the dialog window</param>
        /// <param name="dialogTitle">The title to be displayed in the dialog window</param>
        /// <param name="updateTargetId">The id of the div that should be updated after the form submission</param>
        /// <param name="updateUrl">The url that will return the content to be loaded into the traget div</param>
        /// <returns></returns>
        public static MvcHtmlString DialogFormLink(this HtmlHelper htmlHelper, string linkText, string dialogContentUrl,
            string dialogId, string dialogTitle, string updateTargetId, string updateUrl)
        {
            TagBuilder builder = new TagBuilder("a");
            builder.SetInnerText(linkText);
            builder.Attributes.Add("href", dialogContentUrl);            
            builder.Attributes.Add("data-dialog-title", dialogTitle);
            builder.Attributes.Add("data-update-target-id", updateTargetId);
            builder.Attributes.Add("data-update-url", updateUrl);

            // Add a css class named dialogLink that will be
            // used to identify the anchor tag and to wire up
            // the jQuery functions
            builder.AddCssClass("dialogLink");

            return new MvcHtmlString(builder.ToString());
        }   

    }
}
