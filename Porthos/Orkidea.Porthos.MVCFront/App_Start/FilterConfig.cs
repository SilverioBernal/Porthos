﻿using System.Web;
using System.Web.Mvc;

namespace Orkidea.Porthos.MVCFront
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}