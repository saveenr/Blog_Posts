using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Data;
using System.Linq;

using Microsoft.Dynamics.Framework.Reports;
public partial class Report1
{


    [DataMethod(), AxSessionPermission(SecurityAction.Assert)]
    public static System.Data.DataTable DataMethod_Series_0()
    {
        int num_values = 14;
        double min_value = 0;
        double max_value = 100;
        double start_value = 30;
        double end_value = 70;
        double factor = 0.10;

        var values = generate_random_sequence(num_values, min_value, max_value, start_value, end_value, factor);

        var records = values.Select((value) => new { NumWidgets = (int) value } );

        var datatable = Isotope.Data.DataUtil.DataTableFromEnumerable(records);
        return datatable;

    }

    [DataMethod(), AxSessionPermission(SecurityAction.Assert)]
    public static System.Data.DataTable DataMethod_Series_1()
    {
        int num_values = 14;
        double min_value = 0;
        double max_value = 100;
        double start_value = 30;
        double end_value = 70;
        double factor = 0.10;

        var values = generate_random_sequence(num_values, min_value, max_value, start_value, end_value, factor);

        var records = values.Select((value, index) => new { Index = index, NumWidgets = (int) value });
            
        var datatable = Isotope.Data.DataUtil.DataTableFromEnumerable(records);
        return datatable;

    }



    public static IEnumerable<double> generate_random_sequence(int num_values, double min_value, double max_value,
        double start_value, double end_value, double factor)
    {

        double step = (end_value - start_value) / (num_values - 1);
        var r = new System.Random();

        var indices = Enumerable.Range(0, num_values);

        var values = from i in indices
                     let delta = (r.NextDouble() * (max_value - min_value) * factor) - (factor * 0.5)
                     let v = start_value + (i * step)
                     select v + delta;

        var normalized_values = from value in values
                                select System.Math.Min(max_value, System.Math.Max(min_value, value));

        return normalized_values;

    }

}
