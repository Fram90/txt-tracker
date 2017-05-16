using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TestApp
{
    class Program
    {
        private static string filePath;
        private static string fileName;
        private static int position;

        static void Main(string[] args)
        {
            if (!args.Any())
            {
                Console.WriteLine("Input arguments (-f <filepath>):");
                var input = Console.ReadLine();
                args = input?.Split(' ') ?? new string[] { };
            }

            ParseArgs(new Queue<string>(args));

            var watcher = new FileSystemWatcher
            {
                Path = filePath,
                NotifyFilter = NotifyFilters.LastWrite,
                Filter = fileName
            };
            position = GetInitialSize(Path.Combine(filePath,fileName));
            watcher.Changed += OnChanged;
            watcher.EnableRaisingEvents = true;


            Console.WriteLine("Listening to the file...");

            Console.ReadLine();
        }

        private static void ParseArgs(Queue<string> args)
        {
            var arg = args.Dequeue();

            switch (arg)
            {
                case "-f":
                    var path = args.Dequeue();
                    filePath = Path.GetDirectoryName(path);
                    fileName = Path.GetFileName(path);
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

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            var file = new List<string>();

            using (var fs = new FileStream(e.FullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var sr = new StreamReader(fs))
            {
                while (!sr.EndOfStream)
                {
                    file.Add(sr.ReadLine());
                }

            }

            int currentPos = 0;

            foreach (var line in file)
            {
                currentPos++;
                if (currentPos > position)
                {
                    Console.WriteLine(line);
                }
            }
            position = currentPos;
        }

        private static int GetInitialSize(string path)
        {
            return File.ReadLines(path).Count();
        }
    }
}