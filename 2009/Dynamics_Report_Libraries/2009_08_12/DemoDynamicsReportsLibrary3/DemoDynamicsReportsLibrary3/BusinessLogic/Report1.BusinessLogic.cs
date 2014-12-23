using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Data;
using Microsoft.Dynamics.Framework.Reports;
using System.Linq;

public partial class Report1
{

    [DataMethod(), AxSessionPermission(SecurityAction.Assert)]
        public static System.Data.DataTable DataMethod1()
        {
            var favorite_blogs = new []
            {
                new { 
                        Name="Visio Guy", 
                        Url ="http://www.visguy.com/"},
                new { 
                        Name="Greg's Cool [Insert Clever Name] of the Day", 
                        Url ="http://coolthingoftheday.blogspot.com/"},
                new { 
                        Name="DisplayBlog", 
                        Url ="http://www.displayblog.com/"}

            };

            var datatable = Isotope.Data.DataUtil.DataTableFromEnumerable(favorite_blogs);
            return datatable;
        }

}

