using SearchRegularExpression.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SearchRegularExpression
{
    class Program
    {
        private const int SUCCESS_CODE = 1;

        static void Main(string[] args)
        {
            if (args[0] == null || args[1] == null)
            {
                Console.WriteLine("Wrong arguments! You must set the path to the file and regex expression");
                return;
            }                
            IFileFactory factory = new SelectionFileFactory();
            var file = factory.CreateFile(args[0]);
            int count = file.Recognition(args[1]);
            Console.WriteLine(count);
            Console.ReadLine();
            if (count > 0)
                Environment.ExitCode = SUCCESS_CODE;
        }
       
    }
   
}
