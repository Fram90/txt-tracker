using System;
using TestApp.Abstract;

namespace TestApp.Concrete
{
    class ConsoleObserver : IObserver
    {
        //private readonly string _file;
        //private readonly string _message;

        //public ConsoleObserver()
        //{

        //}

        //public ConsoleObserver(Func<bool,string> str )
        //{
            
        //}

        public void Update(Listener listener)
        {
            Console.WriteLine($"File: {listener.FileName}\tMessage: {listener.Message}");
        }
    }
}
