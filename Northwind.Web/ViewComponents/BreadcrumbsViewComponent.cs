using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Northwind.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Web.ViewComponents
{
    public class BreadcrumbsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Breadcrumb breadcrumb)
        {
            return View(breadcrumb);
        }
    }
}
