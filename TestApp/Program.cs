using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var zxc = 3 / 7;
            using (var directoryEntry = new DirectoryEntry(@"LDAP://hq-dc1.office.auriga.msk/DC=office,DC=auriga,DC=msk"))
            {
                using (var directorySearcher = new DirectorySearcher(directoryEntry))
                {
                    directorySearcher.Filter =
                        $"(&(&(objectClass=user)(objectClass=person)))";
                    directorySearcher.SizeLimit = 9999999;
                    directorySearcher.PageSize = 9999999;
                    directorySearcher.ServerTimeLimit = new TimeSpan(0, 0, 15, 0);
                    directorySearcher.PropertiesToLoad.Add("employeeID");
                    directorySearcher.PropertiesToLoad.Add("sAMAccountName");
                    directorySearcher.PropertiesToLoad.Add("thumbnailPhoto");
                    directorySearcher.PropertiesToLoad.Add("givenName");
                    directorySearcher.PropertiesToLoad.Add("sn");
                    using (var users = directorySearcher.FindAll())
                    {
                        foreach (SearchResult entry in users)
                        {
                            var test = entry.Properties["samaccountname"];
                        }
                    }
                }
            }

            Console.ReadLine();
        }
    }

    class A
    {
        public List<int> List { get; set; }
    }
}