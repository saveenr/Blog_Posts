using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Data;
using System.Linq;

using Microsoft.Dynamics.Framework.Reports;
public partial class Report1
{
    [DataMethod(), AxSessionPermission(SecurityAction.Assert)]
    public static System.Data.DataTable DataMethod_Ages2()
    {

        var records = new[]
        {
            new { Name="Akuma", Age=78 },
            new { Name="Ryu", Age=28 },
            new { Name="Ken", Age=29 },
            new { Name="Guile", Age=25 },
            new { Name="M.Bison", Age=41 }
        
        };
        var datatable = Isotope.Data.DataUtil.DataTableFromEnumerable(records);

        return datatable;

    }

    [DataMethod(), AxSessionPermission(SecurityAction.Assert)]
    public static System.Data.DataTable DataMethod_Ages3()
    {
        int max_age = 60;
        int min_age = 18;

        var r = new System.Random();
        var names = new string[] {      "Akuma", "Ryu", "Ken", 
                                        "Guile", "M.Bison" , 
                                        "Chun-Li", "Cammy" , 
                                        "Blanka", "E.Honda",
                                        "Gen", "Birdie", "Adon",
                                        "Sagat", "Dhalsim", "Zangief",
                                        "Balrog", "Vega"};

        var records = from name in names
                      select new { Name = name, Age = r.Next(min_age, max_age) };

        var datatable = Isotope.Data.DataUtil.DataTableFromEnumerable(records);

        return datatable;

    }

    [DataMethod(), AxSessionPermission(SecurityAction.Assert)]
    public static System.Data.DataTable DataMethod_Ages_and_Weights()
    {
        int max_age = 60;
        int min_age = 18;

        int min_weight = 140; // weight in pounds
        int max_weight = 240;

        var r = new System.Random();
        var names = new string[] {      "Akuma", "Ryu", "Ken", 
                                        "Guile", "M.Bison" , 
                                        "Chun-Li", "Cammy" , 
                                        "Blanka", "E.Honda",
                                        "Gen", "Birdie", "Adon",
                                        "Sagat", "Dhalsim", "Zangief",
                                        "Balrog", "Vega"};

        var records = from name in names
                      select new {      Name = name, 
                                        Age = r.Next(min_age, max_age) ,
                                        Weight = r.Next(min_weight,max_weight) };

        var datatable = Isotope.Data.DataUtil.DataTableFromEnumerable(records);

        return datatable;

    }

}
