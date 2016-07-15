using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CodeExecutor;
using WrapperForExecution.Second;

namespace WrapperForExecution
{
    class Program
    {
        static void Main(string[] args)
        {
            Bat r1 = new Bat();
            Rat r = new Rat();
            int a = 5;
            string s = "mystring";
            float f = 3.14F;
            double d = 3.14182019;
            //object[] argsToPass = new object[] { a, s, r1, f, d, r };

            NVPair[] argsToPass = new NVPair[]
            {
                new NVPair("a", a),
                new NVPair("s", s),
                new NVPair("f", f),
                new NVPair("d", d),
                new NVPair("bat", r1),
                new NVPair("rat", r),
            };

            ExecutionUnit eUnit = new ExecutionUnit();

            eUnit.AddReferences(r.GetType().Assembly);
            eUnit.Execute("Rat rat = new Rat(); rat.Squeak();", null);

            eUnit.AddNameSpaceImports("WrapperForExecution.Second");
            eUnit.Execute("Bat bat = new Bat(); bat.Screech();", null);
            if (eUnit.Result!=null)
            {
                Console.WriteLine(eUnit.Result.ToString());
            }

            
            //eUnit.ClearReferences();

            eUnit.AddNameSpaceImports("WrapperForExecution");
            eUnit.Execute("Rat rat = new Rat(); rat.Squeak();", new NVPair("x", r1));
            eUnit.ClearReferences();
            eUnit.AddNameSpaceImports("WrapperForExecution");
            eUnit.Execute("Rat rat = new Rat(); rat.Squeak();", new NVPair("x", r1));
            eUnit.ClearReferences();
            eUnit.AddNameSpaceImports("WrapperForExecution");
            eUnit.Execute("y.Screech(); y.UpdateText();", new NVPair("y", r1));
            r1.Screech();
            //eUnit.ClearReferences();
            //eUnit.AddNameSpaceImports("WrapperForExecution");
            //eUnit.AddReferences(typeof(Rat).Assembly);

            



            //            eUnit.AddReferences(r.GetType().Assembly, r1.GetType().Assembly);
            //            eUnit.Execute(
            //@"
            //Rat r = new Rat(); 
            //Bat b = new Bat(), c;
            //r.Squeak();
            //b.Screech();
            //c = new Bat();
            //c.UpdateText();
            //c.Screech();
            //Console.WriteLine(""hey"");
            //", argsToPass);




            //Console.WriteLine("\nAfter:");
            //Console.WriteLine("Returned: " + ((int)argsToPass[0]).ToString());
            //Console.WriteLine("Returned: " + ((string)argsToPass[1]).ToString());
            //r1.Screech();

            Console.ReadKey();

        }
    }

    
    public struct Rat
    {
        public void Squeak()
        {
            Console.WriteLine("squeak");
        }
    }

    namespace Second
    {
        public class Bat
        {
            private string _sound = "screech";
            public int i = 0;
            public void Screech()
            {
                Console.WriteLine(_sound);
                Console.WriteLine("i: " + i.ToString());
            }
            public void UpdateText()
            {
                _sound = "new screech";
                i++;
            }
        }
    }
}
