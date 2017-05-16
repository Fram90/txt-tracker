using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    abstract class Listener
    {
        public string FileFolder { get; }
        public string FileName { get; }

        protected Listener(string filePath)
        {
            FileName = filePath.Split('/').Last();
            FileFolder = filePath.Substring(0, filePath.Length - FileName.Length);
        }
    }
}
