using System;
using System.Collections.Generic;

namespace DemoCompiler
{
    class Program
    {
        static void Main(string[] args)
        {


            // Create the source code
            string source = @"

using System.Collections.Generic;
using System.Linq;
namespace Foo
{

    public class Bar
    {
        static void Main(string[] args)
        {
            Bar.SayHello();
        }

        public static void SayHello()
        {
            System.Console.WriteLine(""Hello World"");
            var r = new System.Drawing.Rectangle(0,0,100,100);
            System.Console.WriteLine(r);
            System.Console.WriteLine( string.Join("","", Enumerable.Range(0,10).Select(n=>n.ToString()).ToArray() ) );

        }
    }
}
";

            // Setup for compiling
            var provider_options = new Dictionary<string, string>
                         {
                             {"CompilerVersion","v3.5"}
                         };
            var provider = new Microsoft.CSharp.CSharpCodeProvider(provider_options);

            var compiler_params = new System.CodeDom.Compiler.CompilerParameters();

            string outfile = "D:\\Foo.DLL";
            compiler_params.OutputAssembly = outfile ;
            compiler_params.GenerateExecutable = false;
            
            compiler_params.ReferencedAssemblies.Add("System.Drawing.Dll");
            compiler_params.ReferencedAssemblies.Add("System.Core.Dll");

            // Compile
            var results = provider.CompileAssemblyFromSource(compiler_params, source);

            // Print out any Errors

            Console.WriteLine("Output file: {0}", outfile);
            Console.WriteLine("Number of Errors: {0}", results.Errors.Count);
            foreach (System.CodeDom.Compiler.CompilerError err in results.Errors)
            {
                Console.WriteLine("ERROR {0}", err.ErrorText);

            }

        }
    }
}

    