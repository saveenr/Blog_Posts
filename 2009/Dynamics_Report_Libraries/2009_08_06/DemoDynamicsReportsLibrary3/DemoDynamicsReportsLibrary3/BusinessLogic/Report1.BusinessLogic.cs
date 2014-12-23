using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Data;
using Microsoft.Dynamics.Framework.Reports;
using System.Linq;

public partial class Report1
{
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

        var records = values.Select((value, index) => new { Index = index, NumWidgets = (int)value });
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

        [DataMethod(), AxSessionPermission(SecurityAction.Assert)]
        public static byte[] DataMethod1()
        {
            using (var bmp = new System.Drawing.Bitmap(600, 200))
            {
                var brush_white = System.Drawing.Brushes.White;

                using (var g = System.Drawing.Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                    var f = new System.Drawing.Font("Courier", 36.0f);
                    g.DrawRectangle(System.Drawing.Pens.Red, 10, 10, 400, 100);


                }

                var memstream = new System.IO.MemoryStream();
                bmp.Save(memstream, System.Drawing.Imaging.ImageFormat.Png);
                var bytearray = memstream.ToArray();
                return bytearray;
            }

        }


        [DataMethod(), AxSessionPermission(SecurityAction.Assert)]
        public static byte[] DataMethod2()
        {
            using (var bmp = Demo.DemoBitmap_colorful_header( new System.Drawing.Size(400,200) ))
            {

                var memstream = new System.IO.MemoryStream();
                bmp.Save(memstream, System.Drawing.Imaging.ImageFormat.Png);
                var bytearray = memstream.ToArray();
                return bytearray;
            }
        }


}

public static class Demo
{

    public static System.Drawing.Bitmap DemoBitmap_colorful_header(System.Drawing.Size size)
    {
        var bmp = new System.Drawing.Bitmap(size.Width, size.Height);
        var brush_white = System.Drawing.Brushes.White;

        using (var g = System.Drawing.Graphics.FromImage(bmp))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.Fill(brush_white);

            var f = new System.Drawing.Font("Calibri", 26.0f);


            g.FillEllipseFromCenter(
                new System.Drawing.Point(0, size.Height),
                new System.Drawing.Size(size.Height * 2, size.Height * 2),
                System.Drawing.Color.DodgerBlue, System.Drawing.Color.Transparent,
                0.0f);


            g.FillEllipseFromCenter(
            new System.Drawing.Point(size.Width, size.Height),
            new System.Drawing.Size(size.Width, size.Height * 2),
            System.Drawing.Color.GreenYellow, System.Drawing.Color.Transparent,
            0.0f);


            g.FillEllipseFromCenter(
            new System.Drawing.Point(size.Width / 2, size.Height),
            new System.Drawing.Size(size.Width, size.Height * 2),
            System.Drawing.Color.Coral, System.Drawing.Color.Transparent,
            0.0f);

            g.DrawString("My Fancy Report Title", f, brush_white, 10, 50);

        }

        return bmp;
    }
}


public static class SystemDrawingExtensions
{
    public static void Fill(this System.Drawing.Graphics g, System.Drawing.Brush brush)
    {
        var cb = g.VisibleClipBounds;
        g.FillRectangle(brush, cb);
    }

    public static System.Drawing.Rectangle RectangleFromCenter(this System.Drawing.Point center, System.Drawing.Size size)
    {
        int hw = size.Width / 2;
        int hh = size.Height / 2;
        var rect = new System.Drawing.Rectangle(center.X - hw, center.Y - hh, size.Width, size.Height);
        return rect;
    }

    public static void FillEllipseFromCenter(this System.Drawing.Graphics g, System.Drawing.Point center, System.Drawing.Size size, System.Drawing.Color centercolor, System.Drawing.Color edgecolor, float focus)
    {

        using (var pth = new System.Drawing.Drawing2D.GraphicsPath())
        {
            var rect = center.RectangleFromCenter(size);
            g.FillEllipse(rect, centercolor, edgecolor, focus);
        }

    }

    public static void FillEllipse(this System.Drawing.Graphics g, System.Drawing.Rectangle rect, System.Drawing.Color centercolor, System.Drawing.Color edgecolor, float focus)
    {

        using (var pth = new System.Drawing.Drawing2D.GraphicsPath())
        {
            pth.AddEllipse(rect);
            var pgb = new System.Drawing.Drawing2D.PathGradientBrush(pth);
            pgb.CenterColor = centercolor;
            pgb.SurroundColors = new[] { edgecolor };
            pgb.FocusScales = new System.Drawing.PointF(focus, focus);

            g.FillEllipse(pgb, rect);
        }

    }
}
