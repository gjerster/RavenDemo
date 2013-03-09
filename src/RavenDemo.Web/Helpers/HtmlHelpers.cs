using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace RavenDemo.Web.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString SetFocusTo<TModel, TProperty>(
                        this HtmlHelper<TModel> html,
                        Expression<Func<TModel, TProperty>> expression)
        {
            var prop = ExpressionHelper.GetExpressionText(expression);
            return html.SetFocusToHtml(prop);
        }

        public static MvcHtmlString SetFocusTo(this HtmlHelper html,
                                               string propertyName)
        {
            var prop = ExpressionHelper.GetExpressionText(propertyName);
            return html.SetFocusToHtml(prop);
        }

        private static MvcHtmlString SetFocusToHtml(this HtmlHelper html,
                                                string property)
        {
            string id = html.ViewData.TemplateInfo.GetFullHtmlFieldId(property);
            var script = "<script type='text/javascript'>" +
                            "$(function() {" +
                                "$('#" + id + "').focus();" +
                            "});" +
                            "</script>";
            return MvcHtmlString.Create(script);
        }

    }

}