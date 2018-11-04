using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebStore.Helper
{
    public static class CustomHtmlExtensions
    {
        /// <summary>
        /// 必填 - 顯示星號並以紅色顯示.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="labelString">The label string.</param>
        /// <returns></returns>
        public static MvcHtmlString RequiredLabel(this HtmlHelper helper, string labelString)
        {
            return MvcHtmlString.Create($"<Label class='col-md-2 control-label'><label style = 'color:red' >*</label >" + labelString + "</Label >");
        }
    }
}