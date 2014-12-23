using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Data;
using System.Linq;

using Microsoft.Dynamics.Framework.Reports;
public partial class Report1
{

    [DataMethod(), AxSessionPermission(SecurityAction.Assert)]
    public static System.Data.DataTable DataMethod_Patient_Data()
    {
        int max_age = 60;
        int min_age = 18;

        int min_weight = 140; // weight in pounds
        int max_weight = 240;


        var areas = new[] { "Thoracic Surgery", "Emergency", "ENT" };
        var doctors = new [] { "Reddy", "Jackson", "Tordilla" };

        var r = new System.Random();
        var names = new string[] {      "Akuma", "Ryu", "Ken", 
                                        "Guile", "M.Bison" , 
                                        "Chun-Li", "Cammy" , 
                                        "Blanka", "E.Honda",
                                        "Gen", "Birdie", "Adon",
                                        "Sagat", "Dhalsim", "Zangief",
                                        "Balrog", "Vega"};

        var records = from name in names
                      select new
                      {
                          Name = name,
                          Age = r.Next(min_age, max_age),
                          Weight = r.Next(min_weight, max_weight),
                          Area = areas[ r.Next(0, areas.Length) ],
                          Doctor = doctors [ r.Next(0, doctors.Length)] ,
                          Hypertension = (r.NextDouble() > 0.9) ? true : false,
                          Diabetes = (r.NextDouble() > 0.85) ? true : false,
                          Asthma = (r.NextDouble() > 0.75) ? true : false
                      };

        var datatable = Isotope.Data.DataUtil.DataTableFromEnumerable(records);

        return datatable;

    }


}
