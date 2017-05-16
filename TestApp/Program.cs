using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestApp.Concrete;

namespace TestApp
{
    class Program
    {
        static string filePath;

        static void Main(string[] args)
        {
            if (!args.Any())
            {
                Console.WriteLine("Input arguments (-f <filepath>):");
                var input = Console.ReadLine();
                args = input?.Split(' ') ?? new string[] { };
            }

            ParseArgs(new Queue<string>(args));

            var obs = new ConsoleObserver();

            var listener = new FileListener(filePath);

            listener.Attach(obs);

            listener.Run(args);

            Console.ReadLine();

            listener.Detach(obs);

            Console.ReadLine();
        }

        private static void ParseArgs(Queue<string> args)
        {
            var arg = args.Dequeue();

            switch (arg)
            {
                case "-f":
                    var path = args.Dequeue();
                    filePath = Path.GetFullPath(path);
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