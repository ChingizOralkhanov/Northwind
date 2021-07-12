using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Web.Helpers
{
    public static class NorthwindImageHelper
    {
        public static HtmlString Image(this IHtmlHelper helper, int id)
        {
            StringBuilder result = new StringBuilder();
            result.Append("<a>");
            result.Append($"src=\"{id}\"");
            return new HtmlString(result.ToString());
        }
    }
}
