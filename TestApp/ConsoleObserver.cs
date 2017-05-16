using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class ConsoleObserver : IObserver
    {
        private readonly string _file;
        private readonly string _message;

        public ConsoleObserver(string file, string message)
        {
            _file = file;
            _message = message;
        }
        public void Update()
        {
            Console.WriteLine($"File: {_file}._Message: {_message}");
        }
    }
}
