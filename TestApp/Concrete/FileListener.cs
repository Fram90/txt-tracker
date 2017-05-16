using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestApp.Abstract;

namespace TestApp.Concrete
{
    class FileListener : Listener
    {
        private int position;

        public FileListener(string filePath) : base(filePath)
        {
            position = GetInitialSize(filePath);
        }

        public void Run(string[] args)
        {
            var watcher = new FileSystemWatcher
            {
                Path = FileFolder,
                NotifyFilter = NotifyFilters.LastWrite,
                Filter = FileName
            };
            watcher.Changed += OnChanged;
            watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object source, FileSystemEventArgs e)
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
                    Message = line;
                    Notify();
                }

            }
            position = currentPos;
        }

        private int GetInitialSize(string path)
        {
            return File.ReadLines(path).Count();
        }
    }
}
