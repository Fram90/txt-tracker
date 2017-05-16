using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TestApp
{
    class Program
    {
        static string fileName;
        static string folderPath;

        static void Main(string[] args)
        {
            if (!args.Any())
            {
                Console.WriteLine("Input arguments (-f <filepath>):");
                var input = Console.ReadLine();
                args = input?.Split(' ') ?? new string[] { };
            }

            ParseArgs(new Queue<string>(args));

            var listener = new FileListener();
            listener
            listener.Run(args);
            

            Console.ReadLine();
        }

        private static void ParseArgs(Queue<string> args)
        {
            var arg = args.Dequeue();

            switch (arg)
            {
                case "-f":
                    var filePath = args.Dequeue();
                    fileName = filePath.Split('/').Last();
                    folderPath = filePath.Substring(0, filePath.Length - fileName.Length);
                    break;
                default:
                    Console.WriteLine("Unknown parameter");
                    break;
            }

            if (args.Any())
            {
                ParseArgs(args);
            }
        }
    }
}