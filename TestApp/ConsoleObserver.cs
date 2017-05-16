using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
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
