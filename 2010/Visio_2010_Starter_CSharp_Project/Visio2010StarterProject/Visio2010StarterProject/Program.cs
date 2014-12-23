using System;
using System.Collections.Generic;
using System.Linq;
using  IVisio = Microsoft.Office.Interop.Visio;

namespace Visio2010StarterProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var visapp = new IVisio.Application();
            var docs = visapp.Documents;
            var doc = docs.Add(""); // add an empty doc

            short stencil_flags = (short)IVisio.VisOpenSaveArgs.visOpenRO | (short)IVisio.VisOpenSaveArgs.visOpenDocked;
            var basic_stencil = docs.OpenEx("basic_u.vss", stencil_flags);
            var rounded_rect_master = basic_stencil.Masters["Rounded Rectangle"];

            var page = visapp.ActivePage;

            var shape1 = page.DrawRectangle(1, 1, 3, 2);
            shape1.Text = "Hello World 1";

            var shape2 = page.Drop(rounded_rect_master,2, 4);
            shape2.Text = "Hello World 2";

        }
    }
}
