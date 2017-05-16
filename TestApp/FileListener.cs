using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class FileListener : Listener
    {
        private string folderPath;
        private string fileName;
        private int position;
        private IList<IObserver> observers;

        public FileListener(string filePath)
        {
            observers = new List<IObserver>();
        }

        public void Run(string[] args)
        {
            var watcher = new FileSystemWatcher
            {
                Path = folderPath,
                NotifyFilter = NotifyFilters.LastWrite,
                Filter = fileName
            };
            watcher.Changed += OnChanged;
            watcher.EnableRaisingEvents = true;
        }

        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in observers)
            {
                observer.Update();
            }
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
