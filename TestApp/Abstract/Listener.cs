using System.Collections.Generic;
using System.IO;

namespace TestApp.Abstract
{
    abstract class Listener
    {
        protected string FileFolder { get; }
        public string FileName { get; }
        protected IList<IObserver> Observers { get; }
        public string Message { get; set; }

        protected Listener(string filePath)
        {
            FileName = Path.GetFileName(filePath);
            FileFolder = Path.GetDirectoryName(filePath);
            Observers = new List<IObserver>();
        }

        public void Attach(IObserver observer)
        {
            Observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            Observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in Observers)
            {
                observer.Update(this);
            }
        }
    }
}
