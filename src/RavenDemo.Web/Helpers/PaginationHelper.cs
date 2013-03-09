using System.Text;
using System.Web.Mvc;
using RavenDemo.Web.Models;

namespace RavenDemo.Web.Helpers
{
    public static class PaginationHelper
    {
        public static MvcHtmlString Pagination<T>(this HtmlHelper helper, PagedList<T> pagedList)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<div class='pagination'>");
            sb.AppendLine("<ul>");

            string cssClass = pagedList.CurrentPage < 2 ? "disabled" : string.Empty;
            sb.AppendFormat("<li class={0}><a href='{1}'>&laquo;</a></li>", cssClass, pagedList.CurrentPage - 1);
            for (int page = 1; page < pagedList.NumberOfPages + 1; page++)
            {
                cssClass = page == pagedList.CurrentPage ? "active" : string.Empty;
                sb.AppendFormat("<li class='{0}'><a href='{1}'>{1}</a></li>", cssClass, page);
            }
            cssClass = pagedList.CurrentPage >= pagedList.NumberOfPages ? "disabled" : string.Empty;
            sb.AppendFormat("<li class='{0}'><a href='{1}'>»</a></li>", cssClass, pagedList.CurrentPage + 1);
            sb.AppendLine("</ul>");
            sb.AppendLine("</div>");
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}