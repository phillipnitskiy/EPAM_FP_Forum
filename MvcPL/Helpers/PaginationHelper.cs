using MvcPL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MvcPL.Helpers
{
    public static class PaginationHelper
    {

        public static MvcHtmlString Pagination(this HtmlHelper html, PageInfoModel pageInfo, Func<int, string> pageUrl)
        {
            var pagination = new TagBuilder("ul");
            pagination.AddCssClass("pagination pagination-sm");

            var pages = new StringBuilder();
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                var li = new TagBuilder("li");
                if (i == pageInfo.PageNumber)
                {
                    li.AddCssClass("active");
                }

                var a = new TagBuilder("a");
                a.MergeAttribute("href", pageUrl(i));
                a.SetInnerText(i.ToString());

                li.InnerHtml = a.ToString();

                pages.Append(li.ToString());
            }
            pagination.InnerHtml += pages.ToString();

            return MvcHtmlString.Create(pagination.ToString());
        }

    }
}