using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Word = Microsoft.Office.Interop.Word; 

namespace DemoOfficeCollectionsIEnumerable
{
    class Program
    {

        static void Main(string[] args)
        {
            before();
            after_with_an_extension_method();

        }

        public static void before()
        {

            // Start Word and get Application object 
            var Wd = new Word.Application(); 

            // Define documents 
            var doc1 = Wd.Documents.AddEmptyDoc();
            var doc2 = Wd.Documents.AddEmptyDoc(); 

            // Use Linq to access the document names. 
            var query = from d in Wd.Documents.Cast<Word.Document>() 
                        select d.Name; 
            var docsnames1 = query.ToList(); 

            // Or use Lambda expressions 
            var query2 = Wd.Documents.Cast<Word.Document>().Select(d=> d.Name);
            var docnames2 = query2.ToList(); 


        }



        public static void after_with_an_extension_method()
        {
            // Add to the top of the code file 

            object missingValue = System.Reflection.Missing.Value;

            // Start Word and get Application object 
            var Wd = new Word.Application();

            // Define documents 
            var doc1 = Wd.Documents.AddEmptyDoc();
            var doc2 = Wd.Documents.AddEmptyDoc(); 

            // Use Linq to access the document names. 
            var query = from d in Wd.Documents.AsEnumerable()
                        select d.Name;
            var docsnames1 = query.ToList();

            // Or use Lambda expressions 
            var query2 = Wd.Documents.AsEnumerable().Select(d => d.Name);
            var docnames2 = query2.ToList();


        }


    }

    public static class WordExtensions
    {
        public static IEnumerable<Word.Document> AsEnumerable(this Word.Documents docs)
        {
            return docs.Cast<Word.Document>();
        }

        public static Word.Document AddEmptyDoc(this Word.Documents docs)
        {
            object missingValue = System.Reflection.Missing.Value;
            return docs.Add(ref missingValue, ref missingValue,
                                   ref missingValue, ref missingValue);
        }
    }

}
